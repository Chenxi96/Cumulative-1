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
        /// <param name="startDate">Start date for the filter</param>
        /// <param name="endDate">End date for the filter</param>
        /// <example>
        /// GET : api/Teacher/ListTeachers --> 
        /// [{"teacherId":1,"teacherFirstName":"Alexander","teacherLastName":"Bennett","teacherEmployeeNumber":"T378","teacherHiredDate":"2016-08-05T00:00:00","teacherSalary":55,"teacherError":null},
        /// {"teacherId":2,"teacherFirstName":"Caitlin","teacherLastName":"Cummings","teacherEmployeeNumber":"T381","teacherHiredDate":"2014-06-10T00:00:00","teacherSalary":63,"teacherError":null},
        /// {"teacherId":3,"teacherFirstName":"Linda","teacherLastName":"Chan","teacherEmployeeNumber":"T382","teacherHiredDate":"2015-08-22T00:00:00","teacherSalary":60,"teacherError":null},
        /// {"teacherId":4,"teacherFirstName":"Lauren","teacherLastName":"Smith","teacherEmployeeNumber":"T385","teacherHiredDate":"2014-06-22T00:00:00","teacherSalary":74,"teacherError":null},
        /// {"teacherId":5,"teacherFirstName":"Jessica","teacherLastName":"Morris","teacherEmployeeNumber":"T389","teacherHiredDate":"2012-06-04T00:00:00","teacherSalary":49,"teacherError":null},
        /// {"teacherId":6,"teacherFirstName":"Thomas","teacherLastName":"Hawkins","teacherEmployeeNumber":"T393","teacherHiredDate":"2016-08-10T00:00:00","teacherSalary":54,"teacherError":null},
        /// {"teacherId":7,"teacherFirstName":"Shannon","teacherLastName":"Barton","teacherEmployeeNumber":"T397","teacherHiredDate":"2013-08-04T00:00:00","teacherSalary":65,"teacherError":null},
        /// {"teacherId":8,"teacherFirstName":"Dana","teacherLastName":"Ford","teacherEmployeeNumber":"T401","teacherHiredDate":"2014-06-26T00:00:00","teacherSalary":71,"teacherError":null},
        /// {"teacherId":9,"teacherFirstName":"Cody","teacherLastName":"Holland","teacherEmployeeNumber":"T403","teacherHiredDate":"2016-06-13T00:00:00","teacherSalary":43,"teacherError":null},
        /// {"teacherId":10,"teacherFirstName":"John","teacherLastName":"Taram","teacherEmployeeNumber":"T505","teacherHiredDate":"2015-10-23T00:00:00","teacherSalary":80,"teacherError":null}]
        /// </example>
        /// <example>
        /// GET : api/Teacher/ListTeachers?startdate=2013-05-18&enddate=2014-08-18 -->
        /// [{"teacherId":2,"teacherFirstName":"Caitlin","teacherLastName":"Cummings","teacherEmployeeNumber":"T381","teacherHiredDate":"2014-06-10T00:00:00","teacherSalary":63,"teacherError":null},
        /// {"teacherId":4,"teacherFirstName":"Lauren","teacherLastName":"Smith","teacherEmployeeNumber":"T385","teacherHiredDate":"2014-06-22T00:00:00","teacherSalary":74,"teacherError":null},
        /// {"teacherId":7,"teacherFirstName":"Shannon","teacherLastName":"Barton","teacherEmployeeNumber":"T397","teacherHiredDate":"2013-08-04T00:00:00","teacherSalary":65,"teacherError":null},
        /// {"teacherId":8,"teacherFirstName":"Dana","teacherLastName":"Ford","teacherEmployeeNumber":"T401","teacherHiredDate":"2014-06-26T00:00:00","teacherSalary":71,"teacherError":null}]
        /// </example>
        /// <returns>A List of teacher objects</returns>

        // Declared a function that returns a List with Teacher as the generic type parameter
        public List<Teacher> ListTeachers(DateTime ?startDate, DateTime ?endDate)
        {
            // Declared a variable that returns a List with Teacher as generic parameter type
            List<Teacher> Teacher = new List<Teacher>();

            // Creates a connection to the database and dispose connection when code is finished.
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open(); // Connects to the Database

                MySqlCommand Command = Connection.CreateCommand(); // Call the Create method to initiate the search Query

                string Query = "SELECT * FROM teachers"; // search query variable

                if (startDate != null && endDate != null)
                {
                    Query += " where hiredate between @startfilter and @endfilter";
                    Command.Parameters.AddWithValue("@startfilter", startDate);
                    Command.Parameters.AddWithValue("@endfilter", endDate);
                }

                Command.CommandText = Query; // Set the Sql Statement
                Command.Prepare();

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
        /// GET : api/Teacher/FindTeacher?teacherid=2 -->
        /// {"teacher":{"teacherId":2,"teacherFirstName":"Caitlin","teacherLastName":"Cummings","teacherEmployeeNumber":"T381","teacherHiredDate":"2014-06-10T00:00:00","teacherSalary":62.77,"teacherError":null},
        /// "courses":[{"courseId":2,"courseCode":"http5102","courseTeacherId":2,"courseStartDate":"2018-09-04T00:00:00","courseFinishDate":"2018-12-14T00:00:00","courseName":"Project Management"},
        /// {"courseId":6,"courseCode":"http5201","courseTeacherId":2,"courseStartDate":"2019-01-08T00:00:00","courseFinishDate":"2019-04-27T00:00:00","courseName":"Security & Quality Assurance"}]}
        /// </example>
        /// <example>
        /// GET : api/Teacher/FindTeacher?teacherid=0 -->
        /// {"teacher":{"teacherId":0,"teacherFirstName":null,"teacherLastName":null,"teacherEmployeeNumber":null,"teacherHiredDate":"0001-01-01T00:00:00","teacherSalary":0,"teacherError":"No Teacher Record Found"},
        /// "courses":[]}
        /// </example>
        /// <example>
        /// GET : api/Teacher/FindTeacher?teacherid=-8 -->
        /// {"teacher":{"teacherId":0,"teacherFirstName":null,"teacherLastName":null,"teacherEmployeeNumber":null,"teacherHiredDate":"0001-01-01T00:00:00","teacherSalary":0,"teacherError":"No Teacher Record Found"},
        /// "courses":[]}
        /// </example>
        /// <returns>A Teacher object</returns>
        public TeacherCourses FindTeacher(int teacherid) // Declared a function that returns a data type of Teacher and has teacherid as a number parameter
        {

            // Declare Teacher variable with the data type of Teacher and initialize with a Teacher class
            Teacher Teacher = new Teacher();

            List<Course> Courses = new List<Course>();

            TeacherCourses teacherWithCourses = new TeacherCourses();

            // Creates a connection to the database and dispose connection when code is finished.
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open(); // Connects to the Database

                // Declared Query variable that retrieves a record in the teachers table that has a specific teacher id
                string Query = $"Select * from teachers where teacherid = @key";


                MySqlCommand Command = Connection.CreateCommand(); // Call the Create method to initiate the search Query

                Command.Parameters.AddWithValue("@key", teacherid);
                Command.Prepare();

                Command.CommandText = Query; // Set the Sql Statement

                // Send a query search request to the database to search a query
                using (MySqlDataReader ReaderResult = Command.ExecuteReader())
                {
                    // If rows less than 1
                    if (!ReaderResult.HasRows)
                    {
                        Teacher.TeacherError = "No Teacher Record Found"; // Changed teacher error to "No Teacher Record Found"
                    }
                    else
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
            }

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
            }

            teacherWithCourses.Courses = Courses;
            teacherWithCourses.Teacher = Teacher;
            return teacherWithCourses;
        }

    }
}