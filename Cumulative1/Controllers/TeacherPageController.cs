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

        // Declared a function that shows a list of teachers page
        public IActionResult List(DateTime? startDate, DateTime? endDate)
        {
            // Declared a variable named ListOfTeachers that holds a list of Teacher objects.
            List<Teacher> ListOfTeachers = _teacherApi.ListTeachers(startDate, endDate);
            return View(ListOfTeachers); // returns a view object with the ListOfTeachers
        }

        // Directs to show teacher with specific id page
        public IActionResult Show(int TeacherId)
        {
            // Declared a variable named ListOfTeachers that holds a list of Teacher objects.
            Teacher Teacher = _teacherApi.FindTeacher(TeacherId); //Get Teacher method with TeacherId as parameter

            List<Course> Courses = _courseApi.ListCoursesForTeacher(TeacherId); // Get a list of teacher's courses

            TeacherCourses TeacherCourses = new TeacherCourses(); //Create a instance of TeacherCourses

            // Add Teacher and Courses into TeacherCourses
            TeacherCourses.Teacher = Teacher;
            TeacherCourses.Courses = Courses;

            return View(TeacherCourses); // returns a view object with the TeacherCourses object
        }

        // Declared a method that directs to create teacher form page
        public IActionResult New()
        {
            return View();
        }

        // Declared a method that creates the teacher record then redirects to the teacher show page
        public IActionResult Create(Teacher NewTeacher)
        {
            int id = _teacherApi.AddTeacher(NewTeacher);

            return RedirectToAction("Show", new { teacherid = id });
        }

        // Declared a method that confirms deleting teacher page
        public IActionResult DeleteConfirm(int teacherId)
        {
            Teacher teacher = _teacherApi.FindTeacher(teacherId);
            return View(teacher);
        }

        // Declared a method that delete the selected teacher page
        public IActionResult Delete(int teacherId)
        {
            _teacherApi.DeleteTeacher(teacherId); // Deletes specific teacher with teacher id
            return RedirectToAction("list"); // Redirects to list of teacher page
        }

        // Declared a method that edits a specific teacher record
        public IActionResult Edit(int id)
        {
            Teacher Teacher = _teacherApi.FindTeacher(id); // Find a specific teacher record

            return View(Teacher); // Returns a view page with the specific returned teacher record
        }

        // Declared a method that updates the edited teacher record
        public IActionResult Update(int id, Teacher teacherData)
        {          
            _teacherApi.UpdateTeacher(id, teacherData); // Updates the edited teacher record

            return RedirectToAction("Show", new { teacherid = id }); // Redirects to the show page to the updated teacher record
        }
    }
}