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
    public class SuppliersController : ControllerBase
    {
        private readonly FUNShareContext _context;

        public SuppliersController(FUNShareContext context)
        {
            _context = context;
        }

        // GET: api/Suppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSupplier()
        {
          if (_context.Supplier == null)
          {
              return NotFound();
          }
            return await _context.Supplier.ToListAsync();
        }

        // GET: api/Suppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(int id)
        {
          if (_context.Supplier == null)
          {
              return NotFound();
          }
            var supplier = await _context.Supplier.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return supplier;
        }

        // PUT: api/Suppliers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplier(int id, Supplier supplier)
        {
            if (id != supplier.SupplierId)
            {
                return BadRequest();
            }

            _context.Entry(supplier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(id))
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

        // POST: api/Suppliers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Supplier>> PostSupplier(Supplier supplier)
        {
          if (_context.Supplier == null)
          {
              return Problem("Entity set 'FUNShareContext.Supplier'  is null.");
          }
            _context.Supplier.Add(supplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupplier", new { id = supplier.SupplierId }, supplier);
        }

        // DELETE: api/Suppliers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            if (_context.Supplier == null)
            {
                return NotFound();
            }
            var supplier = await _context.Supplier.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            _context.Supplier.Remove(supplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupplierExists(int id)
        {
            return (_context.Supplier?.Any(e => e.SupplierId == id)).GetValueOrDefault();
        }

        // GET: api/Suppliers/5
        [HttpGet]
        [Route("CheckSupplier")]
        public async Task<ActionResult<CLoginInfo>> CheckSupplier(string taxId)
        {
            if (_context.Supplier == null)
            {
                return NotFound();
            }
            var supplier = await _context.Supplier.Where(x=>x.TaxId==taxId).FirstAsync();

            if (supplier == null)
            {
                return NotFound();
            }

            CLoginInfo info = new CLoginInfo
            {
                SupplierId = supplier.SupplierId,
                TaxId = supplier.TaxId,
                Password = supplier.Password,
                SupplierName = supplier.SupplierName,
                LogoImage = supplier.LogoImage,
            };

            return info;
        }
    }
}
