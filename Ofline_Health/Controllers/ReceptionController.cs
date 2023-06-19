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

    public class ReceptionController : Controller
    {
        private const string AdminUsername = "Reception";
        private const string AdminPassword = "password";

        private readonly ILogger<ReceptionController> _logger;
        private readonly Ofline_HealthContext _context;
        public ReceptionController(ILogger<ReceptionController> logger,Ofline_HealthContext context)
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
                HttpContext.Session.SetString("ReceptionLoggedIn", "true");

                // Create and set authentication cookie
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Reception")
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

        [Authorize(Roles = "Reception")]
        public IActionResult LoginSuccess()
        {
            return View();
        }

        [Authorize(Roles = "Reception")]
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





        public IActionResult AddPatient()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Addpatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                _context.SaveChanges();
                return RedirectToAction(nameof(ViewPatients));
            }

            return View(patient);
        }

        public IActionResult ViewPatients()
        {
            var patients = _context.Patient.ToList();
            return View(patients);
        }

        public IActionResult EditPatient(int id)
        {
            var doctor = _context.Patient.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpPost]
        public IActionResult EditPatient(int id, Patient patient)
        {
            if (id != patient.PatientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(patient.PatientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ViewPatients));
            }

            return View(patient);
        }

        private bool DoctorExists(int id)
        {
            return _context.Patient.Any(e => e.PatientId == id);
        }


        public IActionResult DeletePatient(int id)
        {
            var patient = _context.Patient.Find(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpPost, ActionName("DeletePatient")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePatientConfirmed(int id)
        {
            var patient = _context.Patient.Find(id);
            _context.Patient.Remove(patient);
            _context.SaveChanges();
            return RedirectToAction(nameof(ViewPatients));
        }






    }



}



