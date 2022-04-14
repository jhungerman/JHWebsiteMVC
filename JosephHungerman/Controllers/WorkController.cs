using JosephHungerman.Models;
using JosephHungerman.Models.ViewModels;
using JosephHungerman.Models.Work;
using JosephHungerman.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JosephHungerman.Controllers
{
    public class WorkController : Controller
    {
        private readonly IResumeService _resumeService;
        private readonly IQuoteService _quoteService;

        public WorkController(IResumeService resumeService, IQuoteService quoteService)
        {
            _resumeService = resumeService;
            _quoteService = quoteService;
        }

        public async Task<IActionResult> Resume()
        {
            return await GetResumeDetailsAsync();
        }

        [HttpGet("Work/Resume/Edit")]
        public async Task<IActionResult> EditResume()
        {
            return await GetResumeDetailsAsync();
        }

        [HttpPost]

        public IActionResult Error()
        {
            return View(new ErrorViewModel());
        }

        private async Task<IActionResult> GetResumeDetailsAsync()
        {
            var response = await _resumeService.GetResumeDetailsAsync();

            if (response.IsSuccess)
            {
                var resumeViewModel = new ResumeViewModel
                {
                    Resume = (Resume)response.Result!
                };

                var quoteResponse = await _quoteService.GetPageQuoteAsync(PageType.Resume);
                if (quoteResponse.IsSuccess)
                {
                    resumeViewModel.Quote = (Quote)quoteResponse.Result!;
                    return View(resumeViewModel);
                }
            }

            return RedirectToAction(nameof(Error));
        }
    }
}
