using System.Collections.Generic;
using System.Linq;

namespace Shop.Models
{
    public class CartModel
    {
        public List<CartItemModel> Products { get; set; }

        public decimal TotalSum => Products?.Sum(s => s.Product.Price) ?? 0;
    }
}
