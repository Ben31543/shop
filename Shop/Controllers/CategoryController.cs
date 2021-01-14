using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Repositories.Interfaces;

namespace Shop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) 
                return NotFound();

            var categoryModel = await _categoryRepository.GetAsync(id);

            if (categoryModel == null) 
                return NotFound();

            return View(categoryModel);
        }
    }
}
