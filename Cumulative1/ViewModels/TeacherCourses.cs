using Cumulative1.Models;

namespace Cumulative1.ViewModels
{
    public class TeacherCourses
    {
        public Teacher Teacher { get; set; } // Declared field as Teacher with the Teacher class type 

        public List<Course> Courses { get; set; } // Declared field Courses with list of course class type 
    }
}