using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce
{
    public class FirstController : Controller
    {
        private readonly ILogger<FirstController> _logger;
        private readonly ProductService _productService;

        public FirstController(ILogger<FirstController> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        [TempData]
        public string Message { set; get; }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult HelloView(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                username = "admin";
            }

            return View("xinchao3");
        }
        public IActionResult ProductView(int? id)
        {
            ProductModel product = _productService.Where(p => p.Id == id).FirstOrDefault();
            if (product == null)
            {
                Message = "Khong tim thay";
                return Redirect(Url.Action("Index","Home"));
            }
            ViewData["product"] = product;
            return View();
        }
    }
}
