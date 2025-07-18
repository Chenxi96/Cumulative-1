using Microsoft.AspNetCore.Mvc;
using Cumulative1.Models;
using MySql.Data.MySqlClient;

namespace Cumulative1.Controllers
{
    [Route(template: "api/Students")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        // a private field that can only be initialized once named _context
        private readonly SchoolDbContext _context;

        // A Constructor that sets a dependency injected SchoolDbContext into _context
        public StudentAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        [HttpGet(template: "ListStudent")]
        public List<Student> ListStudent()
        {
            // Declare Students variable with a list and a type of student and initialized list with type student
            List<Student> Students = new List<Student>();

            // Creates a connection to the database and dispose connection when code is finished.
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open(); // Connects to the Database

                MySqlCommand Command = Connection.CreateCommand(); // Call the Create method to initiate the search Query

                string Query = "Select * from students"; // Search query variable

                Command.CommandText = Query; // Set the Sql Statement

                using (MySqlDataReader DataResult = Command.ExecuteReader()) // Send a query search request to the database to search a query
                {
                    // Loop Through Query until record returns false
                    while (DataResult.Read())
                    {
                        Student Student = new Student(); // Declare InfoOfTeacher and initialize with a new Teacher instance class

                        Student.StudentId = Convert.ToInt32(DataResult["studentid"]);
                        Student.StudentFirstName = DataResult["studentfname"].ToString();
                        Student.StudentLastName = DataResult["studentlname"].ToString();
                        Student.StudentNumber = DataResult["studentnumber"].ToString();
                        Student.StudentEnrolDate = Convert.ToDateTime(DataResult["enroldate"]);

                        Students.Add(Student);
                    }
                }
            }
            return Students;
        }

        [HttpGet(template: "FindStudent")]

        public Student FindStudent(int studentId)
        {
            Student Student = new Student();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                string Query = "select * from students where studentid = @key"; // Search query string

                MySqlCommand Command = Connection.CreateCommand();

                Command.Parameters.AddWithValue("@key", studentId);

                Command.CommandText = Query;
                Command.Prepare();

                using (MySqlDataReader DataResult = Command.ExecuteReader())
                {
                    while (DataResult.Read())
                    {
                        // Store values into Student
                        Student.StudentId = Convert.ToInt32(DataResult["studentid"]);
                        Student.StudentFirstName = DataResult["studentfname"].ToString();
                        Student.StudentLastName = DataResult["studentlname"].ToString();
                        Student.StudentNumber = DataResult["studentnumber"].ToString();
                        Student.StudentEnrolDate = Convert.ToDateTime(DataResult["enroldate"]);

                    }
                }
            }
            return Student;
        }
    }
}