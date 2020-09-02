namespace API.ViewModels
{
    public class OrderViewModel
    {
        public string CartId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressViewModel ShippingAddress { get; set; }
    }
}