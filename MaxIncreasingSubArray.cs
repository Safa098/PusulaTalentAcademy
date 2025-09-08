using System;
using System.Collections.Generic;
using System.Text.Json;

public class MaxIncreasingSubarray
{
    public static string MaxIncreasingSubarrayAsJson(List<int> numbers)
    {
        if (numbers == null || numbers.Count == 0)
            return JsonSerializer.Serialize(new List<int>());

        List<int> maxSubarray = new List<int>();
        List<int> currentSubarray = new List<int> { numbers[0] };
        int maxSum = numbers[0];
        int currentSum = numbers[0];

        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] > numbers[i - 1])
            {
                currentSubarray.Add(numbers[i]);
                currentSum += numbers[i];
            }
            else
            {
                if (currentSum > maxSum)
                {
                    maxSum = currentSum;
                    maxSubarray = new List<int>(currentSubarray);
                }
                currentSubarray = new List<int> { numbers[i] };
                currentSum = numbers[i];
            }
        }

        // Son alt diziyi de kontrol et
        if (currentSum > maxSum)
        {
            maxSubarray = currentSubarray;
        }

        return JsonSerializer.Serialize(maxSubarray);
    }

    public static void Main()
    {
        var testCases = new List<List<int>>
        {
            new List<int> {1, 2, 3, 1, 2},
            new List<int> {2, 5, 4, 3, 2, 1},
            new List<int> {1, 2, 2, 3},
            new List<int> {1, 3, 5, 4, 7, 8, 2},
            new List<int> {}
        };

        for (int i = 0; i < testCases.Count; i++)
        {
            string result = MaxIncreasingSubarrayAsJson(testCases[i]);
            Console.WriteLine($"Giriş {i + 1}: [{string.Join(",", testCases[i])}]");
            Console.WriteLine($"Sonuç {i + 1}: {result}");
            Console.WriteLine();
        }
    }
}
