using Microsoft.AspNetCore.Mvc;
using Cumulative1.Models;
using MySql.Data.MySqlClient;

namespace Cumulative1.Controllers
{
    [Route(template: "api/Course")]
    [ApiController]
    public class CourseAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public CourseAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        [HttpGet(template: "FindCourse")]

        public Course FindCourse(string coursecode)
        {
            Course Course = new Course();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                string Query = $"Select * from courses where coursecode = '{coursecode}'";

                MySqlCommand Command = Connection.CreateCommand();

                Command.CommandText = Query;

                using (MySqlDataReader ReaderResult = Command.ExecuteReader())
                {
                    while (ReaderResult.Read())
                    {
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