using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WhizKids.Auth.Models;
using WhizKids.Repositories;
using WhizKids.Models;
using Microsoft.Extensions.Configuration;

namespace WhizKids.Auth
{
    public class AccountController : Controller
    {
        private readonly IFirebaseAuthService _firebaseAuthService;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserStudentRepository _userStudentRepository;


        public AccountController(IFirebaseAuthService firebaseAuthService, IUserProfileRepository userProfileRepository, IUserStudentRepository userStudentRepository)
        {
            _userProfileRepository = userProfileRepository;
            _firebaseAuthService = firebaseAuthService;
            _userStudentRepository = userStudentRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Credentials credentials)
        {
            if (!ModelState.IsValid)
            {
                return View(credentials);
            }

            var fbUser = await _firebaseAuthService.Login(credentials);
            if (fbUser == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(credentials);
            }


            var userProfile = _userProfileRepository.GetByFirebaseUserId(fbUser.FirebaseUserId);
            if (userProfile == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to Login.");
                return View(credentials);
            }

            await LoginToApp(userProfile);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Registration registration)
        {
            if (!ModelState.IsValid)
            {
                return View(registration);
            }

            var fbUser = await _firebaseAuthService.Register(registration);

            if (fbUser == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to register, do you already have an account?");
                return View(registration);
            }

            var newUserStudent = new UserStudent
            {
                
                StudentId = registration.StudentId,
            };

            var newUserProfile = new UserProfile
            {

                Email = registration.Email,
                FirebaseUserId = fbUser.FirebaseUserId,
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                Address = registration.Address,
                PhoneNumber = registration.PhoneNumber,
                IsAdmin = 0,
                StudentId = registration.StudentId,
            };
            _userProfileRepository.AddUserProfile(newUserProfile, newUserStudent);
            _userStudentRepository.AddUserStudent(registration.StudentId, newUserProfile.Id);
            

            await LoginToApp(newUserProfile);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task LoginToApp(UserProfile userProfile)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userProfile.Id.ToString()),
                new Claim(ClaimTypes.Email, userProfile.Email),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }
    }
}