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

        public WorkController(IResumeService resumeService)
        {
            _resumeService = resumeService;
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

                return View(resumeViewModel);
            }

            return RedirectToAction(nameof(Error));
        }
    }
}
