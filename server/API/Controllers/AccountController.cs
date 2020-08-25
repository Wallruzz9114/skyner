using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Errors;
using API.Extensions;
using API.ViewModels;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJWTTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IJWTTokenService jwtTokenService,
            IMapper mapper
        )
        {
            _jwtTokenService = jwtTokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("get")]
        public async Task<ActionResult<AppUserViewModel>> GetCurrentAppUser()
        {
            var appUser = await _userManager.FindByClaimsPrincipal(HttpContext.User);

            return new AppUserViewModel
            {
                Email = appUser.Email,
                Token = _jwtTokenService.CreateToken(appUser),
                DisplayName = appUser.DisplayName
            };
        }

        [HttpGet("exists")]
        public async Task<ActionResult<bool>> UserExists([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressViewModel>> GetUserAddress()
        {
            var appUser = await _userManager.FindUserByCaimPrincipalWithAddressAsync(HttpContext.User);
            return _mapper.Map<Address, AddressViewModel>(appUser.Address);
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult<AddressViewModel>> UpdateUserAddressAsync(AddressViewModel addressViewModel)
        {
            var appUser = await _userManager.FindUserByCaimPrincipalWithAddressAsync(HttpContext.User);
            appUser.Address = _mapper.Map<AddressViewModel, Address>(addressViewModel);

            var identityResult = await _userManager.UpdateAsync(appUser);

            if (identityResult.Succeeded) return Ok(_mapper.Map<Address, AddressViewModel>(appUser.Address));

            return BadRequest("Problem updating the user");
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUserViewModel>> Login(LoginRequestViewModel loginRequestViewModel)
        {
            var appUser = await _userManager.FindByEmailAsync(loginRequestViewModel.Email);

            if (appUser == null)
                return Unauthorized(new APIResponse(StatusCodes.Status401Unauthorized));

            var signInResult = await _signInManager
                .CheckPasswordSignInAsync(appUser, loginRequestViewModel.Password, false);

            if (!signInResult.Succeeded)
                return Unauthorized(new APIResponse(StatusCodes.Status401Unauthorized));

            return new AppUserViewModel
            {
                Email = appUser.Email,
                Token = _jwtTokenService.CreateToken(appUser),
                DisplayName = appUser.DisplayName
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUserViewModel>> Register(RegisterRequestViewModel registerRequestViewModel)
        {
            var appUser = new AppUser
            {
                DisplayName = registerRequestViewModel.DisplayName,
                Email = registerRequestViewModel.Email,
                UserName = registerRequestViewModel.Email
            };

            var identityResult = await _userManager.CreateAsync(appUser, registerRequestViewModel.Password);

            if (!identityResult.Succeeded)
                return BadRequest(new APIResponse(StatusCodes.Status400BadRequest));

            return new AppUserViewModel
            {
                DisplayName = appUser.DisplayName,
                Token = _jwtTokenService.CreateToken(appUser),
                Email = appUser.Email
            };
        }
    }
}