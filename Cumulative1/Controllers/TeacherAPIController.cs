using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Cumulative1.Models;
using Cumulative1.ViewModels;

namespace Cumulative1.Controllers
{
    // Created a route to "api/Teacher"
    [Route(template: "api/Teacher")]

    [ApiController]
    public class TeacherAPIController : ControllerBase // Created a class without view support
    {
        private readonly SchoolDbContext _context; // Declared a field named _context that has a SchoolDbContext data type which can only be accessed inside this class and assigned once

        // Inject Dependency of SchoolDbContext to _context variable
        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        // Define a route to "GetAllTeachers" which is a get method
        [HttpGet(template: "ListTeachers")]

        /// <summary>
        ///  Receives a HTTP GET Request that returns List of Teachers object from the database
        /// </summary>
        /// <example>
        /// Get: api/Teacher/ListTeachers -> 
        /// [{"teacherId":1,"teacherFirstName":"Alexander","teacherLastName":"Bennett","teacherEmployeeNumber":"T378","teacherHiredDate":"2016-08-05T00:00:00","teacherSalary":55},
        /// {"teacherId":2,"teacherFirstName":"Caitlin","teacherLastName":"Cummings","teacherEmployeeNumber":"T381","teacherHiredDate":"2014-06-10T00:00:00","teacherSalary":63},
        /// {"teacherId":3,"teacherFirstName":"Linda","teacherLastName":"Chan","teacherEmployeeNumber":"T382","teacherHiredDate":"2015-08-22T00:00:00","teacherSalary":60},
        /// {"teacherId":4,"teacherFirstName":"Lauren","teacherLastName":"Smith","teacherEmployeeNumber":"T385","teacherHiredDate":"2014-06-22T00:00:00","teacherSalary":74},
        /// {"teacherId":5,"teacherFirstName":"Jessica","teacherLastName":"Morris","teacherEmployeeNumber":"T389","teacherHiredDate":"2012-06-04T00:00:00","teacherSalary":49},
        /// {"teacherId":6,"teacherFirstName":"Thomas","teacherLastName":"Hawkins","teacherEmployeeNumber":"T393","teacherHiredDate":"2016-08-10T00:00:00","teacherSalary":54},
        /// {"teacherId":7,"teacherFirstName":"Shannon","teacherLastName":"Barton","teacherEmployeeNumber":"T397","teacherHiredDate":"2013-08-04T00:00:00","teacherSalary":65},
        /// {"teacherId":8,"teacherFirstName":"Dana","teacherLastName":"Ford","teacherEmployeeNumber":"T401","teacherHiredDate":"2014-06-26T00:00:00","teacherSalary":71},
        /// {"teacherId":9,"teacherFirstName":"Cody","teacherLastName":"Holland","teacherEmployeeNumber":"T403","teacherHiredDate":"2016-06-13T00:00:00","teacherSalary":43},
        /// {"teacherId":10,"teacherFirstName":"John","teacherLastName":"Taram","teacherEmployeeNumber":"T505","teacherHiredDate":"2015-10-23T00:00:00","teacherSalary":80}]
        /// </example>
        /// <returns>A List of teacher objects</returns>

        // Declared a function that returns a List with Teacher as the generic type parameter
        public List<Teacher> ListTeachers()
        {
            // Declared a variable that returns a List with Teacher as generic parameter type
            List<Teacher> Teacher = new List<Teacher>();

            // Creates a connection to the database and dispose connection when code is finished.
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open(); // Connects to the Database

                MySqlCommand Command = Connection.CreateCommand(); // Call the Create method to initiate the search Query

                string Query = "SELECT * FROM teachers"; // search query variable

                Command.CommandText = Query; // Set the Sql Statement

                using (MySqlDataReader ReaderResult = Command.ExecuteReader()) // Send a query search request to the database to search a query
                {
                    // Loop Through Query until record returns false
                    while (ReaderResult.Read())
                    {
                        Teacher InfoOfTeacher = new Teacher(); // Declare InfoOfTeacher and initialize with a new Teacher instance class

                        // Set the InfoOfTeacher properties
                        InfoOfTeacher.TeacherId = Convert.ToInt32(ReaderResult["teacherid"]);
                        InfoOfTeacher.TeacherFirstName = ReaderResult["teacherfname"].ToString();
                        InfoOfTeacher.TeacherLastName = ReaderResult["teacherlname"].ToString();
                        InfoOfTeacher.TeacherEmployeeNumber = ReaderResult["employeenumber"].ToString();
                        InfoOfTeacher.TeacherHiredDate = Convert.ToDateTime(ReaderResult["hiredate"].ToString());
                        InfoOfTeacher.TeacherSalary = Convert.ToInt32(ReaderResult["salary"]);

                        Teacher.Add(InfoOfTeacher); // Add to InfoOfTeacher to Teacher
                    }
                }

            }

            return Teacher;
        }

        // Define a route to "GetAllTeachers" which is a get method
        [HttpGet(template: "FindTeacher")]

        

        /// <summary>
        /// Receives a HTTP GET Request and return a Teacher object from the database
        /// </summary>
        /// <param name="teacherid">Teacher's Id</param>
        /// <example>
        /// GET : api/Teacher/FindTeacher?teacherid=1 ->
        /// {"teacherId":1,"teacherFirstName":"Alexander",
        /// "teacherLastName":"Bennett",
        /// "teacherEmployeeNumber":"T378",
        /// "teacherHiredDate":"2016-08-05T00:00:00",
        /// "teacherSalary":55.30}
        /// </example>
        /// <returns>A Teacher object</returns>
        public TeacherCourses FindTeacher(int teacherid) // Declared a function that returns a data type of Teacher and has teacherid as a number parameter
        {

            // Declare Teacher variable with the data type of Teacher and initialize with a Teacher class
            Teacher Teacher = new Teacher();

            List<Course> Courses = new List<Course>();


            // Creates a connection to the database and dispose connection when code is finished.
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open(); // Connects to the Database

                // Declared Query variable that retrieves a record in the teachers table that has a specific teacher id
                string Query = $"Select * from teachers where teacherid = {teacherid}";

                MySqlCommand Command = Connection.CreateCommand(); // Call the Create method to initiate the search Query

                Command.CommandText = Query; // Set the Sql Statement

                // Send a query search request to the database to search a query
                using (MySqlDataReader ReaderResult = Command.ExecuteReader())
                {

                    // Loop Through Query until record returns false
                    while (ReaderResult.Read())
                    {
                        // Set the Teacher properties
                        Teacher.TeacherId = Convert.ToInt32(ReaderResult["teacherid"]);
                        Teacher.TeacherFirstName = ReaderResult["teacherfname"].ToString();
                        Teacher.TeacherLastName = ReaderResult["teacherlname"].ToString();
                        Teacher.TeacherEmployeeNumber = ReaderResult["employeenumber"].ToString();
                        Teacher.TeacherHiredDate = Convert.ToDateTime(ReaderResult["hiredate"]);
                        Teacher.TeacherSalary = Convert.ToDecimal(ReaderResult["salary"]);
                    }
                }
            }

            

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open(); // Connects to the Database

                // Declared Query variable to retrieve a record in courses table that has a specific teacher id
                string Query = $"Select * from courses where teacherid = {teacherid}";

                MySqlCommand Command = Connection.CreateCommand(); // Call the Create method to initiate the search Query

                Command.CommandText = Query; // Set the Sql Statement

                // Send a query search request to the database to search a query
                using (MySqlDataReader ReaderResult = Command.ExecuteReader())
                {

                    // Loop Through Query until record returns false
                    while (ReaderResult.Read())
                    {
                        Course Course = new Course();

                        Course.CourseId = Convert.ToInt32(ReaderResult["courseid"]);
                        Course.CourseCode = ReaderResult["coursecode"].ToString();
                        Course.CourseTeacherId = Convert.ToInt32(ReaderResult["teacherid"]);
                        Course.CourseStartDate = Convert.ToDateTime(ReaderResult["startdate"]);
                        Course.CourseFinishDate = Convert.ToDateTime(ReaderResult["finishdate"]);
                        Course.CourseName = ReaderResult["coursename"].ToString();

                        Courses.Add(Course);
                    }
                }
            }

            TeacherCourses teacherWithCourses = new TeacherCourses();
            teacherWithCourses.Courses = Courses;
            teacherWithCourses.Teacher = Teacher;
            return teacherWithCourses;
        }

    }
}