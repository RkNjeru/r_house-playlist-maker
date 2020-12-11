using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using playlistApi.Models;

namespace playlistApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloItemsController : ControllerBase
    {
        private readonly HelloContext _context;

        public HelloItemsController(HelloContext context)
        {
            _context = context;
        }

        // GET: api/HelloItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HelloItem>>> GetHelloItems()
        {
            return await _context.HelloItems.ToListAsync();
        }

        // GET: api/HelloItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HelloItem>> GetHelloItem(long id)
        {
            var helloItem = await _context.HelloItems.FindAsync(id);

            if (helloItem == null)
            {
                return NotFound();
            }

            return helloItem;
        }

        // PUT: api/HelloItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHelloItem(long id, HelloItem helloItem)
        {
            if (id != helloItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(helloItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HelloItemExists(id))
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

        // POST: api/HelloItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HelloItem>> PostHelloItem(HelloItem helloItem)
        {
            _context.HelloItems.Add(helloItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHelloItem", new { id = helloItem.Id }, helloItem);
        }

        // DELETE: api/HelloItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHelloItem(long id)
        {
            var helloItem = await _context.HelloItems.FindAsync(id);
            if (helloItem == null)
            {
                return NotFound();
            }

            _context.HelloItems.Remove(helloItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HelloItemExists(long id)
        {
            return _context.HelloItems.Any(e => e.Id == id);
        }
    }
}
