using Tutorial7.Showcase.Entities;

namespace Tutorial7.Showcase.Infrastrucutre;

public interface ISchoolRepository
{
    Task<bool> Add(School newSchool);
    Task<IEnumerable<School>> Get();
    Task<School?> GetById(int id);
    Task<bool> Update(School school);
    Task<bool> Delete(School school);
}
