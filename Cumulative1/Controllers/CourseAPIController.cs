using Microsoft.AspNetCore.Mvc;
using Cumulative1.Models;
using MySql.Data.MySqlClient;

namespace Cumulative1.Controllers
{
    [Route(template: "api/Course")]
    [ApiController]
    public class CourseAPIController : ControllerBase
    {
        // a private field that can only be initialized once named _context
        private readonly SchoolDbContext _context;

        // A Constructor that sets a dependency injected SchoolDbContext into _context
        public CourseAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        [HttpGet(template: "FindCourse")]

        public Course FindCourse(string coursecode)
        {
            // Declare Students variable with a list and a type of course and initialized list with type course
            Course Course = new Course();

            // Creates a connection to the database and dispose connection when code is finished.
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open(); // Connects to the Database

                string Query = $"Select * from courses where coursecode = @key"; // The Query string

                MySqlCommand Command = Connection.CreateCommand(); // Call the Create method to initiate the search Query
                Command.Parameters.AddWithValue("@key", coursecode);
                Command.CommandText = Query; // Set the Sql Statement
                Command.Prepare();
                using (MySqlDataReader ReaderResult = Command.ExecuteReader())
                {
                    //loop through the rows and stops when Read() return false
                    while (ReaderResult.Read())
                    {
                        // Insert the values into the course object
                        Course.CourseId = Convert.ToInt32(ReaderResult["courseid"]);
                        Course.CourseCode = ReaderResult["coursecode"].ToString();
                        Course.CourseTeacherId = Convert.ToInt32(ReaderResult["teacherid"]);
                        Course.CourseStartDate = Convert.ToDateTime(ReaderResult["finishdate"]);
                        Course.CourseFinishDate = Convert.ToDateTime(ReaderResult["finishdate"]);
                        Course.CourseName = ReaderResult["coursename"].ToString();
                    }
                }
            }
            return Course;
        }

        [HttpGet(template: "listCourses")]

        public List<Course> ListCourses()
        {
            List<Course> Courses = new List<Course>();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                MySqlCommand Command = Connection.CreateCommand();

                string query = "select * from courses";

                using (MySqlDataReader ReaderResult = Command.ExecuteReader())
                {
                    while (ReaderResult.Read())
                    {
                        Course Course = new Course(); 

                        // Insert the values into the course object
                        Course.CourseId = Convert.ToInt32(ReaderResult["courseid"]);
                        Course.CourseCode = ReaderResult["coursecode"].ToString();
                        Course.CourseTeacherId = Convert.ToInt32(ReaderResult["teacherid"]);
                        Course.CourseStartDate = Convert.ToDateTime(ReaderResult["finishdate"]);
                        Course.CourseFinishDate = Convert.ToDateTime(ReaderResult["finishdate"]);
                        Course.CourseName = ReaderResult["coursename"].ToString();

                        Courses.Add(Course);
                    }
                }
            }
            return Courses;
        }
    }
}