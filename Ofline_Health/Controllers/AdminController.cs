using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Ofline_Health.Models;
using Ofline_Health.Data;

namespace Ofline_Health.Controllers
{

    public class AdminController : Controller
    {
        private const string AdminUsername = "admin";
        private const string AdminPassword = "password";

        private readonly ILogger<AdminController> _logger;
        private readonly Ofline_HealthContext _context;
        public AdminController(ILogger<AdminController> logger,Ofline_HealthContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == AdminUsername && password == AdminPassword)
            {
                // Set a session variable to indicate that the user is logged in
                HttpContext.Session.SetString("AdminLoggedIn", "true");

                // Create and set authentication cookie
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                var authScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                HttpContext.SignInAsync(authScheme, new ClaimsPrincipal(new ClaimsIdentity(authClaims, authScheme)), authProperties);

                return RedirectToAction(nameof(LoginSuccess));
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult LoginSuccess()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Logout()
        {
            // Remove the "AdminLoggedIn" session variable
            HttpContext.Session.Remove("AdminLoggedIn");

            // Sign out the authentication cookie
            var authScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            HttpContext.SignOutAsync(authScheme);

            return RedirectToAction(nameof(Login));
        }

        public IActionResult Check()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddDoctor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDoctor(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctor);
                _context.SaveChanges();
                return RedirectToAction(nameof(ViewDoctors));
            }

            return View(doctor);
        }

        public IActionResult ViewDoctors()
        {
            var doctor = _context.Doctor.ToList();
            return View(doctor);
        }

        public IActionResult EditDoctors(int id)
        {
            var doctor = _context.Doctor.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpPost]
        public IActionResult EditDoctors(int id, Doctor doctor)
        {
            if (id != doctor.DocotorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.DocotorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ViewDoctors));
            }

            return View(doctor);
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctor.Any(e => e.DocotorId == id);
        }

        public IActionResult DeleteDoctor(int id)
        {
            var doctor = _context.Doctor.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpPost, ActionName("DeleteDoctor")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteDoctorconfirmed(int id)
        {
            var  doctors = _context.Doctor.Find(id);
            _context.Doctor.Remove(doctors);
            _context.SaveChanges();
            return RedirectToAction(nameof(ViewDoctors));
        }

    }
}



