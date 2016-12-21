using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using userDashboard.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;


namespace userDashboard.Controllers
{
    public class HomeController : Controller
    {
        PasswordHasher<User> Hasher = new PasswordHasher<User>();
        private DataContext _context;
        public HomeController(DataContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("curUserEmail") != null)
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }
            @ViewBag.Error = "";
            @ViewBag.isThere = false;
            return View();
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterUser RegisterUser)
        {   
            if(ModelState.IsValid)
            {
                if(HttpContext.Session.GetString("curUserEmail") != null)
                {
                    return RedirectToAction("Dashboard", "Dashboard");
                }
                if(_context.Users.SingleOrDefault(user => user.email == RegisterUser.Email) != null)
                {
                    @ViewBag.Error = "Error: Email Already In Use";
                    @ViewBag.isThere = true;
                    return View();
                }
                User NewUser = new User
                {
                    firstname = RegisterUser.Firstname,
                    lastname = RegisterUser.Lastname,
                    email = RegisterUser.Email,
                    password = RegisterUser.Password,
                    createdat = DateTime.Now,
                    updatedat = DateTime.Now
                };
                NewUser.password = Hasher.HashPassword(NewUser, RegisterUser.Password);
                _context.Users.Add(NewUser);
                _context.SaveChanges();
                HttpContext.Session.SetString("curUserEmail", NewUser.email);
                return RedirectToAction("Dashboard", "Dashboard");
            }
            return View();
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginUser LoginUser)
        {
            if(ModelState.IsValid)
            {
                User thisuser = _context.Users.SingleOrDefault(user => user.email == LoginUser.Email);
                if(thisuser != null && 0 != Hasher.VerifyHashedPassword(thisuser, thisuser.password, LoginUser.Password))
                {
                    HttpContext.Session.SetString("curUserEmail", LoginUser.Email);
                    return RedirectToAction("Dashboard", "Dashboard");
                }
            }
            Console.WriteLine("urgh!!!!");
            @ViewBag.isThere = true;
            @ViewBag.Error = "Please check your email or password again.";
            return View("Index", LoginUser);
        }
        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
