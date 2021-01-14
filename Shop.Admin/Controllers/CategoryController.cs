using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using Shop.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Shop.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryRepository.GetAllAsync());
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.CreateAsync(categoryModel);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var categoryModel = await _categoryRepository.GetAsync(id);

            if (categoryModel == null)
                return NotFound();

            return View(categoryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CategoryModel categoryModel)
        {
            if (id != categoryModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryRepository.UpdateAsync(categoryModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await CategoryModelExists(categoryModel.Id)))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var categoryModel = await _categoryRepository.GetAsync(id);

            if (categoryModel == null)
                return NotFound();

            return View(categoryModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CategoryModelExists(int id)
        {
            return await _categoryRepository.CategoryExistsAsync(id);
        }
    }
}
