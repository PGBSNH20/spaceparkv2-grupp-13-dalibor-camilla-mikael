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
                return NotFound("Payment does ");
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
                return NotFound($"There is no payment for {name}.");
            }
            return payment;
        }

        //PUT api/payments
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment([FromBody] Payment payment)
        {
            var parking = _context.Parkings.FirstOrDefault(p => p.Id == payment.Id);
            
            payment.SpacePortId = parking.SpacePortId;
            payment.PersonName = parking.ParkedBy;
            payment.SpaceShip = parking.ShipName;
            payment.ArrivalTime = DateTime.Now;
            payment.Payed = false;

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return payment;
        }

        //PATCH api/payments/1
        [HttpPatch("{id}")]
        public async Task<ActionResult<Payment>> PatchPayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if(payment != null)
            {
                payment.EndTime = DateTime.Now;
                TimeSpan timeSpan = (payment.EndTime - payment.ArrivalTime);
                payment.Price = timeSpan.Hours * 5;
                payment.Payed = true;

                await _context.SaveChangesAsync();

                return payment;
            }
            return NotFound("Payment not found");
        }
    }
}
