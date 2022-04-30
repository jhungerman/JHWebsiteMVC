using JosephHungerman.Data.Models;
using JosephHungerman.Services.Models.Dtos;
using JosephHungerman.Services.Services.Interfaces;
using JosephHungerman.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JosephHungerman.UI.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ContactController : Controller
    {
        private readonly IContactService _service;
        private readonly ICaptchaService _captchaService;
        private readonly IQuoteService _quoteService;

        public ContactController(IContactService service, ICaptchaService captchaService, IQuoteService quoteService)
        {
            _service = service;
            _captchaService = captchaService;
            _quoteService = quoteService;
        }

        [BindProperty(Name = "g-recaptcha-response")]
        public string CaptchaResponse { get; set; }

        [HttpGet("Contact")]
        public async Task<IActionResult> Contact()
        {
            var response = await _quoteService.GetPageQuoteAsync(PageType.Contact);

            if (response.IsSuccess)
            {
                var captchaClientKey = _captchaService.ClientKey;
                var message = new Message();

                var viewModel = new ContactViewModel { CaptchaClientKey = captchaClientKey, Message = message, Quote = (Quote)response.Result!};

                return View(viewModel);
            }

            return RedirectToAction(nameof(Error));
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

        public async Task<IActionResult> MessageSuccess()
        {
            var response = await _quoteService.GetPageQuoteAsync(PageType.MessageSuccess);

            if (response.IsSuccess)
            {
                var viewModel = new MessageSuccessViewModel() { Quote = (Quote)response.Result! };

                return View(viewModel);
            }

            return RedirectToAction(nameof(Error));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel());
        }
    }
}
