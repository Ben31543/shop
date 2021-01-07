namespace Shop.Models
{
    public class CartItemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }

        public int ProductId { get; set; }

        public ProductModel Product { get; set; }

        public int Count { get; set; }
    }
}
