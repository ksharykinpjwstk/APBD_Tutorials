using Microsoft.Data.SqlClient;
using Tutorial7.Showcase.Entities;

namespace Tutorial7.Showcase.Infrastrucutre;

public class CountryRepository(string connectionString) : ICountryRepository
{
    public async Task<IEnumerable<Country>> Get()
    {
        List<Country> countries = [];

        const string selectSchools = "SELECT * FROM Country";
        using var con = new SqlConnection(connectionString);
        using var com = new SqlCommand(selectSchools);

        SqlDataReader reader = await com.ExecuteReaderAsync();
        if (reader.HasRows) 
        {
            while(reader.Read())
            {
                var country = new Country {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
                countries.Add(country);
            }
        }
        
        return countries;
    }

    public async Task<Country?> GetById(int id)
    {
        Country? country = null;
        
        const string selectSchools = "SELECT Top 1 * FROM Country WHERE Id = @Id";
        using var con = new SqlConnection(connectionString);
        using var com = new SqlCommand(selectSchools);

        com.Parameters.AddWithValue("@Id", id);

        SqlDataReader reader = await com.ExecuteReaderAsync();
        if (reader.HasRows) 
        {
            while(reader.Read())
            {
                country = new Country {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
            }
        }

        return country;
    }
}
