using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Web.Http.Description;


namespace AdrianPiecykLaby5.Controllers
{
    /// <summary>
    /// Podaje z jakiego url bede korzystal
    /// </summary>

    [RoutePrefix("api/courses")]

    public class CoursesController : ApiController
    {
        /// <summary>
        /// Tworze polaczenie z baza danych korzystajac z LINQ
        /// </summary>

        AdrianPiecykLaby5DataContext context = new AdrianPiecykLaby5DataContext();
        /// <summary>
        /// deklaracja listy kursow
        /// </summary>
        List<Course> dataCourse;

        /// <summary>
        /// Funkcja w ktorej do listy dodaje wszystkie kursy
        /// </summary>
        /// <returns></returns>
        public List<Course> GetCourses()
        {
            dataCourse = new List<Course>(context.Courses.Select(c => c).ToList());
            return dataCourse;
        }
        /// <summary>
        /// wyswietlanie w url api/courses wszystkich kursow
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        [ResponseType(typeof(List<Course>))]
        public IHttpActionResult Get()
        {
            GetCourses();
            return Ok(dataCourse);
        }


        /// <summary>
        /// Wyszukiwanie po id odpowiedniego kursu 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet, Route("{ID:int}", Name = "GetCourses")]
        [ResponseType(typeof(Student))]
        public IHttpActionResult Get(int ID)
        {
            GetCourses();
            var course = dataCourse.SingleOrDefault(x => x.CoursesID == ID);

            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }
        /// <summary>
        /// Dodawanie nowego kursu
        /// </summary>
        /// <param name="coursk"></param>
        /// <returns></returns>
        [HttpPost, Route("")]
        public IHttpActionResult Post([FromBody]Course coursk)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            GetCourses();
            var maxId = dataCourse.Max(x => x.CoursesID);
            coursk.CoursesID = ++maxId;

            dataCourse.Add(coursk);
            context.Courses.InsertOnSubmit(coursk);
            context.SubmitChanges();

            return CreatedAtRoute("GetCourses", new { id = coursk.CoursesID }, coursk);
        }
        /// <summary>
        /// Edytowanie kursu
        /// </summary>
        /// <param name="course"></param>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Put([FromBody]Course course, int id)
        {
            GetCourses();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var courseToEdit = dataCourse.SingleOrDefault(x => x.CoursesID == id);
            if (courseToEdit == null)
            {
                return NotFound();
            }

            courseToEdit.Name = course.Name;
            courseToEdit.ECTSPoints = course.ECTSPoints;
            context.SubmitChanges();

            return StatusCode(HttpStatusCode.NoContent);

        }
        /// <summary>
        /// Usuwanie kursu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            GetCourses();
            var course = dataCourse.SingleOrDefault(x => x.CoursesID == id);
            if (course == null)
            {
                return NotFound();
            }
            dataCourse.Remove(course);
            context.Courses.DeleteOnSubmit(course);
            context.SubmitChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
