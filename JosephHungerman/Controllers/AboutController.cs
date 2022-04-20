using JosephHungerman.Models;
using JosephHungerman.Models.About;
using JosephHungerman.Models.ViewModels;
using JosephHungerman.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JosephHungerman.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        private readonly IQuoteService _quoteService;

        public AboutController(IAboutService aboutService, IQuoteService quoteService)
        {
            _aboutService = aboutService;
            _quoteService = quoteService;
        }

        [HttpGet("About")]
        public async Task<IActionResult> About()
        {
            return await GetAboutDetailsAsync();
        }

        [HttpGet("About/Edit")]
        public async Task<IActionResult> EditAbout()
        {
            return await GetAboutDetailsAsync();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel());
        }

        private async Task<IActionResult> GetAboutDetailsAsync()
        {
            var sectionResponse = await _aboutService.GetSectionsAsync();
            var quoteResponse = await _quoteService.GetPageQuoteAsync(PageType.About);

            if (sectionResponse.IsSuccess && quoteResponse.IsSuccess)
            {
                var model = new AboutViewModel
                {
                    Quote = (Quote)quoteResponse.Result!,
                    Sections = (List<Section>)sectionResponse.Result!
                };

                return View(model);
            }

            return RedirectToAction(nameof(Error));
        }
    }
}
