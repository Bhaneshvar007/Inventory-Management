using InventoryManagement_System.Interface;
using InventoryManagement_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace InventoryManagement_System.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuth auth;

        public AuthenticationController(IAuth auth)
        {
            this.auth = auth;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Registration")]
        public async  Task<IActionResult> Registration(Auth auth)
        {
            // user pahgle se ragister hia uska logic
            var res = await this.auth.RegistrationAsync(auth);
            return RedirectToAction("LoginUser", "Authentication");
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var res = await this.auth.GetAuthUserAsync();
            return View(res);
        }

        public IActionResult LoginUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(Auth auth)
        {
            var user = await this.auth.GetAuthUserAsync();
            if (user != null)
            {
                if (user.FirstOrDefault().Email.Equals(auth.Email)) {
                    HttpContext.Session.SetString("Name" , user.FirstOrDefault().Username);
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.ErrorMessage = "Invalid email or password!";
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();      
            return RedirectToAction("LoginUser", "Authentication");    
        }

    }
}
