using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using Shop.Repositories.Data;
using Shop.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ShopContext _context;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, ShopContext context)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            ViewData["Categories"] = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name");
            //ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name");
            var products = await _productRepository.GetAllAsync();

            var model = new ProductPageModel
            {
                SearchCriteria = new ProductCriteriaModel(),
                Products = products
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Search(ProductPageModel model)
        {
            ViewData["Categories"] = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name");

            model.Products = await _productRepository.GeneralFilterAsync(model.SearchCriteria);

            return View("Index", model);
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

        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Count,SKU,CategoryId")] ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.CreateAsync(productModel);
                return RedirectToAction(nameof(Index));
            }

            return View(productModel);
        }

        public async Task<IActionResult> Edit(int? id)
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

            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAllAsync(), "Id", "Name");
            return View(productModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Count,SKU,CategoryId")] ProductModel productModel)
        {
            if (id != productModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productRepository.UpdateAsync(productModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_productRepository.GetAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);
        }

        public async Task<IActionResult> Delete(int? id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
