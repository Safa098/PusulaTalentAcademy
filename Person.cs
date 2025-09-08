using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;

public class Person
{
    public static string FilterPeopleFromXml(string xmlData)
    {
        // XML'i yükle
        XDocument doc = XDocument.Parse(xmlData);

        // Kriterlere uyan kişileri filtrele
        var filteredPeople = doc.Descendants("Person")
            .Select(p => new
            {
                Name = (string)p.Element("Name"),
                Age = (int)p.Element("Age"),
                Department = (string)p.Element("Department"),
                Salary = (int)p.Element("Salary"),
                HireDate = DateTime.Parse((string)p.Element("HireDate"), CultureInfo.InvariantCulture)
            })
            .Where(p => p.Age > 30
                     && p.Department == "IT"
                     && p.Salary > 5000
                     && p.HireDate < new DateTime(2019, 1, 1))
            .ToList();

        var result = new
        {
            Names = filteredPeople.Select(p => p.Name).OrderBy(name => name).ToList(),
            TotalSalary = filteredPeople.Sum(p => p.Salary),
            AverageSalary = filteredPeople.Count > 0 ? filteredPeople.Average(p => p.Salary) : 0,
            MaxSalary = filteredPeople.Count > 0 ? filteredPeople.Max(p => p.Salary) : 0,
            Count = filteredPeople.Count
        };

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        return JsonSerializer.Serialize(result, options);
    }
}
