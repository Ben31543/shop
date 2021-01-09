using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using Shop.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartController(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _cartRepository.CartViewAsync();

            CartModel model = new CartModel
            {
                Products = products
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(ProductModel model, int productCount)
        {
            var product = await _productRepository.GetAsync(model.Id);

            await _cartRepository.AddToCartAsync(product, productCount);

            return RedirectToAction("Products", "Shop");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItemModel = await _cartRepository.GetCartItemAsync(id);

            if (cartItemModel == null)
            {
                return NotFound();
            }

            return View(cartItemModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Count,Product,ProductId")] CartItemModel cartItemModel)
        {
            if (id != cartItemModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _cartRepository.UpdateAsync(cartItemModel);
                }

                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(cartItemModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _cartRepository.GetCartItemAsync(id);

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
            await _cartRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
