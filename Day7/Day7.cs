using System.Numerics;
using System.Runtime.InteropServices;


class Day7
{
    static void Main(string[] args) {
        string filePath = "day7.txt";
        string[] lines = File.ReadAllLines(filePath);
        var results = new Dictionary<string, List<int>>();

        foreach (var line in lines)
        {
            var parts = line.Split(':');
            if (parts.Length != 2)
            {
                Console.WriteLine($"Invalid line format: {line}");
                continue;
            }

            var solution = parts[0].Trim();
            var numbers = new List<int>();

            foreach (var num in parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                if (int.TryParse(num, out int parsedNum))
                {
                    numbers.Add(parsedNum);
                }
                else
                {
                    Console.WriteLine($"Invalid number format: {num}");
                }
            }
            results[solution] = numbers;
        }

        foreach (var result in results)
        {
            Console.WriteLine($"Solution: {result.Key}, Numbers: {string.Join(", ", result.Value)}");
        }
    }
}
