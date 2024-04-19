using Tutorial7.Showcase.Entities;

namespace Tutorial7.Showcase.Infrastrucutre;

public interface ICountryRepository
{
    public Task<IEnumerable<Country>> Get();
    public Task<Country?> GetById(int id);
}
