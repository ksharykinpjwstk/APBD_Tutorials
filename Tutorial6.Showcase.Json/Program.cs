// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Tutorial6.Showcase.Json.Models;

const string fileName = "example.json";
var jsonString = File.ReadAllText(fileName);
var serializerOptions = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};
var personData = JsonSerializer.Deserialize<Person>(jsonString, serializerOptions)!;
Console.WriteLine(personData);

var anotherPersonData = new Person
{
    Name = "Chris",
    FavouriteGame = null,
    Jobs = ["Entrepreneur"],
    Universities = null
};

List<Person> persons = [personData, anotherPersonData];

var serializedPeople = JsonSerializer.Serialize(persons);
Console.WriteLine(serializedPeople);

File.WriteAllText("anotherExample.json", serializedPeople);