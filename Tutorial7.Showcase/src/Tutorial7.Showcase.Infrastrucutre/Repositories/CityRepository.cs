using Microsoft.Data.SqlClient;
using Tutorial7.Showcase.Entities;

namespace Tutorial7.Showcase.Infrastrucutre;

public class CityRepository(string connectionString) : ICityRepository
{
    public async Task<IEnumerable<City>> Get()
    {
        List<City> cities = [];

        const string selectSchools = "SELECT * FROM City";
        using var con = new SqlConnection(connectionString);
        using var com = new SqlCommand(selectSchools, con);

        await con.OpenAsync();

        SqlDataReader reader = await com.ExecuteReaderAsync();
        if (reader.HasRows) 
        {
            while(reader.Read())
            {
                var city = new City {
                    Id = reader.GetInt32(0),
                    CountryId = reader.GetInt32(1),
                    Name = reader.GetString(2)
                };
                cities.Add(city);
            }
        }
        
        return cities;
    }

    public async Task<City?> GetById(int id)
    {
        City? city = null;
        
        const string selectSchools = "SELECT Top 1 * FROM Country WHERE Id = @Id";
        using var con = new SqlConnection(connectionString);
        using var com = new SqlCommand(selectSchools, con);

        com.Parameters.AddWithValue("@Id", id);

        await con.OpenAsync();

        SqlDataReader reader = await com.ExecuteReaderAsync();
        if (reader.HasRows) 
        {
            while(reader.Read())
            {
                city = new City {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
            }
        }

        return city;
    }
}
