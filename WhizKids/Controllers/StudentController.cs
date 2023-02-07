using Microsoft.AspNetCore.Mvc;
using WhizKids.Repositories;
using System.Collections.Generic;
using WhizKids.Models;
using System;

namespace WhizKids.Controllers
{
    public class StudentController : Controller
    {

        private readonly IStudentRepository _studentRepo;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepo = studentRepository;
        }

        // GET: Students
        public ActionResult Index()
        {
            List<Student> students = _studentRepo.GetAllStudents();

            return View(students);
        }

        // GET:Students/Details/5
        public ActionResult Details(int id)
        {
            Student student = _studentRepo.GetStudentById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
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