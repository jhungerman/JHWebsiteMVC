using JosephHungerman.Data.Repositories;
using JosephHungerman.Models;
using JosephHungerman.Models.Dtos;
using JosephHungerman.Services;
using Microsoft.AspNetCore.Mvc;

namespace JosephHungerman.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _service;

        public ContactController(IContactService service)
        {
            _service = service;
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage(MessageDto message)
        {
            try
            {
                var response = await _service.AddMessageAsync(message);

                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(MessageSuccess));
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
