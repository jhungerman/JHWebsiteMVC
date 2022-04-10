using JosephHungerman.Models;
using JosephHungerman.Models.Contact;
using JosephHungerman.Models.Dtos;
using JosephHungerman.Models.ViewModels;
using JosephHungerman.Services;
using Microsoft.AspNetCore.Mvc;

namespace JosephHungerman.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _service;
        private readonly ICaptchaService _captchaService;

        public ContactController(IContactService service, ICaptchaService captchaService)
        {
            _service = service;
            _captchaService = captchaService;
        }

        [BindProperty(Name = "g-recaptcha-response")]
        public string CaptchaResponse { get; set; }

        public async Task<IActionResult> Contact()
        {
            var captchaClientKey = _captchaService.ClientKey;
            var message = new Message();

            var viewModel = new ContactViewModel {CaptchaClientKey = captchaClientKey, Message = message};

            return View(viewModel);
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage(MessageDto message)
        {
            try
            {
                var requestIsValid = await _captchaService.IsCaptchaValid(CaptchaResponse);

                if (requestIsValid)
                {
                    var response = await _service.AddMessageAsync(message);

                    if (response.IsSuccess)
                    {
                        return RedirectToAction(nameof(MessageSuccess));
                    }
                }
                return RedirectToAction("Error");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IActionResult MessageSuccess()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel());
        }
    }
}
