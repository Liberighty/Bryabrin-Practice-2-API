using GodSimulator.Data;
using GodSimulator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldUpdate = GodSimulator.Data.Update;

namespace GodSimulator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DivisionController : ControllerBase
    {
        private readonly MyApplicationContext _context;

        public DivisionController(MyApplicationContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetDivision")]
        public List<Division> Get()
        {
            return _context.Divisions.ToList();
        }

        [HttpPut(Name = "PutDivision")]
        public Division Put(DivisionAdd requestDivision)
        {
            var newDivision = new Division
            {
                Name = requestDivision.Name,
            };
            _context.Divisions.Add(newDivision);
            _context.SaveChanges();

            return newDivision;
        }

        [HttpDelete(Name = "DeleteDivision")]
        public IActionResult Delete(int divisionId)
        {
            var deleteDivision = _context.Divisions.FirstOrDefault(x => x.Id == divisionId);
            if (deleteDivision != null)
            {
                _context.Remove(deleteDivision);
                _context.SaveChanges();

                return Ok();
            }

            return NotFound();
        }
    }
}