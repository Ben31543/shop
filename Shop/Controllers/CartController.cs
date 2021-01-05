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

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IActionResult> AddToCart(ProductModel model, int productCount)
        {
            await _cartRepository.AddToCart(model, productCount);
            return View();
        }

        public async Task<IActionResult> IndexAsync()
        {
            var cartItems = await _cartRepository.CartView();
            return View(cartItems);
        }
    }
}
