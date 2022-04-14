using JosephHungerman.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using JosephHungerman.Models.ViewModels;
using JosephHungerman.Services;
using JosephHungerman.Services.Interfaces;

namespace JosephHungerman.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IQuoteService _quoteService;

        public HomeController(ILogger<HomeController> logger, IQuoteService quoteService)
        {
            _logger = logger;
            _quoteService = quoteService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _quoteService.GetPageQuoteAsync(PageType.Home);
            if (response.IsSuccess)
            {
                return View(new HomeViewModel { Quote = (Quote)response.Result! });
            }

            return RedirectToAction(nameof(Error));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}