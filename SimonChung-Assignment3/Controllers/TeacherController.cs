using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimonChung_Assignment3.Models;


namespace SimonChung_Assignment3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET : Teachers/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id) 
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
           
            return View(NewTeacher); 
        }

        //GET: /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }

        //POST: /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET: /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //Post: /Author/Create
        [HttpPost]
        public ActionResult Create(string Teacherfname, string Teacherlname, DateTime Hiredate, string Employeenumber, decimal Salary)
        {
            //Identify that this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have accessed the Create Method");
            Debug.WriteLine(Teacherfname);
            Debug.WriteLine(Teacherlname);
            

            Teacher NewTeacher = new Teacher();
            NewTeacher.Teacherfname = Teacherfname;
            NewTeacher.Teacherlname = Teacherlname;
            NewTeacher.Hiredate = Hiredate;
            NewTeacher.Employeenumber= Employeenumber;
            NewTeacher.Salary = Salary;

            

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("list");
        }

    }
}