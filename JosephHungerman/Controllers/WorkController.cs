using JosephHungerman.Models.ViewModels;
using JosephHungerman.Models.Work;
using JosephHungerman.Services;
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
            var response = await _resumeService.GetResumeDetailsAsync();

            if (response.IsSuccess)
            {
                var resumeViewModel = new ResumeViewModel
                {
                    Resume = (Resume) response.Result!
                };

                return View(resumeViewModel);
            }

            return RedirectToAction("Error");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel());
        }
    }
}
