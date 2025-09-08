using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Encodings.Web; 

class FilterEmployees
{
    
    static void Main()
    {
        var testCases = new List<(string Name, List<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> Employees)>
        {
            ("Giriş 1", new List<(string, int, string, decimal, DateTime)>
            {
                ("Ali", 30, "IT", 6000m, new DateTime(2018, 5, 1)),
                ("Ayşe", 35, "Finance", 8500m, new DateTime(2019, 3, 15)),
                ("Veli", 28, "IT", 7000m, new DateTime(2020, 1, 1))
            }),

            ("Giriş 2", new List<(string, int, string, decimal, DateTime)>
            {
                ("Mehmet", 26, "Finance", 5000m, new DateTime(2021, 7, 1)),
                ("Zeynep", 39, "IT", 9000m, new DateTime(2018, 11, 20))
            }),

            ("Giriş 3", new List<(string, int, string, decimal, DateTime)>
            {
                ("Burak", 41, "IT", 6000m, new DateTime(2018, 6, 1))
            }),

            ("Giriş 4", new List<(string, int, string, decimal, DateTime)>
            {
                ("Canan", 29, "Finance", 8000m, new DateTime(2019, 9, 1)),
                ("Okan", 35, "IT", 7500m, new DateTime(2020, 5, 10))
            }),

            ("Giriş 5", new List<(string, int, string, decimal, DateTime)>
            {
                ("Elif", 27, "Finance", 6500m, new DateTime(2017, 12, 31))
            }),
        };

        foreach (var testCase in testCases)
        {
            Console.WriteLine($"{testCase.Name} sonucu:");
            string result = FilterEmployee(testCase.Employees);
            Console.WriteLine(result);
            Console.WriteLine();
        }
    }
    public static string FilterEmployee(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        var filtered = employees
            .Where(e =>
                e.Age >= 25 && e.Age <= 40 &&
                (e.Department == "IT" || e.Department == "Finance") &&
                e.Salary >= 5000 && e.Salary <= 9000 &&
                e.HireDate > new DateTime(2017, 1, 1))
            .ToList();

        var names = filtered
            .Select(e => e.Name)
            .OrderByDescending(name => name.Length)
            .ThenBy(name => name)
            .ToList();

        var totalSalary = filtered.Sum(e => e.Salary);
        var count = filtered.Count;
        var averageSalary = count > 0 ? Math.Round(filtered.Average(e => e.Salary), 2) : 0;
        var minSalary = count > 0 ? filtered.Min(e => e.Salary) : 0;
        var maxSalary = count > 0 ? filtered.Max(e => e.Salary) : 0;

        var result = new
        {
            Names = names,
            TotalSalary = totalSalary,
            AverageSalary = averageSalary,
            MinSalary = minSalary,
            MaxSalary = maxSalary,
            Count = count
        };

        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // json içinde türkçe karakterler olmayabiliyo ayşenin yazılabilmesi için koydum
        };

        return JsonSerializer.Serialize(result, options);
    }
}
