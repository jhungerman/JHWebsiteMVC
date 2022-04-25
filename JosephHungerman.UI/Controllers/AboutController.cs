using JosephHungerman.Data.Models;
using JosephHungerman.Services.Services.Interfaces;
using JosephHungerman.UI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JosephHungerman.UI.Controllers
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

        [Authorize]
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

                foreach (var modelSection in model.Sections)
                {
                    modelSection.Paragraphs = modelSection.Paragraphs.OrderBy(p => p.Id).ToList();
                }

                return View(model);
            }

            return RedirectToAction(nameof(Error));
        }

        [Authorize]
        [HttpPost("About/Edit")]
        public async Task<IActionResult> SaveAbout(AboutViewModel aboutView)
        {
            ModelState.Clear();
            var aboutResponse = await _aboutService.UpdateSectionsAsync(aboutView.Sections);

            if (aboutResponse.IsSuccess)
            {
                return View(nameof(EditAbout), aboutView);
            }

            return RedirectToAction(nameof(Error));
        }

        public IActionResult AddSection(AboutViewModel aboutView)
        {
            aboutView.Sections.Add(new Section { Paragraphs = new List<Paragraph> {new()}});

            return View(nameof(EditAbout), aboutView);
        }

        public IActionResult RemoveSection(AboutViewModel aboutView, int index)
        {
            ModelState.Clear();
            aboutView.Sections.RemoveAt(index);

            return View(nameof(EditAbout), aboutView);
        }

        public IActionResult RemoveParagraph(AboutViewModel aboutView, int sectionIndex, int paraIndex)
        {
            ModelState.Clear();
            aboutView.Sections[sectionIndex].Paragraphs.RemoveAt(paraIndex);

            return View(nameof(EditAbout), aboutView);
        }

        public IActionResult AddParagraph(AboutViewModel aboutView, int sectionIndex)
        {
            ModelState.Clear();
            aboutView.Sections[sectionIndex].Paragraphs.Add(new Paragraph());

            return View(nameof(EditAbout), aboutView);
        }
    }
}
