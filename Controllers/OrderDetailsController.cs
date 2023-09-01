using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FunShareWebApi.Models;

namespace FunShareWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly FUNShareContext _context;

        public OrderDetailsController(FUNShareContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetail()
        {
          if (_context.OrderDetail == null)
          {
              return NotFound();
          }
            return await _context.OrderDetail.ToListAsync();
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
          if (_context.OrderDetail == null)
          {
              return NotFound();
          }
            var orderDetail = await _context.OrderDetail.FindAsync(id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            return orderDetail;
        }

        // PUT: api/OrderDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetail(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailId)
            {
                return BadRequest();
            }

            _context.Entry(orderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetail orderDetail)
        {
          if (_context.OrderDetail == null)
          {
              return Problem("Entity set 'FUNShareContext.OrderDetail'  is null.");
          }
            _context.OrderDetail.Add(orderDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderDetail", new { id = orderDetail.OrderDetailId }, orderDetail);
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            if (_context.OrderDetail == null)
            {
                return NotFound();
            }
            var orderDetail = await _context.OrderDetail.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetail.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderDetailExists(int id)
        {
            return (_context.OrderDetail?.Any(e => e.OrderDetailId == id)).GetValueOrDefault();
        }

        //GET: api/OrderDetails/AttendList
        [HttpGet]
        [Route("AttendList")]
        public async Task<ActionResult<List<CAttendList>>> GetAttendListForApp(int id)
        {
            if (_context.OrderDetail == null)
            {
                return NotFound();
            }
            var attendList = await _context.OrderDetail.Where(o=>o.ProductDetailId == id)
                .Select(od => new CAttendList
                {
                    OrderDetailId = od.OrderDetailId,
                    Name = od.Member.Name,
                    Email = od.Member.Email,
                    odNumber = $"OD{od.Order.OrderTime.Date.ToString("yyyyMMdd")}{od.OrderDetailId.ToString("0000")}",
                    IsAttend = od.IsAttend,
                }).ToListAsync();

            return attendList;
        }

    }
}
