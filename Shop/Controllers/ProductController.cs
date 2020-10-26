using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using Shop.Repositories.Data;
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

        // GET: Product
        public async Task<IActionResult> Index(string searchString, int page = 1)
        {
            //var foundProducts = _productRepository.SearchedProducts(searchString);
            //return View(await foundProducts);

            var products = await _productRepository.GetAllAsync();
            int pageSize = 5, productCount = products.Count();

            if (page * pageSize - 2 > productCount)
            {
                if (productCount % pageSize == 0)
                    page = productCount / pageSize;
                else
                    page = productCount / pageSize + 1;
            }

            if (page <= 1)
                page = 1;
            ViewData["page"] = page;

            if (productCount % pageSize == 0)
                ViewData["allPageCount"] = productCount / pageSize;
            else
                ViewData["allPageCount"] = productCount / pageSize + 1;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = await _productRepository.SearchedProducts(searchString);
            }
            else
                products = products.Skip((page - 1) * pageSize).Take(pageSize);

            return View(products);
        }

        // GET: Product/Details/5
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

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Count,SKU")] ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.CreateAsync(productModel);
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);
        }

        // GET: Product/Edit/5
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
            return View(productModel);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Count,SKU")] ProductModel productModel)
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
                    if (!ProductModelExists(productModel.Id))
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

        // GET: Product/Delete/5
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

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductModelExists(int id)
        {
            return _productRepository.ProductExists(id);
        }
    }
}
