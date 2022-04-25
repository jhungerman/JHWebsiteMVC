using JosephHungerman.Data.Models;
using JosephHungerman.Services.Services.Interfaces;
using JosephHungerman.UI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JosephHungerman.UI.Controllers
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

        [Authorize]
        [HttpGet("Work/Resume/Edit")]
        public async Task<IActionResult> EditResume()
        {
            return await GetResumeDetailsAsync();
        }

        [Authorize]
        [HttpPost("Work/Resume/Edit")]
        public async Task<IActionResult> SaveResume(ResumeViewModel resumeModel)
        {
            var resumeResponse = await _resumeService.AddResumeAsync(resumeModel.Resume);

            if (resumeResponse.IsSuccess)
            {
                return View(nameof(EditResume), resumeModel);
            }
            return RedirectToAction(nameof(Error));
        }

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

        public IActionResult AddSkill(ResumeViewModel resumeModel)
        {
            resumeModel.Resume.Skills.Add(new Skill());

            return View(nameof(EditResume), resumeModel);
        }

        public IActionResult RemoveSkill(ResumeViewModel resumeModel, int index)
        {
            ModelState.Clear();
            resumeModel.Resume.Skills.RemoveAt(index);

            return View(nameof(EditResume), resumeModel);
        }

        public IActionResult AddWorkExperience(ResumeViewModel resumeModel)
        {
            resumeModel.Resume.WorkExperiences.Add(new WorkExperience { WorkDetails = new List<WorkDetail> {new WorkDetail()}});

            return View(nameof(EditResume), resumeModel);
        }

        public IActionResult AddWorkDetail(ResumeViewModel resumeModel, int workExperienceId)
        {
            resumeModel.Resume.WorkExperiences[workExperienceId].WorkDetails.Add(new WorkDetail());

            return View(nameof(EditResume), resumeModel);
        }

        public IActionResult RemoveWorkDetail(ResumeViewModel resumeModel, int workIndex, int detailIndex)
        {
            ModelState.Clear();
            resumeModel.Resume.WorkExperiences[workIndex].WorkDetails.RemoveAt(detailIndex);

            return View(nameof(EditResume), resumeModel);
        }

        public IActionResult RemoveWorkExperience(ResumeViewModel resumeModel, int index)
        {
            ModelState.Clear();
            resumeModel.Resume.WorkExperiences.RemoveAt(index);

            return View(nameof(EditResume), resumeModel);
        }

        public IActionResult AddEducation(ResumeViewModel resumeModel)
        {
            resumeModel.Resume.Educations.Add(new Education());

            return View(nameof(EditResume), resumeModel);
        }

        public IActionResult RemoveEducation(ResumeViewModel resumeModel, int index)
        {
            ModelState.Clear();
            resumeModel.Resume.Educations.RemoveAt(index);

            return View(nameof(EditResume), resumeModel);
        }

        public IActionResult AddCertification(ResumeViewModel resumeModel)
        {
            resumeModel.Resume.Certifications.Add(new Certification());

            return View(nameof(EditResume), resumeModel);
        }

        public IActionResult RemoveCertification(ResumeViewModel resumeModel, int index)
        {
            ModelState.Clear();
            resumeModel.Resume.Certifications.RemoveAt(index);

            return View(nameof(EditResume), resumeModel);
        }
    }
}
