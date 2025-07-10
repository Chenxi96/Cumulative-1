using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Cumulative1.Models;


namespace Cumulative1.Controllers
{
    [Route(template: "api/Teacher")]
    [ApiController]

    public class TeacherAPIController : ControllerBase
    {
        private SchoolDbContext School = new SchoolDbContext();

        [HttpGet(template: "ListOfTeacher")]

        public List<string> ListOfTeacher()
        {
            List<string> Teachers = new List<string>();
            using (MySqlConnection Connection = School.AccessDatabase())
            {
                Connection.Open();

                MySqlCommand Command = Connection.CreateCommand();

                Command.CommandText = "SELECT * FROM teachers";

                using (MySqlDataReader ReaderResult = Command.ExecuteReader())
                {
                    while (ReaderResult.Read())
                    {
                        string FirstName = ReaderResult["teacherfname"].ToString();
                        string LastName = ReaderResult["teacherlname"].ToString();
                        string EmployeeNumber = ReaderResult["employeenumber"].ToString();

                        Teachers.Add(FirstName);
                        Teachers.Add(LastName);
                        Teachers.Add(EmployeeNumber);
                    }
                }

            }
            
            return Teachers;
        }

    }
}