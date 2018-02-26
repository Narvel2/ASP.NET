using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data;





namespace AdrianPiecykLaby5.Controllers
{
    [RoutePrefix("api/enrollment")]

    public class EnrollmentController : ApiController
    {
        AdrianPiecykLaby5DataContext enroll = new AdrianPiecykLaby5DataContext();
        List<Connector> dataConnector;


        public List<Connector> GetConnector()
        {
            dataConnector = new List<Connector>(enroll.Connectors.Select(x => x).ToList());
            return dataConnector;


        }

        /// <summary>
        /// Do Controllers dodaje studentID i CourseID 
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="CourseID"></param>
        /// <returns></returns>
        [HttpPost, Route("{StudentID:int}/{CourseID:int}")]
        public IHttpActionResult Post(int StudentID, int CourseID)
        {


            GetConnector();
            Connector conn = new Connector();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stud = enroll.Students.Single(x => x.StudentsID == StudentID);
            var cour = enroll.Courses.Single(x => x.CoursesID == CourseID);

            conn.Student = stud;
            conn.Course = cour;


            dataConnector.Add(conn);

            enroll.Connectors.InsertOnSubmit(conn);
            enroll.SubmitChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }
        /// <summary>
        /// Wypisywanie polaczen 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        [ResponseType(typeof(List<Connector>))]
        public IHttpActionResult Get()
        {

            GetConnector();
            return Ok(dataConnector);
        }


    }
}
