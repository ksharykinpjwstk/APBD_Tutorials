/*
All LINQ query operations consist of three distinct actions:
    1. Obtain the data source.
    2. Create the query.
    3. Execute the query.
 */

// The Three Parts of a LINQ Query:
// 1. Data source.
int[] numbers = [ 0, 1, 2, 3, 4, 5, 6 ];

// 2. Query creation.
// numQuery is an IEnumerable<int>
var numQuery = numbers.Where(num => num % 2 == 0).Select(num => num);

// 3. Query execution.
foreach (int num in numQuery)
{
    // 0 2 4 6 
    Console.Write("{0,1} ", num);
}

// ALL LINQ QUERIES TECHNICALLY CAN BE RE-WRITTEN TO LOOP AND VICE VERSA!

// LINQ is used for select operations. We don't mix it with add/edit/delete operations

// SIDE EFFECTS + Deffered execution
Console.WriteLine();
var numList = numbers.ToList();
var x = Enumerable.Range(0, 5).Select(i =>
{
    numList.Add(i);
    return i;
});
x.ToList();
//x.ToList();
foreach (var num in numList)
{
    Console.WriteLine(num); 
}

// query syntax (try to avoid it)
// Data source.
int[] scores = [90, 71, 82, 93, 75, 82];

// Query Expression.
IEnumerable<int> scoreQuery = //query variable
    from score in scores //required
    where score > 80 // optional
    orderby score descending // optional
    select score; //must end with select or group

// Execute the query to produce the results
foreach (var testScore in scoreQuery)
{
    Console.WriteLine(testScore);
}

// INNER JOIN

var strList1 = new List<string>() { 
    "One", 
    "Two", 
    "Three", 
    "Four"
};

var strList2 = new List<string>() { 
    "One", 
    "Two", 
    "Five", 
    "Six"
};

var joinResult = strList1.Join(strList2,
    str1 => str1, 
    str2 => str2, 
    (str1, str2) => str1);

foreach (var element in joinResult)
{
    Console.WriteLine(element);
}

// Custom LINQ Methods

double[] numbersFloat = [1.9, 2, 8, 4, 5.7, 6, 7.2, 0];
var query = numbersFloat.Median();

Console.WriteLine($"double: Median = {query}");

public static class EnumerableExtension
{
    public static double Median(this IEnumerable<double>? source)
    {
        if (source is null || !source.Any())
        {
            throw new InvalidOperationException("Cannot compute median for a null or empty set.");
        }

        var sortedList =
            source.OrderBy(number => number).ToList();

        var itemIndex = sortedList.Count / 2;

        return sortedList.Count % 2 == 0
            ?
            // Even number of items.
            (sortedList[itemIndex] + sortedList[itemIndex - 1]) / 2
            :
            // Odd number of items.
            sortedList[itemIndex];
    }
}