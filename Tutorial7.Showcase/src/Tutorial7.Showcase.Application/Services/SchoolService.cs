
using Tutorial7.Showcase.Entities;
using Tutorial7.Showcase.Infrastrucutre;

namespace Tutorial7.Showcase.Application;

public class SchoolService(
    ISchoolRepository schoolRepository, 
    ICountryRepository countryRepository, 
    ICityRepository cityRepository) : ISchoolService
{
    public async Task<bool> Add(AddSchoolDTO newSchool)
    {
        var city = await cityRepository.GetById(newSchool.CityId);
        if (city is null) 
        {
            throw new ArgumentException("City doesn't exist!");
        }
        return await schoolRepository.Add(new School {
            CityId = newSchool.CityId,
            Name = newSchool.Name,
            StudentCoutn = newSchool.StudentCount,
            Description = newSchool.Description
        });
    }

    public async Task<IEnumerable<SchoolDTO>> GetAll()
    {
        List<SchoolDTO> schools = [];
        // Maybe not the best solution from the point of productivity.
        var cities = await cityRepository.Get();
        var countries = await countryRepository.Get();

        var dbSchools = await schoolRepository.Get();
        foreach (var school in dbSchools) 
        {
            var schoolCity = cities.First(ci => ci.Id == school.CityId);
            var schoolCountry = countries.First(c => c.Id == schoolCity.Id);
            schools.Add(new SchoolDTO{ 
                    Country = schoolCountry.Name, 
                    City = schoolCity.Name, 
                    Name = school.Name, 
                    StudentCount = school.StudentCoutn, 
                    Description = school.Description
                });
        }

        return schools;
    }
}
