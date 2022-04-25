using System.Diagnostics;
using JosephHungerman.Data.Models;
using JosephHungerman.Services.Services.Interfaces;
using JosephHungerman.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JosephHungerman.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuoteService _quoteService;

        public HomeController(IQuoteService quoteService)
        {
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