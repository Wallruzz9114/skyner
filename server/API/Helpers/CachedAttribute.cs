using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace API.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _lifeSpan;

        public CachedAttribute(int lifeSpan) => _lifeSpan = lifeSpan;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Get reference to cache service
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            // Generate cache key based on the query string parameters
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            // Cache the key (for every request)
            var cachedResponse = await cacheService.GetCacheResponseAsync(cacheKey);

            if (string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;
                return;
            }

            // Move to controller, get response from databse
            var executedContext = await next();
            // If there is a response, cache it
            if (executedContext.Result is OkObjectResult objectResult)
                await cacheService.CacheResponseAsync(cacheKey, objectResult.Value, TimeSpan.FromSeconds(_lifeSpan));
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"{ request.Path }");

            foreach (var (key, value) in request.Query.OrderBy(pair => pair.Key))
                stringBuilder.Append($"|{ key }--{ value }");

            return stringBuilder.ToString();
        }
    }
}