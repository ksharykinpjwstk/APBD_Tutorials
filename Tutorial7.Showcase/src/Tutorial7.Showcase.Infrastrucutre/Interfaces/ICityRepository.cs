using Tutorial7.Showcase.Entities;

namespace Tutorial7.Showcase.Infrastrucutre;

public interface ICityRepository
{
    Task<IEnumerable<City>> Get();
    Task<City?> GetById(int id);
}
