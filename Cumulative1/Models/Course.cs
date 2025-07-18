namespace Cumulative1.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string CourseCode { get; set; }

        public long CourseTeacherId { get; set; }

        public DateTime CourseStartDate { get; set; }

        public DateTime CourseFinishDate { get;  set; }

        public string CourseName { get; set; }
    } 
}