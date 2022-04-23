using JosephHungerman.Models;
using JosephHungerman.Models.ViewModels;
using JosephHungerman.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JosephHungerman.Controllers
{
    public class QuotesController : Controller
    {
        private readonly IQuoteService _quoteService;

        public QuotesController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [Authorize]
        [HttpGet("Quotes/Edit")]
        public async Task<IActionResult> EditQuotes()
        {
            var response = await _quoteService.GetPageQuotesAsync();

            if (response.IsSuccess)
            {
                var viewModel = new QuotesViewModel
                {
                    Quotes = (List<Quote>) response.Result!
                };

                return View(viewModel);
            }

            return RedirectToAction(nameof(Error));
        }

        [Authorize]
        [HttpPost("Quotes/Edit")]
        public async Task<IActionResult> SaveQuotes(QuotesViewModel quotesView)
        {
            ModelState.Clear();

            var response = await _quoteService.UpdateQuotesAsync(quotesView.Quotes);

            if (response.IsSuccess)
            {
                quotesView.Quotes = (List<Quote>) response.Result!;
                return View(nameof(EditQuotes), quotesView);
            }

            return RedirectToAction(nameof(Error));
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
