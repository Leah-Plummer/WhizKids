using Microsoft.AspNetCore.Mvc;
using WhizKids.Repositories;
using System.Collections.Generic;
using WhizKids.Models;
using System;
using WhizKids.Auth;
using WhizKids.Auth.Models;
using System.Threading.Tasks;

namespace WhizKids.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IFirebaseAuthService _firebaseAuthService;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserProfileRepository _userRepo;
        private readonly IStudentRepository _studentRepo;
        public UserProfileController(IUserProfileRepository userRepository, IStudentRepository studentRepository, IFirebaseAuthService firebaseAuthService, IUserProfileRepository userProfileRepository)
        {
            _userRepo = userRepository;
            _studentRepo = studentRepository;
            _userProfileRepository = userProfileRepository;
            _firebaseAuthService = firebaseAuthService;

        }

        // GET: UserProfiles
        public ActionResult Index()
        {
            List<UserProfile> users = _userRepo.GetAllUsersProfiles();

            return View(users);
        }

        // GET:UserProfiles/Details/5
        public ActionResult Details(int id)
        {
            UserProfile user = _userRepo.GetUserProfileById(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: UserProfilesController/Create
        public ActionResult Register()
        {
            var registration = new Registration();
            registration.Students = _studentRepo.GetAllStudents();
            return View(registration);
        }

        // POST: UserProfilesController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
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
            _userProfileRepository.AddUserProfile(newUserProfile);



            return RedirectToAction("Index", "Home");
        }

        // GET: UserProfilesProfilesController/Edit/5
        public ActionResult Edit(int id)
        {
            UserProfile user = _userRepo.GetUserProfileById(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UserProfilesController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserProfile user)
        {
            try
            {
                _userRepo.UpdateUserProfile(user);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(user);
            }
        }

        // GET: UserProfilesController/Delete/5
        public ActionResult Delete(int id)
        {
            UserProfile user = _userRepo.GetUserProfileById(id);

            return View(user);
        }

        // POST: UserProfilesController/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id, UserProfile user)
        {
            try
            {
                _userRepo.DeleteUserProfile(id);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(user);
            }
        }
    }
}