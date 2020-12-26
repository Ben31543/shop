using Microsoft.AspNetCore.Mvc;
using Shop.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductRepository _products;
        public OrderController(IProductRepository products)
        {
            _products = products;
        }
        public async Task<IActionResult> Order()
        {
            return View(await _products.GetAllAsync());
        }
    }
}
