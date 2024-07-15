using reCAPTCHA.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CaptchaIntegration.Models;
using System.Diagnostics;

namespace CaptchaIntegration.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRecaptchaService _recaptchaService;

        public HomeController(IRecaptchaService recaptchaService)
        {
            _recaptchaService = recaptchaService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string gRecaptchaResponse)
        {
            var recaptchaResult = new RecaptchaResponse();
            if (gRecaptchaResponse != null)
                recaptchaResult = await _recaptchaService.Validate(gRecaptchaResponse);

            // Check if recaptchaResult has the Success property or equivalent
            if (recaptchaResult == null || !recaptchaResult.success) // Adjust according to actual property name
            {
                ModelState.AddModelError(string.Empty, "Please complete the CAPTCHA.");
                return View();
            }

            // Process the login (check username and password)
            // ...

            return RedirectToAction("Index");
        }
    }
}