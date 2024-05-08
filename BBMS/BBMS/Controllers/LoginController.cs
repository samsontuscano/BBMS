using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using BBMS.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BBMS.Controllers
{
    
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Authenticate(User user)
        {
            if (user.Username == "receiver" && user.Password == "receiver")
            {
                TempData["user"] = user.Username;
                // Authentication successful
                return RedirectToAction("Create", "OrdersController2");
            }
            else if((user.Username == "staff" && user.Password == "staff") ||
                    (user.Username == "admin" && user.Password == "admin"))
            {
                TempData["user"] = user.Username;
                // Authentication successful
                return RedirectToAction("Index", "OrdersController2");
            }
            else
            {
                // Authentication failed
                ViewBag.Message = "Invalid username or password. Please try again.";
                return View("Index", user);
            }
        }

        public ActionResult Welcome(string username)
        {
            ViewBag.Username = "username";
            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}
