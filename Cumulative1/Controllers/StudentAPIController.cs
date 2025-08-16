using Microsoft.AspNetCore.Mvc;
using Cumulative1.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace Cumulative1.Controllers
{
    [Route(template: "api/Student")]
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
        // Method that returns a list of students
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
        // Method that return a specific student
        public Student FindStudent(int studentId)
        {
            Student Student = new Student();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                string Query = "select * from students where studentid = @key"; // Search query string

                MySqlCommand Command = Connection.CreateCommand();

                Command.Parameters.AddWithValue("@key", studentId); // Sanitize input to be used for the query string

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
        [HttpPost(template: "AddStudent")] // Route to Add student method
        public int AddStudent([FromBody] Student StudentData) // Created a method to add a student and returns student id
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open(); // open connection to database

                // Query string for inserting new student
                string query = "INSERT INTO students(studentfname, studentlname, studentnumber, enroldate) VALUE(@firstname, @lastname, @studentnumber, @enroldate)";

                // Created object to be sent to database
                MySqlCommand Command = Connection.CreateCommand();
                // Sanitize parameter value to be added to query string
                Command.Parameters.AddWithValue("@firstname", StudentData.StudentFirstName);
                Command.Parameters.AddWithValue("@lastname", StudentData.StudentLastName);
                Command.Parameters.AddWithValue("@studentnumber", StudentData.StudentNumber);
                Command.Parameters.AddWithValue("@enroldate", StudentData.StudentEnrolDate);

                Command.CommandText = query;

                // Execute Query
                Command.ExecuteNonQuery();
                return Convert.ToInt32(Command.LastInsertedId); // Return inserted student id as a number
            }
            return 0; // return 0 if failed to create
        }
        [HttpDelete(template: "DeleteStudent")]
        // Created a method to delete a specific student record
        public int DeleteStudent(int studentId)
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                string query = "Delete From students where studentid = @id"; // query string

                MySqlCommand Command = Connection.CreateCommand();
                Command.Parameters.AddWithValue("@id", studentId); // Sanitize input value

                Command.CommandText = query;

                return Command.ExecuteNonQuery(); // Execute Query Search
            }
            return 0;
        }

        [HttpPut(template: "updatestudent")]
        // Declared a method that updates a student record
        public Student UpdateStudent(int id, [FromBody] Student studentData)
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                // Query string
                string query = "UPDATE students SET studentfname=@firstname, studentlname=@lastname, studentnumber=@studentnumber, enroldate=@enroldate WHERE studentid=@id";

                MySqlCommand Command = Connection.CreateCommand();

                // Sanitize the input values
                Command.Parameters.AddWithValue("@id", studentData.StudentId);
                Command.Parameters.AddWithValue("@firstname", studentData.StudentFirstName);
                Command.Parameters.AddWithValue("@lastname", studentData.StudentLastName);
                Command.Parameters.AddWithValue("@studentnumber", studentData.StudentNumber);
                Command.Parameters.AddWithValue("@enroldate", studentData.StudentEnrolDate);

                Command.ExecuteNonQuery(); // Execute query
            }
            return FindStudent(id); // Return student record
        }
    }
}