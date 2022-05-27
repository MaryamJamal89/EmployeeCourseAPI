using LabAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public LabAPIContext db;
        public EmployeesController(LabAPIContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public List<Employee> GetAll()
        {
            return db.Employees.ToList(); 
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            Employee e = db.Employees.Where(e => e.Id == Id).FirstOrDefault();
            if (e == null)
                return NotFound();
            else
                return Ok(e);
        }

        [HttpGet("{Name:alpha}")]
        public ActionResult GetByName(string Name)
        {
            Employee e = db.Employees.Where(e => e.Name == Name).FirstOrDefault();
            if (e == null)
                return NotFound();
            else
                return Ok(e);
        }

        [HttpPost]
        public ActionResult Create(Employee em)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(em);
                db.SaveChanges();
                return Created("url", em);
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPut]
        public ActionResult Update(Employee em)
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
            Employee em = db.Employees.Where((n) => n.Id == Id).FirstOrDefault();
            if (em == null) return NotFound();
            else
            {
                db.Employees.Remove(em);
                db.SaveChanges();
                return Ok(em);
            }
        }
    }
}
