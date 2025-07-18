using Cumulative1.Models;
using Cumulative1.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace Cumulative1.Controllers
{
    public class TeacherController : Controller
    {
        // private field that can only be used inside the class TeacherController and can only be initialized once
        private readonly TeacherAPIController _api;

        // set the _api value to api from TeacherAPIController
        public TeacherController(TeacherAPIController api)
        {
            _api = api;
        }

        // Declared a function called list
        public IActionResult List(DateTime ?startDate, DateTime ?endDate)
        {
            // Declared a variable named ListOfTeachers that holds a list of Teacher objects.
            List<Teacher> ListOfTeachers = _api.ListTeachers(startDate, endDate);
            return View(ListOfTeachers); // returns a view object with the ListOfTeachers
        }

        public IActionResult Show(int TeacherId)
        {
            // Declared a variable named ListOfTeachers that holds a list of Teacher objects.
            TeacherCourses Teacher = _api.FindTeacher(TeacherId); //getTeacher method with TeacherId as parameter
            return View(Teacher); // returns a view object with the ListOfTeachers
        }
    }
}