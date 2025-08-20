using System.ComponentModel.DataAnnotations;

namespace Cumulative1.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; } // Field to store teacher id as a number

        [Required(ErrorMessage = "First name must not be empty!")] // Validate first name to ensure input field is not empty
        public string TeacherFirstName { get; set; } // Field to store teacher first name as a string

        [Required(ErrorMessage = "Last name not be empty!")] // Validate last name to ensure input field is not empty
        public string TeacherLastName { get; set; } // Field to store teacher last name as a string

        [RegularExpression(@"^T.*", ErrorMessage = "The employee number must start with T")] // Validate employee number to ensure employee starts with T
        public string TeacherEmployeeNumber { get; set; } // Field to store teacher employee number as a string

        public DateTime TeacherHiredDate { get; set; } // Field to store teacher hired date as a date time

        [Range(1, 9000000, ErrorMessage = "Value Must be between {1} and {2}.")] // Validate salary amount
        public decimal TeacherSalary { get; set; } // Field to store teacher salary as a decimal type

        public string TeacherWorkPhone { get; set; } // Field to store teacher's work phone number

        public string TeacherError { get; set; } // Field to store no Teacher records error
    }
}