using LabAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        public LabAPIContext db;
        public CoursesController(LabAPIContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public List<Course> GetAll()
        {
            //Lazy 
            //return db.Courses.ToList();

            //Eager
            return db.Courses.Include(a => a.Track).ToList();
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            Course c = GetAll().Where(a => a.Id == Id).FirstOrDefault();
            if (c == null)
                return NotFound();
            else
                return Ok(c);
        }

        [HttpGet("{Name:alpha}")]
        public ActionResult GetByName(string Name)
        {
            Course c = GetAll().Where(a => a.Name == Name).FirstOrDefault();
            if (c == null)
                return NotFound();
            else
                return Ok(c);
        }

        [HttpPost]
        public ActionResult Create(Course crs)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(crs);
                db.SaveChanges();
                return Created("url", crs);
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public ActionResult Update(Course crs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crs).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            else return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(int Id)
        {
            Course crs = db.Courses.Where((a) => a.Id == Id).FirstOrDefault();
            if (crs == null) return NotFound();
            else
            {
                db.Courses.Remove(crs);
                db.SaveChanges();
                return Ok(crs);
            }
        }
    }
}
