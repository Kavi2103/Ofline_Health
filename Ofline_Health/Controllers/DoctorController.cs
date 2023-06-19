using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ofline_Health.Data;
using System.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Ofline_Health.Models;
using Ofline_Health.Migrations;

namespace Ofline_Health.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ILogger<DoctorController> _logger;
        private readonly Ofline_HealthContext _context;
        public DoctorController(ILogger<DoctorController> logger, Ofline_HealthContext context)
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
        public async Task<IActionResult> Login(string Email, string Password)
        {
            if (ModelState.IsValid)
            {

                var patients = await _context.Doctor.FirstOrDefaultAsync(p => p.Email == Email && p.Password == Password);
                // Set a session variable to indicate that the user is logged in
                HttpContext.Session.SetString("DoctorLoggedIn", "true");
                if (patients != null)
                {
                    // Create and set authentication cookie
                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Email),
                    new Claim(ClaimTypes.Role, "Doctor")
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
               
            }
                    
            }
           
            return View();
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult LoginSuccess()
        {
            return View();
        }

        [Authorize(Roles = "Doctor")]
        [HttpPost]
        public IActionResult Logout()
        {
            // Remove the "AdminLoggedIn" session variable
            HttpContext.Session.Remove("DoctorLoggedIn");

            // Sign out the authentication cookie
            var authScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            HttpContext.SignOutAsync(authScheme);

            return RedirectToAction(nameof(Login));
        }


        [Authorize]
        public async Task<IActionResult> Details()
        {
            // Get the current user's email address from the claims
            var userEmail = User.FindFirstValue(ClaimTypes.Name);

            // Get the patient with the matching email address
            var doctors = await _context.Doctor.FirstOrDefaultAsync(p => p.Email == userEmail);

            if (doctors == null)
            {
                return NotFound();
            }

            return View(doctors);
        }


        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> ViewPatientsBySpecialty()
        {
            // Get the current user's email address from the claims
            var doctorEmail = User.FindFirstValue(ClaimTypes.Name);

            // Get the doctor with the matching email address
            var doctor = await _context.Doctor.FirstOrDefaultAsync(d => d.Email == doctorEmail);

            if (doctor == null)
            {
                return NotFound();
            }

            // Get the list of patients with the matching specialty
            var patients = await _context.Patient.Where(p => p.Problem == doctor.Specialty).ToListAsync();

            return View(patients);
        }

        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> PatientDetails(int id)
        {
            var doctorEmail = User.FindFirstValue(ClaimTypes.Name);
            var doctor = await _context.Doctor.FirstOrDefaultAsync(d => d.Email == doctorEmail);

            if (doctor == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.FirstOrDefaultAsync(p => p.PatientId == id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [Authorize(Roles = "Doctor")]
        public IActionResult AddPrescription(int id)
        {
            // Retrieve the patient with the matching ID from the database
            var patient = _context.Patient.FirstOrDefault(p => p.PatientId == id);

            if (patient == null)
            {
                return NotFound();
            }

            // Pass the patient to the view for prescription creation
            return View(patient);
        }


    }
}
