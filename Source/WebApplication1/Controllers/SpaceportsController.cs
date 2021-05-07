using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuthAttributeAdmin]
    public class SpaceportsController : ControllerBase
    {
        private readonly SpaceContext _context;

        public SpaceportsController(SpaceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Spaceport> Get()
        {
            return _context.Spaceports;
        }

        [HttpPost("[Action]/{name}")]
        public async Task<IActionResult> AddSpacePort(string name)
        {
            var spaceport = new Spaceport() { Name = name };

            var matchingSpaceport = _context.Spaceports.SingleOrDefault(s => s.Name == name);

            if(matchingSpaceport == null || name == null)
            {
                return BadRequest($"A spaceport already exist with the name {name} or there where no name provided.");
            }

            _context.Spaceports.Add(spaceport);
            await _context.SaveChangesAsync();

            return Ok($"{name} has been added.");
        }

        [HttpPut("[Action]/{oldName}/{newName}")]
        public async Task<IActionResult> ChangeNameOnSpaceport(string oldName, string newName)
        {
            var spaceportToChange = _context.Spaceports.SingleOrDefault(s => s.Name == oldName);

            if(oldName.ToLower() == newName.ToLower())
            {
                return BadRequest("There is already a spaceport with that name.");
            }

            else if(spaceportToChange != null)
            {
                spaceportToChange.Name = newName;
                await _context.SaveChangesAsync();
                return Ok($"{oldName} has changed name to {newName}.");
            }

            return BadRequest("There is no spaceport with that name.");
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteSpaceport(int id)
        {
            var spaceport = _context.Spaceports.SingleOrDefault(s => s.Id == id);
            if (spaceport == null)
            {
                return NotFound("We cannot find any spaceport matching this ID.");
            }
            else
            {
                _context.Spaceports.Remove(spaceport);
                await _context.SaveChangesAsync();
                return Ok("Spaceport deleted.");
            }
        }
    }
}
