using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data;
using System.Web.Http;
using System.Web.Http.Description;

/// <summary>
/// DOKLADNIE IDENTYCZNIE JAK W COURSESCONTROLLERS
/// </summary>
namespace AdrianPiecykLaby5.Controllers
{
    [RoutePrefix("api/students")]


    public class StudentsController : ApiController
    {
        AdrianPiecykLaby5DataContext dc = new AdrianPiecykLaby5DataContext();
        List<Student> data;



        public List<Student> GetStudents()
        {
            data = new List<Student>(dc.Students.Select(m => m).ToList());
            return data;
        }


        [HttpGet, Route("")]
        [ResponseType(typeof(List<Student>))]
        public IHttpActionResult Get()
        {
            GetStudents();
            return Ok(data);
        }



        [HttpGet, Route("{id:int}", Name = "GetStudent")]
        [ResponseType(typeof(Student))]
        public IHttpActionResult Get(int id)
        {
            GetStudents();
            var student = data.SingleOrDefault(x => x.StudentsID == id);

            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Post([FromBody]Student studencik)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            GetStudents();
            var maxId = data.Max(x => x.StudentsID);
            studencik.StudentsID = ++maxId;

            data.Add(studencik);
            dc.Students.InsertOnSubmit(studencik);
            dc.SubmitChanges();

            return CreatedAtRoute("GetStudent()", new { id = studencik.StudentsID }, studencik);
        }


        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Put([FromBody]Student student, int id)
        {
            GetStudents();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var studentToEdit = data.SingleOrDefault(x => x.StudentsID == id);
            if (studentToEdit == null)
            {
                return NotFound();
            }

            studentToEdit.Name = student.Name;
            studentToEdit.Surname = student.Surname;
            studentToEdit.DateOfBirth = student.DateOfBirth;
            dc.SubmitChanges();


            return StatusCode(HttpStatusCode.NoContent);

        }

        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            GetStudents();
            var student = data.SingleOrDefault(x => x.StudentsID == id);
            if (student == null)
            {
                return NotFound();
            }
            data.Remove(student);
            dc.Students.DeleteOnSubmit(student);
            dc.SubmitChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }




    }
}
