namespace Core.Models
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered() { }

        public ProductItemOrdered(int productItemId, string productName, string pictureURL)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            PictureURL = pictureURL;
        }

        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureURL { get; set; }
    }
}