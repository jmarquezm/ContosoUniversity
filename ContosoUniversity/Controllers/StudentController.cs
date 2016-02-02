using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using System.Net;
using System.Data.Entity.Infrastructure;

namespace ContosoUniversity.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Student()
        {
            var db=new SchoolContext();
            var StudentList = (from student in db.Students
                               select student).ToList();
            return View(StudentList);
        }

        public ActionResult Detail(int? id)
        {
            var db = new SchoolContext();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
            
        }
        [HttpGet]
        public ActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStudent(Student model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var db = new SchoolContext();
                    db.Students.Add(model);
                    db.SaveChanges();
                    TempData["success"] = "true";
                    TempData["message"] = "The Student has been added successfully";
                    return RedirectToAction("Student");
                }
            }
            catch (RetryLimitExceededException ex)
            {
                TempData["error"] = "true";
                TempData["message"] = "An error has ocurred while added the Student";
                return RedirectToAction("Student");
            }

            return View();
        }

        [HttpGet]
        public ActionResult EditStudent(int? id)
        {
            var db=new SchoolContext();
            Student student = db.Students.Find(id);
            return View(student);
        }

        [HttpPost]
        public ActionResult EditStudent(Student student)
        {

            try
            {
                var db = new SchoolContext();
                db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "true";
                TempData["message"] = "The student has been updated";
                return RedirectToAction("Student");
            }
            catch (Exception ex)
            {
                TempData["error"] = "true";
                TempData["message"] = "An error has ocurred while update the student";
                return RedirectToAction("Student");
            }
        }

        [HttpPost]
        public JsonResult DeleteStudent(int? id)
        {
            try
            {
                var db = new SchoolContext();
                Student student = db.Students.Find(id);
                db.Students.Remove(student);
                db.SaveChanges();
                return Json(new { result = "ok", message = "The Student has been deleted" });
            }
            catch (Exception ex)
            {
                return Json(new { result = "error", message = "An error has ocurred while delete student" });
            }
        }
    }
}
