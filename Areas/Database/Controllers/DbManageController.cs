using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Areas.Database.Controllers
{
    [Area("Database")]
    [Route("database/manage/[action]")]
    public class DbManageController : Controller
    {
        private readonly EcommerceDbContext _ecommerceDbContext;
        private readonly ILogger<DbManageController> _logger; 
        public DbManageController(EcommerceDbContext ecommerceDbContext, ILogger<DbManageController> logger)
        {
            _ecommerceDbContext = ecommerceDbContext;
            _logger = logger;
        }
        [TempData]
        public string StatusMessage { set; get; }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DeleteDb()
        {
            _logger.LogWarning("Hihi");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteDbAsync()
        {
            var success = await _ecommerceDbContext.Database.EnsureDeletedAsync();
            StatusMessage = success ? "Complete delete" : "Cannot delete";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Migration()
        {
            await _ecommerceDbContext.Database.MigrateAsync();
            StatusMessage = "Create DB success";
            _logger.LogInformation(nameof(Index));
            return RedirectToAction(nameof(Index));
        }
    }
}
