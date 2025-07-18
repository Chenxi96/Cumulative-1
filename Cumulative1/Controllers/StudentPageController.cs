using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;


namespace Cumulative1.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentAPIController _api;

        public StudentController(StudentAPIController api)
        {
            _api = api;
        }

        public IActionResult List()
        {
            List<Student> ListStudent = _api.ListStudent();
            return View(ListStudent);
        }

        public IActionResult Show(int studentId)
        {
            Student Student = _api.FindStudent(studentId);

            return View(Student);
        }
    }
}