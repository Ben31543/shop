using Microsoft.AspNetCore.Mvc;
using Shop.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductRepository products;
        public OrderController(IProductRepository products)
        {
            this.products = products;
        }
        public async Task<IActionResult> Order()
        {
            return View(await products.GetAllAsync());
        }
    }
}
