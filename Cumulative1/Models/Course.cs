namespace Cumulative1.Models
{
    public class Course
    {
        // Created Field for course id
        public int CourseId { get; set; }

        // Created field for course code
        public string CourseCode { get; set; }

        // Created field for course teacher id
        public long CourseTeacherId { get; set; }

        // Created field for course start date
        public DateTime CourseStartDate { get; set; }
        
        // Created field for course finished date
        public DateTime CourseFinishDate { get; set; }

        // Created field for course name
        public string CourseName { get; set; }
    } 
}