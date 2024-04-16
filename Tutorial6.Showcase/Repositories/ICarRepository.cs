using Tutorial6.Showcase.Models;

namespace Tutorial6.Showcase.Repositories;

public interface ICarRepository
{
    IEnumerable<Car> GetAll();
    Car? GetById(int id);
    
}