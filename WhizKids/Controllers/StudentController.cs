using Microsoft.AspNetCore.Mvc;
using WhizKids.Repositories;
using System.Collections.Generic;
using WhizKids.Models;
using System;
using WhizKids.Models.ViewModels;

namespace WhizKids.Controllers
{
    public class StudentController : Controller
    {

        private readonly IStudentRepository _studentRepo;
        private readonly IUserProfileRepository _userProfileRepo;
        private readonly IMessageRepository _messageRepo;
        public StudentController(IStudentRepository studentRepository, IUserProfileRepository userProfileRepository, IMessageRepository messageRepo)
        {
            _studentRepo = studentRepository;
            _userProfileRepo = userProfileRepository;
            _messageRepo = messageRepo;
        }

        // GET: Students
        public ActionResult Index()
        {
            List<Student> students = _studentRepo.GetAllStudents();

            return View(students);
        }

        public ActionResult Messages(int id)
        {
            List<Message> messages = _messageRepo.GetAllMessagesById(id);

            return View(messages);
        }

        // GET:Students/Details/5
        public ActionResult Details(int id)
        {
            
            Student student = _studentRepo.GetStudentById(id);
            List<UserProfile> userProfiles = _userProfileRepo.GetUserProfilesByStudentId(student.Id);

            if (student == null)
            {
                return NotFound();
            }

            StudentProfileViewModel spvm = new StudentProfileViewModel()
            {
                
                Student = student,
                UserProfiles = userProfiles,
            };

            return View(spvm);
        }

        // GET: StudentsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentsController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            try
            {
                _studentRepo.AddStudent(student);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(student);
            }
        }

        // GET: StudentsController/Edit/5
        public ActionResult Edit(int id)
        {
            Student student = _studentRepo.GetStudentById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: StudentsController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Student student)
        {
            try
            {
                _studentRepo.UpdateStudent(student);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(student);
            }
        }

        // GET: StudentsController/Delete/5
        public ActionResult Delete(int id)
        {
            Student student = _studentRepo.GetStudentById(id);

            return View(student);
        }

        // POST: StudentsController/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Student student)
        {
            try
            {
                _studentRepo.DeleteStudent(id);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(student);
            }
        }
    }
}