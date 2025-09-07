using BusStation.REST.Models;
using BusStation.REST.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BusStation.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusesController : ControllerBase
    {
        private readonly ICrudServiceAsync<BusModel> _busService;

        public BusesController(ICrudServiceAsync<BusModel> busService)
        {
            _busService = busService;
        }

        // GET api/buses
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var buses = await _busService.ReadAllAsync();
            return Ok(buses);
        }

        // GET api/buses/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var bus = await _busService.ReadAsync(id);
            if (bus == null)
                return NotFound();
            return Ok(bus);
        }

        // POST api/buses
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BusCreateModel busCreate)
        {
            if (busCreate == null)
                return BadRequest();

            var bus = new BusModel
            {
                Id = Guid.NewGuid(),
                NumberPlate = busCreate.NumberPlate,
                Route = busCreate.Route,
                Capacity = busCreate.Capacity
            };

            var created = await _busService.CreateAsync(bus);
            if (!created)
                return BadRequest();

            await _busService.SaveAsync();

            return CreatedAtAction(nameof(Get), new { id = bus.Id }, bus);
        }

        // PUT api/buses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BusModel busUpdate)
        {
            if (id != busUpdate.Id)
                return BadRequest();

            var existing = await _busService.ReadAsync(id);
            if (existing == null)
                return NotFound();

            var updated = await _busService.UpdateAsync(busUpdate);
            if (!updated)
                return BadRequest();

            await _busService.SaveAsync();

            return NoContent();
        }

        // DELETE api/buses/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var bus = await _busService.ReadAsync(id);
            if (bus == null)
                return NotFound();

            var removed = await _busService.RemoveAsync(bus);
            if (!removed)
                return BadRequest();

            await _busService.SaveAsync();

            return NoContent();
        }
    }
}
