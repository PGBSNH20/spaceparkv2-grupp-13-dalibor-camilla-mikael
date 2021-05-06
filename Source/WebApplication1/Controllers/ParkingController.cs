using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Swapi;

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
        public async Task<ActionResult<IEnumerable<Parkings>>> Get()
        {
            return Ok(await _context.Parkings.ToListAsync());
        }

        //GET api/parkings/1
        [HttpGet("[Action]/{id}")]
        public async Task<ActionResult<Parkings>> GetParking(int id)
        {
            var parking = await _context.Parkings.FindAsync(id);

            if (parking == null)
            {
                return NotFound("There is no parkingspot with this ID.");
            }
            return parking;
        }

        //GET api/parking/name
        [HttpGet("[Action]/{name}")]
        public async Task<ActionResult<IEnumerable<Parkings>>> GetCharacterParking(string name)
        {
            var parking = await _context.Parkings.Where(p => p.ParkedBy == name).ToListAsync();

            if (parking == null)
            {
                return NotFound($"Nobody with the name {name} has parked here.");
            }
            return parking;
        }

        //PATCH api/parking/1
        [HttpPatch("[action]/{id}/{personName}/{shipName}")]
        public async Task<ActionResult> PatchParking(int id, string personName, string shipName)
        {
            var parking = await _context.Parkings.FindAsync(id);

            if(parking != null && !parking.Occupied)
            {
                var People = new StarwarsPeople().GetAllPersons(); // Get all starwars characters.

                var Ships = new StarwarsShips().GetAllShips(); // Get all starwars ships.

                var PersonMatch = People.Result.Where(p => p.Name.ToLower() == personName.ToLower()).FirstOrDefault();
                if(PersonMatch != null)
                {
                    var StarshipMatch = Ships.Result.Where(s => s.Name.ToLower() == shipName.ToLower()).FirstOrDefault();

                    if(StarshipMatch != null && StarshipMatch.Length >= parking.MaxLength)
                    {
                        parking.ParkedBy = personName;
                        parking.ShipName = shipName;
                        parking.Occupied = true;

                        // Add new payment
                        var payment = new Payment();
                        payment.SpacePortId = parking.SpacePortId;
                        payment.ParkingId = parking.Id;
                        payment.PersonName = parking.ParkedBy;
                        payment.SpaceShip = parking.ShipName;
                        payment.ArrivalTime = DateTime.Now;
                        payment.Payed = false;

                        _context.Payments.Add(payment);
                        await _context.SaveChangesAsync();
                        return StatusCode(StatusCodes.Status201Created);
                    }

                    return BadRequest($"There is no shipmodel named {shipName} or the ship is to big for this parkinglot.");
                }

                return BadRequest($"No one with the name {personName} participated in any star wars movie.");
            }

            // Actions for leaving parkinglot and make a payment.
            if(parking.Occupied)
            {
                parking.ParkedBy = null;
                parking.ShipName = null;
                parking.Occupied = false;

                // Pay for parking
                Payment payment = _context.Payments.Where(p => p.ParkingId == parking.Id).FirstOrDefault();
                payment.EndTime = DateTime.Now;
                TimeSpan timeParked = (payment.EndTime - payment.ArrivalTime);
                payment.Price = timeParked.Hours * 5;
                payment.Payed = true;
                                                                  
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created);
            }

            return NotFound("Parking does not exist.");
        }

    }
}

