using Microsoft.AspNetCore.Mvc;
using Cumulative1.Models;
using MySql.Data.MySqlClient;
using System.Security.Cryptography.X509Certificates;

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
        public List<Course> ListCourses() // Method to return a list of courses
        {
            List<Course> Courses = new List<Course>(); // Create an instance of List of courses

            // Connects with Database and closes the connection after retrieving a record
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                MySqlCommand Command = Connection.CreateCommand();

                string query = "select * from courses"; // string query

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
        [HttpGet(template: "ListCoursesForTeacher")]
        public List<Course> ListCoursesForTeacher(int teacherid)
        {
            List<Course> Courses = new List<Course>();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open(); // Connects to the Database

                // Declared Query variable to retrieve a record in courses table that has a specific teacher id
                string Query = $"Select * from courses where teacherid = @key";

                MySqlCommand Command = Connection.CreateCommand(); // Call the Create method to initiate the search Query

                Command.Parameters.AddWithValue("@key", teacherid);
                Command.Prepare();

                Command.CommandText = Query; // Set the Sql Statement

                // Send a query search request to the database to search a query
                using (MySqlDataReader ReaderResult = Command.ExecuteReader())
                {

                    // Loop Through Query until record returns false
                    while (ReaderResult.Read())
                    {
                        Course Course = new Course();

                        // Stores values into Course
                        Course.CourseId = Convert.ToInt32(ReaderResult["courseid"]);
                        Course.CourseCode = ReaderResult["coursecode"].ToString();
                        Course.CourseTeacherId = Convert.ToInt32(ReaderResult["teacherid"]);
                        Course.CourseStartDate = Convert.ToDateTime(ReaderResult["startdate"]);
                        Course.CourseFinishDate = Convert.ToDateTime(ReaderResult["finishdate"]);
                        Course.CourseName = ReaderResult["coursename"].ToString();

                        Courses.Add(Course);
                    }

                }
                return Courses;
            }
        }
        [HttpPost]
        public int AddCourse([FromBody] Course CourseData)
        {
            // Connection will close after executing the insert
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                // String query
                string query = "INSERT INTO courses(coursecode, teacherid, startdate, finishdate, coursename) VALUE(@coursecode, @teacherid, @startdate, @finishdate, @coursename)";

                MySqlCommand Command = Connection.CreateCommand();

                // Sanitize data to be used in the string query
                Command.Parameters.AddWithValue("@coursecode", CourseData.CourseCode);
                Command.Parameters.AddWithValue("@teacherid", CourseData.CourseTeacherId);
                Command.Parameters.AddWithValue("@startdate", CourseData.CourseStartDate);
                Command.Parameters.AddWithValue("@finishdate", CourseData.CourseFinishDate);
                Command.Parameters.AddWithValue("@coursename", CourseData.CourseName);

                Command.CommandText = query;

                Command.ExecuteNonQuery();
                return Convert.ToInt32(Command.LastInsertedId); // return the last inserted course id
            }
            return 0;
        }
        [HttpDelete(template: "DeleteCourse")]
        // Declared a method that delete a specific course record with course id
        public int DeleteCourse(int courseid)
        {
            using (MySqlConnection connection = _context.AccessDatabase())
            {
                connection.Open();

                string query = "Delete From Courses Where courseid = @id"; // query string

                MySqlCommand Command = connection.CreateCommand();
                // Sanitize course id value
                Command.Parameters.AddWithValue("@id", courseid);

                Command.CommandText = query;

                return Command.ExecuteNonQuery(); // returns the rows that has been deleted
            }
            return 0;
        }
    
    }
}