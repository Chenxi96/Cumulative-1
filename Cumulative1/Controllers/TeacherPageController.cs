using Cumulative1.Models;
using Microsoft.AspNetCore.Mvc;


namespace Cumulative1.Controllers
{
    public class TeacherController : Controller
    {
        private readonly TeacherAPIController _api;

        public TeacherController(TeacherAPIController api)
        {
            _api = api;
        }

        public IActionResult List()
        {
            List<Teacher> ListOfTeachers = _api.GetAllTeachers();
            return View(ListOfTeachers);
        }

        public IActionResult Show(int TeacherId)
        {
            Teacher Teacher = _api.GetTeacher(TeacherId);
            return View(Teacher);
        }
    }
}