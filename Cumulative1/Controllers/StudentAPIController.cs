using Microsoft.AspNetCore.Mvc;
using Cumulative1.Models;
using MySql.Data.MySqlClient;

namespace Cumulative1.Controllers
{
    [Route(template: "api/Students")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {

        private readonly SchoolDbContext _context;

        public StudentAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        [HttpGet(template: "getStudent")]
        public List<Student> GetStudents()
        {
            List<Student> Students = new List<Student>();


            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                MySqlCommand Command = Connection.CreateCommand();

                string Query = "Select * from students";

                Command.CommandText = Query;

                using (MySqlDataReader DataResult = Command.ExecuteReader())
                {
                    while (DataResult.Read())
                    {
                        Student Student = new Student();

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
    }
}