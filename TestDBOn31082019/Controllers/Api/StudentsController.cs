using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestDBOn31082019.Models;

namespace TestDBOn31082019.Controllers.Api
{
    public class StudentsController : ApiController
    {
        // GET: api/Students
        public List<Student> Get()
        {
            return Student.GetStudent();
        }

        // GET: api/Students/5
        public List<Student> Get(int id)
        {
            return Student.GetStudentWhere("id= " + id);
        }

        // POST: api/Students
        public List<Student> Post(List<Student> students)
        {

            foreach (Student student in students)
            {
                Student.InsertStudent(student);
            }
            return students;
        }


        // PUT: api/Students/5
        public Student Put(Student student)
        {
            return Student.UpdateStudent(student);
        }

        // DELETE: api/Students/5
        public IHttpActionResult Delete(int id)
        {
            if (Get(id)[0] != null)
                return Ok(Student.DeleteStudent(id));
            else
                return NotFound();
        }
    }
}
