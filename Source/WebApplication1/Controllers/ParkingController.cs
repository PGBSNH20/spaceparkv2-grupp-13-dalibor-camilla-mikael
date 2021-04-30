using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly SpaceContext _context;

        public ParkingController(SpaceContext context)
        {
            _context = context;
        }

        // GET api/parkings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parkings>>> GetParkings()
        {

            return await _context.Parkings.ToListAsync();
        }

        //GET api/parkings/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Parkings>> GetParking(int id)
        {
            var parking = await _context.Parkings.FindAsync(id);

            if (parking == null)
            {
                //return NotParked();
            }
            return parking;
        }

        //GET api/parking/name
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<Parkings>>> GetCharacterParking(string name)
        {
            var parking = await _context.Parkings.Where(p => p.ParkedBy == name).ToListAsync();

            if (parking == null)
            {
                //return NotParked();
            }
            return parking;
        }

        //PATCH api/parking/1
        [HttpPatch("{id}")]
        public async Task<ActionResult<Parkings>> PatchParking(int id, string personName, string shipName)
        {
            var parking = await _context.Parkings.FindAsync(id);

            if (!parking.Occupied)
            {
                parking.ParkedBy = personName;
                parking.ShipName = shipName;
                parking.Occupied = true;
            }
            else
            {
                parking.ParkedBy = null;
                parking.ShipName = null;
                parking.Occupied = false;
            }

            return parking;
        }
    }
}

