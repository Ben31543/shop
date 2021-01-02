using Microsoft.AspNetCore.Mvc;
using Shop.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopViewRepository _viewRepository;
        
        public ShopController(IShopViewRepository viewRepository)
        {
            _viewRepository = viewRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await _viewRepository.GetAllAsync());
        }
    }
}
