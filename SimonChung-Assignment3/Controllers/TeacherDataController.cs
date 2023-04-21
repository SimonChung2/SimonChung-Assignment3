﻿using MySql.Data.MySqlClient;
using SimonChung_Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;

namespace SimonChung_Assignment3.Controllers
{
    public class TeacherDataController : ApiController
    {
        //The database context class allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller will access the teachers table of our school database.

        ///<summary>
        ///Returns a list of teachers in the school
        ///</summary>
        ///<example>GET api/TeacherData/ListTeachers</example>
        ///<returns>
        ///A list of teachers' first names and last names
        ///</returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Estable a new query for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname)" +
                " like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet= cmd.ExecuteReader();

            //Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher>() { };

            //Loop through each row of the result set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherId"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.Teacherfname= TeacherFname;
                NewTeacher.Teacherlname= TeacherLname;
                NewTeacher.Employeenumber= EmployeeNumber;
                NewTeacher.Hiredate= HireDate;
                NewTeacher.Salary=Salary;

                //Add the Teacher's Name to the List
                Teachers.Add (NewTeacher);
            }

            //Close the connection between the MySQL Database and the Web Server
            Conn.Close();

            //Return the final list of teacher names
            return Teachers;
        }

        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Estable a new query for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from Teachers where teacherid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherId"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.Teacherfname = TeacherFname;
                NewTeacher.Teacherlname = TeacherLname;
                NewTeacher.Employeenumber = EmployeeNumber;
                NewTeacher.Hiredate = HireDate;
                NewTeacher.Salary = Salary;
            }

            return NewTeacher;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <example>POST : /api/TeacherData/DeleteTeacher/9</example>

        [HttpPost]
     
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Estable a new query for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("id", id);
            cmd.Prepare();
            
            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        [HttpPost]
        public void AddTeacher(Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Estable a new query for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, salary, employeenumber, hiredate  ) values (@Teacherfname,@Teacherlname,@Salary, @Employeenumber, @Hiredate)";
            cmd.Parameters.AddWithValue("@Teacherfname", NewTeacher.Teacherfname);
            cmd.Parameters.AddWithValue("@Teacherlname", NewTeacher.Teacherlname);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Parameters.AddWithValue("@Employeenumber", NewTeacher.Employeenumber);
            cmd.Parameters.AddWithValue("@Hiredate", NewTeacher.Hiredate);


            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        } 

        public void UpdateTeacher(int id, [FromBody]Teacher TeacherInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            Debug.WriteLine(TeacherInfo.Teacherfname);

            //Open the connection between the web server and database
            Conn.Open();

            //Estable a new query for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "update teachers set teacherfname=@Teacherfname, teacherlname=@Teacherlname, salary=@Salary, employeenumber=@Employeenumber, hiredate=@Hiredate  where teacherid=@TeacherId";
            cmd.Parameters.AddWithValue("@Teacherfname", TeacherInfo.Teacherfname);
            cmd.Parameters.AddWithValue("@Teacherlname", TeacherInfo.Teacherlname);
            cmd.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
            cmd.Parameters.AddWithValue("@Employeenumber", TeacherInfo.Employeenumber);
            cmd.Parameters.AddWithValue("@Hiredate", TeacherInfo.Hiredate);
            cmd.Parameters.AddWithValue("@TeacherId", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }


    }
}
