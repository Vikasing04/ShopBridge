using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopBridge.BAL;
using ShopBridge.Modal;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServices _services;

        public HomeController(ILogger<HomeController> logger, IServices services)
        {
            _logger = logger;
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _services.GetALL();
               
                return View(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View();
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(InventoryML inventory)
        {
            try
            {
                var response = await _services.Create(inventory);
                if (response.Code == 200)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var res= await _services.GetById(id);
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InventoryML inventory)
        {
            try
            {
                var response = await _services.Edit(inventory);
                if (response.Code == 200)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await _services.GetById(id);
                return View(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View();


        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                var response = await _services.Delete(id);
                if (response.Code == 200)
                {
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
