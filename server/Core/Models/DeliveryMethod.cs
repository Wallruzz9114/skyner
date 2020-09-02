namespace Core.Models
{
    public class DeliveryMethod : BaseModel
    {
        public string Name { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}