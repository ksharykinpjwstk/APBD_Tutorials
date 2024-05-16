using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorial10.RestApi.DTO;
using Tutorial10.RestApi.Helpers;
using Tutorial10.RestApi.Models;

namespace Tutorial10.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController(CarContext context) : ControllerBase
    {
        // GET: api/Car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await context.Cars.ToListAsync();
        }

        // GET: api/Car/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/Car/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, CarDto updateCar)
        {
            var car = await context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            car = FromDto(updateCar);
            car.Id = id;
            context.Entry(car).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        // POST: api/Car
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(CarDto newCar)
        {
            var car = FromDto(newCar);
            context.Cars.Add(car);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CarExists(car.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("PostCar", new { id = car.Id }, car);
        }

        // DELETE: api/Car/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            context.Cars.Remove(car);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return context.Cars.Any(e => e.Id == id);
        }

        private Car FromDto(CarDto carDto)
        {
            return new Car
            {
                Weight = carDto.Weight,
                Name = carDto.Name,
                TopSpeed = carDto.TopSpeed
            };
        }
    }
}
