using System.Data;
using Microsoft.Data.SqlClient;
using Tutorial7.Showcase.Entities;

namespace Tutorial7.Showcase.Infrastrucutre;

public class SchoolRepository(string connectionString) : ISchoolRepository
{
    public async Task<bool> Add(School newSchool)
    {
        int result;
        using var con = new SqlConnection(connectionString);
        if (newSchool.Description is null) 
        {
            using var com = new SqlCommand("AddSchoolWithoutDescription", con) 
            {
                CommandType = CommandType.StoredProcedure
                
            };

            com.Parameters.Add("@IdCity", SqlDbType.Int).Value = newSchool.CityId;
            com.Parameters.Add("@SchoolName", SqlDbType.VarChar).Value = newSchool.Name;
            com.Parameters.Add("@StudentCount", SqlDbType.Int).Value = newSchool.StudentCoutn;

            await con.OpenAsync();
            result = await com.ExecuteNonQueryAsync();
        }
        else 
        {
            var sqlTextCom = @"INSERT INTO School(CityId, SchoolName, StudentCount, Description) VALUES 
                            (@CityId, @SchoolName, @StudentCount, @Description)";
            using var com = new SqlCommand(sqlTextCom);
            com.Parameters.AddWithValue("@CityId", newSchool.CityId);
            com.Parameters.AddWithValue("@SchoolName", newSchool.Name);
            com.Parameters.AddWithValue("@StudentCount", newSchool.StudentCoutn);
            com.Parameters.AddWithValue("@Description", newSchool.Description);

            result = await com.ExecuteNonQueryAsync();
        }

        return result > 0;
    }

    public Task<bool> Delete(School school)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<School>> Get()
    {
        List<School> schools = [];
        const string selectSchools = "SELECT * FROM School";
        using var con = new SqlConnection(connectionString);
        using var com = new SqlCommand(selectSchools);

        SqlDataReader reader = await com.ExecuteReaderAsync();
        if (reader.HasRows) 
        {
            while(reader.Read());
            {
                var school = new School {
                    Id = reader.GetInt32(0),
                    CityId = reader.GetInt32(1),
                    Name = reader.GetString(2),
                    StudentCoutn = reader.GetInt32(3),
                    Description = reader.GetString(4)
                };
                schools.Add(school);
            }
        }

        return schools;
    }

    public Task<School?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(School school)
    {
        throw new NotImplementedException();
    }
}
