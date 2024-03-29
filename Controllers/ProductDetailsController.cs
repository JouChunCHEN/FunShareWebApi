﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FunShareWebApi.Models;
using System.Collections;

namespace FunShareWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly FUNShareContext _context;

        public ProductDetailsController(FUNShareContext context)
        {
            _context = context;
        }

        // GET: api/ProductDetails
        [HttpGet]
        public async Task<ActionResult<List<ProductDetail>>> GetProductDetail()
        {
          if (_context.ProductDetail == null)
          {
              return NotFound();
          }

            return await _context.ProductDetail.ToListAsync();
        }

        // GET: api/ProductDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetail>> GetProductDetail(int id)
        {
          if (_context.ProductDetail == null)
          {
              return NotFound();
          }
            var productDetail = await _context.ProductDetail.FindAsync(id);

            if (productDetail == null)
            {
                return NotFound();
            }

            return productDetail;
        }

        // PUT: api/ProductDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductDetail(int id, ProductDetail productDetail)
        {
            if (id != productDetail.ProductDetailId)
            {
                return BadRequest();
            }

            _context.Entry(productDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductDetailExists(id))
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

        // POST: api/ProductDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductDetail>> PostProductDetail(ProductDetail productDetail)
        {
          if (_context.ProductDetail == null)
          {
              return Problem("Entity set 'FUNShareContext.ProductDetail'  is null.");
          }
            _context.ProductDetail.Add(productDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductDetail", new { id = productDetail.ProductDetailId }, productDetail);
        }

        // DELETE: api/ProductDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductDetail(int id)
        {
            if (_context.ProductDetail == null)
            {
                return NotFound();
            }
            var productDetail = await _context.ProductDetail.FindAsync(id);
            if (productDetail == null)
            {
                return NotFound();
            }

            _context.ProductDetail.Remove(productDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductDetailExists(int id)
        {
            return (_context.ProductDetail?.Any(e => e.ProductDetailId == id)).GetValueOrDefault();
        }

        //GET: api/ProductDetails/EventList
        [HttpGet]
        [Route("EventList")]
        public async Task<ActionResult<List<CEventListForApp>>> GetEventListForApp(int id)
        {
            if (_context.ProductDetail == null)
            {
                return NotFound();
            }
            var products = await _context.ProductDetail.Where(p=>p.Product.SupplierId==id)
                .Where(p => p.Product.StatusId==12)
                .Select(p => new CEventListForApp
            {
                ProductDetail_ID = p.ProductDetailId,
                ProductName = p.Product.ProductName,
                BeginTime = (DateTime)p.BeginTime,
                EndTime = (DateTime)p.EndTime,
                Address = p.District.City.CityName+ p.District.DistrictName+ p.Address,
                ImageFileName = p.Product.ImageList.First().ImagePath,
                Stock = (int)p.Stock
                })
                .ToListAsync();

            return products;
        }

        
    }
}
