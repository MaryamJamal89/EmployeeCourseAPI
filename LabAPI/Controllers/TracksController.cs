using LabAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        public LabAPIContext db;
        public TracksController(LabAPIContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public List<Track> GetAll()
        {
            return db.Tracks.ToList();
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            Track e = db.Tracks.Where(e => e.Id == Id).FirstOrDefault();
            if (e == null)
                return NotFound();
            else
                return Ok(e);
        }

        [HttpGet("{Name:alpha}")]
        public ActionResult GetByName(string Name)
        {
            Track e = db.Tracks.Where(e => e.Name == Name).FirstOrDefault();
            if (e == null)
                return NotFound();
            else
                return Ok(e);
        }

        [HttpPost]
        public ActionResult Create(Track em)
        {
            if (ModelState.IsValid)
            {
                db.Tracks.Add(em);
                db.SaveChanges();
                return Created("url", em);
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPut]
        public ActionResult Update(Track em)
        {
            if (ModelState.IsValid)
            {
                db.Entry(em).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
            Track em = db.Tracks.Where((n) => n.Id == Id).FirstOrDefault();
            if (em == null) return NotFound();
            else
            {
                db.Tracks.Remove(em);
                db.SaveChanges();
                return Ok(em);
            }
        }
    }
}
