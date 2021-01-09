

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Models;
using Shop.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ShopController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Products()
        {
            ViewData["Categories"] = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name");

            var products = await _productRepository.GetAllAsync();

            var model = new ProductPageModel
            {
                SearchCriteria = new ProductCriteriaModel(),
                Products = products
            };

            return View(model);
        }

        public async Task<IActionResult> Search(ProductPageModel pageModel)
        {
            ViewData["Categories"] = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name");

            pageModel.Products = await _productRepository.GeneralFilterAsync(pageModel.SearchCriteria);

            return View("Products", pageModel);
        }
    }
}
