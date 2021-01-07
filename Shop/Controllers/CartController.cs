using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<IActionResult> AddToCart(ProductModel model, int productCount)
        {
            var product = await _productRepository.GetAsync(model.Id);

            await _cartRepository.AddToCartAsync(product, productCount);

            return RedirectToAction("Products", "Shop");
        }

        public async Task<IActionResult> Index()
        {
	        CartModel model = new CartModel
	        {
		        Products = await _cartRepository.CartViewAsync()
	        };

			return View(model);
        }
    }
}
