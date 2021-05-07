using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPost]
        public IActionResult PostSpacePort([FromBody] Spaceport spaceport)
        {
            if(string.IsNullOrEmpty(spaceport.Name))
            {
                return BadRequest("The spaceport must have a name.");
            }

            _context.Spaceports.Add(spaceport);
            _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
