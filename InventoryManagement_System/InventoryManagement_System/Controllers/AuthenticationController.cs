using InventoryManagement_System.Interface;
using InventoryManagement_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;

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
        public async Task<IActionResult> Registration(Auth authModel)
        {
            // Hash the password before storing it
            authModel.Password = BCrypt.Net.BCrypt.HashPassword(authModel.Password);

            var res = await this.auth.RegistrationAsync(authModel);
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
        public async Task<IActionResult> LoginUser(Auth authModel)
        {
            var users = await this.auth.GetAuthUserAsync();

            if (users != null)
            {
                var dbUser = users.FirstOrDefault(u => u.Email == authModel.Email);

                if (dbUser != null && BCrypt.Net.BCrypt.Verify(authModel.Password, dbUser.Password))
                {
                    HttpContext.Session.SetString("Name", dbUser.Username);
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
