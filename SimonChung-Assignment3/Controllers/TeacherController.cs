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
            Teacher SelectedTeacher = controller.FindTeacher(id);
           
            return View(SelectedTeacher); 
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

        //GET:  /Teacher/Update/{id}
        /// <summary>
        /// Routes to the Update page and gathers information from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A dynamic Update webpage which provides the current information about a teacher and asks the user
        /// for new information through a form</returns>
        /// <example> GET : /Teacher/Update/{id}</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }


        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the school with new values. This information
        /// is sent through the API and redirects to Show.cshtml for the updated teacher
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Teacherfname"></param>
        /// <param name="Teacherlname"></param>
        /// <param name="Hiredate"></param>
        /// <param name="Employeenumber"></param>
        /// <param name="Salary"></param>
        /// <returns>Dynamic webpag which provides the current information about the teacher.</returns>
        /// <example>
        /// POST:   /Teacher/Update/3
        /// Form Data
        /// {
        /// "Teachaerfname":"Theophilus",
        /// "Teachaerlname":"Pythagoras",
        /// "HireDate":"2021-01-13",
        /// "Employeenumber":"T007",
        /// "Salary":34.88
        /// }
        /// </example>
        //POST :  /Teacher/Update/{id}

        [HttpPost]
        public ActionResult Update(int id, string Teacherfname, string Teacherlname, DateTime Hiredate, string Employeenumber, decimal Salary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.Teacherfname = Teacherfname;
            TeacherInfo.Teacherlname = Teacherlname;
            TeacherInfo.Hiredate = Hiredate;
            TeacherInfo.Employeenumber = Employeenumber;
            TeacherInfo.Salary = Salary;



            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }

    }
}