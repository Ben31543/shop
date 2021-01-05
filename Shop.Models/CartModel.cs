using System.Collections.Generic;

namespace Shop.Models
{
    public class CartModel
    {
        public List<CartItemModel> Products { get; set; }
        public decimal TotalSum { get; set; }
    }
}
