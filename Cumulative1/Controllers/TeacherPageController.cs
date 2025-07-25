using Cumulative1.Models;
using Cumulative1.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace Cumulative1.Controllers
{
    public class TeacherController : Controller
    {
        // private field that can only be used inside the class TeacherController and can only be initialized once
        private readonly TeacherAPIController _teacherApi;
        private readonly CourseAPIController _courseApi;

        // set the _api value to api from TeacherAPIController
        public TeacherController(TeacherAPIController teacherApi, CourseAPIController courseApi)
        {
            _teacherApi = teacherApi;
            _courseApi = courseApi;
        }

        // Declared a function called list
        public IActionResult List(DateTime ?startDate, DateTime ?endDate)
        {
            // Declared a variable named ListOfTeachers that holds a list of Teacher objects.
            List<Teacher> ListOfTeachers = _teacherApi.ListTeachers(startDate, endDate);
            return View(ListOfTeachers); // returns a view object with the ListOfTeachers
        }

        public IActionResult Show(int TeacherId)
        {
            // Declared a variable named ListOfTeachers that holds a list of Teacher objects.
            Teacher Teacher = _teacherApi.FindTeacher(TeacherId); //getTeacher method with TeacherId as parameter

            List<Course> Courses = _courseApi.ListCoursesForTeacher(TeacherId);

            TeacherCourses TeacherCourses = new TeacherCourses();

            TeacherCourses.Teacher = Teacher;
            TeacherCourses.Courses = Courses;

            return View(TeacherCourses); // returns a view object with the ListOfTeachers
        }
    }
}