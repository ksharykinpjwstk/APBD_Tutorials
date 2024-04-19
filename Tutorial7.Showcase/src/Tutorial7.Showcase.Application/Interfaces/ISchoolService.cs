namespace Tutorial7.Showcase.Application;

public interface ISchoolService
{
    Task<IEnumerable<SchoolDTO>> GetAll();

    Task<bool> Add(AddSchoolDTO newSchool);
}
