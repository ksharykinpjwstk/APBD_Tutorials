using Microsoft.AspNetCore.Mvc;
using Tutorial6.Showcase.Models;
using Tutorial6.Showcase.Repositories;

[ApiController]
public class CarController(ICarRepository carRepository) : ControllerBase 
{
    [HttpGet("api/cars")]
    public IActionResult Get() 
    {
        return Ok(carRepository.GetAll());      
    }

    [HttpGet("api/cars/{id}")]
    public IActionResult Get(int id) 
    {
        return Ok(carRepository.GetById(id)); 
    }

    [HttpPost("api/cars")]
    public IActionResult Create([FromBody] Car car) 
    {
        var isCreated = carRepository.AddCar(car);
        return isCreated ? Created() : BadRequest();
    }
}