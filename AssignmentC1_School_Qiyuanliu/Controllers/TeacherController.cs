using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssignmentC1_School_Qiyuanliu.Models;
using System.Diagnostics;

namespace AssignmentC1_School_Qiyuanliu.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: / Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);

        }

        //Get :/Teacher/Show/{id}

        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);

        }



        //GET : /Author/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewAuthor = controller.FindTeacher(id);


            return View(NewAuthor);
        }


        //POST : /Author/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : Teacher/New
        [HttpGet]
        [Route("Teacher/New")]

        public ActionResult New()
        {
            return View();

        }


        //POST : Teacher/Create
        [HttpPost]
        [Route("Teacher/Create")]

        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, decimal Salary)
        {
            Debug.WriteLine("You are trying to add a teacher with name:" + TeacherFname + TeacherLname+"and the employeen number is "+ EmployeeNumber +"; salary is "+ Salary);


            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();

            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }



    }
}