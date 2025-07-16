

namespace Cumulative1.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; } // Teacher id

        public string TeacherFirstName { get; set; } // Teacher first name as a string

        public string TeacherLastName { get; set; } // Teacher last name as a string

        public string TeacherEmployeeNumber { get; set; } // Teacher employee number as a string

        public DateTime TeacherHiredDate { get; set; } // Teacher hire date as a date time

        public decimal TeacherSalary { get; set; } // Teacher Salary as a demcimal
    }
}