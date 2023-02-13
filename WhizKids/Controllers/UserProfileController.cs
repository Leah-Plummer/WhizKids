using Microsoft.AspNetCore.Mvc;
using WhizKids.Repositories;
using System.Collections.Generic;
using WhizKids.Models;
using System;

namespace WhizKids.Controllers
{
    public class UserProfileController : Controller
    {

        private readonly IUserProfileRepository _userRepo;
        private readonly IStudentRepository _studentRepo;
        public UserProfileController(IUserProfileRepository userRepository, IStudentRepository studentRepository)
        {
            _userRepo = userRepository;
            _studentRepo = studentRepository;
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserProfilesController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(UserProfile user)
        {
            try
            {
                _userRepo.AddUserProfile(user);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(user);
            }
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