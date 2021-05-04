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
    public class PaymentController : ControllerBase
    {
        private readonly SpaceContext _context;

        public PaymentController(SpaceContext context)
        {
            _context = context;
        }

        // GET api/payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> Get()
        {
            return await _context.Payments.ToListAsync();
        }

        //GET api/payments/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {

            }
            return payment;
        }

        //GET api/payments/name
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetCharacterPayment(string name)
        {
            var payment = await _context.Payments.Where(p => p.PersonName == name).ToListAsync();

            if (payment == null)
            {

            }
            return payment;
        }

        //PUT api/payments
        [HttpPost]
        public IActionResult PutPayment([FromBody] Payment payment)
        {
            var parking = _context.Parkings.FirstOrDefault(p => p.Id == payment.Id);
            var spacePort = _context.Spaceports.FirstOrDefault(s => s.Id == parking.SpacePortId);

            _context.Payments.Add(payment);
            payment.SpacePortId = spacePort.Id;
            payment.PersonName = parking.ParkedBy;
            payment.SpaceShip = parking.ShipName;
            payment.ArrivalTime = DateTime.Now;
            payment.Payed = false;

            _context.Payments.Add(payment);
            _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, "Payment added.");
        }

        //PATCH api/payments/1
        [HttpPatch("{id}")]
        public IActionResult PatchPayment(int id)
        {
            try
            {
                var payment = _context.Payments.FirstOrDefault(p => p.Id == id);
        
                payment.EndTime = DateTime.Now;
                TimeSpan timeSpan = (payment.EndTime - payment.ArrivalTime);
                payment.Price = timeSpan.Hours * 5;
                payment.Payed = true;

                _context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, "Payment done.");
            }
            
            catch
            {
                return NotFound("Payment not found");
            }
        }

    }
}
