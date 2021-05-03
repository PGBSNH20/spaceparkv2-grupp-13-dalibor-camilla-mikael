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
        public async Task<ActionResult<IEnumerable<Payment>>> GetParkings()
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
                //return NotParked();
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
                //return NotParked();
            }
            return payment;
        }

        //PUT api/payments
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(Spaceport spaceport, string name, string ship)
        {
            Payment payment = new Payment();

            payment.SpacePortId = spaceport.Id;
            payment.PersonName = name;
            payment.SpaceShip = ship;
            payment.ArrivalTime = DateTime.Now;
            payment.Payed = false;

            await _context.SaveChangesAsync();

            return payment;
        }

        //PATCH api/payments/1
        [HttpPatch("{id}")]
        public async Task<ActionResult<Payment>> PatchPayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            payment.EndTime = DateTime.Now;
            payment.Price = (payment.EndTime.Hour - payment.ArrivalTime.Hour) * 5;
            payment.Payed = true;

            await _context.SaveChangesAsync();

            return payment;
        }
    }
}
