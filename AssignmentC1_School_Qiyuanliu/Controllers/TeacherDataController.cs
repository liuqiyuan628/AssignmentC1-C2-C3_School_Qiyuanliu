using AssignmentC1_School_Qiyuanliu.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;



namespace AssignmentC1_School_Qiyuanliu.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the teachers table of our school database.
        /// <summary>
        /// Returns a list of teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, '',teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of teacher Names
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = (string)ResultSet["teacherfname"].ToString();
                string TeacherLname = (string)ResultSet["teacherlname"].ToString();
                string EmployeeNumber = (string)ResultSet["employeenumber"].ToString();



                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;




                //Add the teacher Name to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher names
            return Teachers;
        }



        /// <summary>
        /// Find a teacher based on the teacher ID
        /// </summary>
        /// <param name="id">The id of the teacher</param>
        /// <returns>The name of the teacher </returns>
        [HttpGet]
        [Route("api/TeacherData/findteacher/{id}")]

        public Teacher FindTeacher(int id)
        {
            Teacher SelectedTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where teacherid =" + id;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();


            while (ResultSet.Read())
            {

                //Access Column information by the DB column name as an index
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = (string)ResultSet["teacherfname"].ToString();
                string TeacherLname = (string)ResultSet["teacherlname"].ToString();
                string EmployeeNumber = (string)ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];




                SelectedTeacher.TeacherId = TeacherId;
                SelectedTeacher.TeacherFname = TeacherFname;
                SelectedTeacher.TeacherLname = TeacherLname;
                SelectedTeacher.EmployeeNumber = EmployeeNumber;
                SelectedTeacher.HireDate = HireDate;
                SelectedTeacher.Salary = Salary;


            }

            Conn.Close();

            return SelectedTeacher;
        }

        /// <summary>
        /// delete a teacher on database
        /// </summary>
        /// <param name="id"></param>
        /// 
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }



        /// <summary>
        /// add a teacher to database
        /// </summary>
        /// <param name="NewTeacher"></param>
        [HttpPost]
        public void AddTeacher([FromBody]Teacher NewTeacher)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Debug.WriteLine(NewTeacher.TeacherFname);


            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();


            cmd.CommandText = "insert into teachers (teacherFname,teacherLname,employeenumber,hiredate,salary) value (@TeacherFname,@TeacherLname,@EmployeeNumber,CURRENT_DATE(),@Salary)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }


    } 
} 
