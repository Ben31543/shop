using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Repositories.Interfaces;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _productRepository.GetAsync(id);

            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }
    }
}