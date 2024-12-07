using System.Numerics;
using System.Runtime.InteropServices;

class Day7
{
    static void Main(string[] args) {
        string filePath = "day7.txt";
        string[] lines = File.ReadAllLines(filePath);
        Dictionary<string, List<long>> results = [];

        long finalResult = 0;

        foreach (var line in lines)
        {
            var parts = line.Split(':');
            if (parts.Length != 2) continue;

            long solution = long.Parse(parts[0].Trim());
            List<long> numbers = [];

            foreach (var num in parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)) {
                if (int.TryParse(num, out int parsedNum)) numbers.Add(parsedNum);
            }

            if (MatchSolution(solution, numbers))
            {
                finalResult += solution;
            }
        }
        Console.WriteLine(finalResult);

        static bool MatchSolution(long solution, List<long> numbers)
        {
            // Hashsets can only hold one of the same value
            // This should perform better than using a list and checking for duplicates
            // We do lose the order of the elements using a HashSet, but that should be fine here
            HashSet<long> foundSolutions = [];

            void FindPermutations(int index, long currentValue)
            {
                // If we've reached the end of the numbers, check the result
                if (index == numbers.Count)
                {
                    if (currentValue == solution)
                    {
                        foundSolutions.Add(solution);
                    }
                    return;
                }

                // This should take the next number, test it with addition.
                // No fit, and it will try with mult instead.
                // Still no fit, and it will keep stepping through the number (index+1)
                // Until it eventually hits the catch aboe (index == numbers.count)
                FindPermutations(index + 1, currentValue + numbers[index]);
                FindPermutations(index + 1, currentValue * numbers[index]);
            }

            // Start recursion
            FindPermutations(1, numbers[0]);

            // Return true if we found any matching solutions
            return foundSolutions.Count > 0;
        }
    }
}
