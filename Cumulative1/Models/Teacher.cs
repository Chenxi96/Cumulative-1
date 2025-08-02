namespace Cumulative1.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; } // Field to store teacher id as a number

        public string TeacherFirstName { get; set; } // Field to store teacher first name as a string

        public string TeacherLastName { get; set; } // Field to store teacher last name as a string

        public string TeacherEmployeeNumber { get; set; } // Field to store teacher employee number as a string

        public DateTime TeacherHiredDate { get; set; } // Field to store teacher hired date as a date time

        public decimal TeacherSalary { get; set; } // Field to store teacher salary as a decimal type

        public string TeacherWorkPhone { get; set; } // Field to store teacher's work phone number

        public string TeacherError { get; set; } // Field to store no Teacher records error
    }
}