using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Cumulative1.Models;


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
        [HttpGet(template: "GetAllTeachers")]

        // Declared a function that returns a List with Teacher as the generic type parameter
        public List<Teacher> GetAllTeachers()
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
        [HttpGet(template: "getTeacher")]

        // Declared a function that returns a data type of Teacher and has teacherid as a number parameter
        public Teacher GetTeacher(int teacherid)
        {

            // Declare Teacher variable with the data type of Teacher and initialize with a Teacher class
            Teacher Teacher = new Teacher(); 

            // Creates a connection to the database and dispose connection when code is finished.
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open(); // Connects to the Database

                // Declated Query variable 
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
            return Teacher;
        }

    }
}