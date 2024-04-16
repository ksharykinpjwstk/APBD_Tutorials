using System.Data;
using Microsoft.Data.SqlClient;
using Tutorial6.Showcase.Models;

namespace Tutorial6.Showcase.Repositories;

public class CarRepository(string connectionString) : ICarRepository
{
    public IEnumerable<Car> GetAll()
    {
        List<Car> cars = new();
        // Provide the query string with a parameter placeholder.
        const string queryString = "SELECT * FROM Car";

        // Create and open the connection in a using block. This
        // ensures that all resources will be closed and disposed
        // when the code exits.
        using (SqlConnection connection = new(connectionString))
        {
            // Create the Command and Parameter objects.
            SqlCommand command = new(queryString, connection);

            // Open the connection in a try/catch block.
            // Create and execute the DataReader, writing the result
            // set to the console window.
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    // Read row by row
                    while (reader.Read()) 
                    {
                        var car = new Car {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Weight = reader.GetDouble(2),
                            TopSpeed = reader.GetDouble(3)
                        };
                        cars.Add(car);
                    }
                }
            }
            finally 
            {
                reader.Close();
            }
        }

        return cars;
    }

    public Car? GetById(int id)
    {
        Car? specificCar = null;
        // Provide the query string with a parameter placeholder.
        const string queryString = "SELECT * FROM Car WHERE Id = @carId";

        // Create and open the connection in a using block. This
        // ensures that all resources will be closed and disposed
        // when the code exits.
        using (SqlConnection connection = new(connectionString))
        {
            // Create the Command and Parameter objects.
            SqlCommand command = new(queryString, connection);
            command.Parameters.AddWithValue("carId", id);

            // Open the connection in a try/catch block.
            // Create and execute the DataReader, writing the result
            // set to the console window.
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                   while (reader.Read()) 
                   {
                        specificCar = new Car {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Weight = reader.GetDouble(2),
                            TopSpeed = reader.GetDouble(3)
                        };
                   } 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally {
                reader.Close();
            }
        }

        return specificCar;
    }

    public bool AddCar(Car newCar) 
    {
        const string insertString = "INSERT INTO Car(Id, Name, Weight, TopSpeed) VALUES (@Id, @Name, @Weight, @TopSpeed)";
        int countRowsAdded = -1;
        using (SqlConnection connection = new SqlConnection(connectionString)) 
        {
            // Create the Command and Parameter objects.
            SqlCommand command = new(insertString, connection);
            command.Parameters.AddWithValue("Id", newCar.Id);
            command.Parameters.AddWithValue("Name", newCar.Name);
            command.Parameters.AddWithValue("Weight", newCar.Weight);
            command.Parameters.AddWithValue("TopSpeed", newCar.TopSpeed);

            connection.Open();
            countRowsAdded = command.ExecuteNonQuery();
        }

        return countRowsAdded != -1;
    }
}