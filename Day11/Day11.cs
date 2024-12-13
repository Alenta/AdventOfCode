using System.Dynamic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

class Day11
{
    static void Main(string[] args) {
        string filePath = "test11.txt";
        StreamReader sr = new StreamReader(filePath);
        string? line = sr.ReadLine();
        if(line == null) return;
        Console.WriteLine(line + " turns into:");
        List<long> output = [];
        List<long> ints = ParseNumbersFromString(line);
        output = Blink(ints, 25);
        
        Console.WriteLine(output.Count() + " THIS");

        static List<long> Blink(List<long> intList, int n)
        {
            Queue<long> queue = new Queue<long>(intList);
            List<long> finalList = new List<long>();
            HashSet<long> processed = new HashSet<long>();

            while (n > 0)
            {
                int queueSize = queue.Count;

                for (int i = 0; i < queueSize; i++)
                {
                    long number = queue.Dequeue();

                    if (processed.Contains(number))
                    {
                        finalList.Add(number);
                        continue;
                    }

                    Console.WriteLine($"Processing number: {number}");

                    try
                    {
                        if (number == 0)
                        {
                            queue.Enqueue(1);
                        }
                        else if (HasEvenDigits(number))
                        {
                            string numStr = number.ToString();
                            int mid = numStr.Length / 2;

                            string leftStr = numStr.Substring(0, mid);
                            string rightStr = numStr.Substring(mid);

                            if (!int.TryParse(leftStr, out int left) || !int.TryParse(rightStr, out int right))
                                throw new FormatException($"Invalid split for {number}: '{leftStr}' and '{rightStr}'");

                            queue.Enqueue(left);
                            queue.Enqueue(right);
                        }
                        else
                        {
                            long result = checked(number * 2024);

                            if (result < 0)
                                throw new OverflowException($"Overflow detected: {number} * 2024 = {result}");

                            queue.Enqueue(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing number {number}: {ex.Message}");
                        throw;
                    }

                    processed.Add(number);
                }

                n--;
                if (n == 0) finalList.AddRange(queue);
            }
            return finalList;
            Console.WriteLine("Final result:");
            Console.WriteLine(string.Join(" ", finalList));
        }
    }

    static bool HasEvenDigits(long number)
    {
        return number.ToString().Length % 2 == 0;
    }

    static List<long> ParseNumbersFromString(string input)
    {
        List<long> numbers = new List<long>();
        string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (string part in parts)
        {
            if (long.TryParse(part, out long number))
            {
                Console.WriteLine("Number: " + part);
                numbers.Add(number);
            }
            else
            {
                Console.WriteLine($"Warning: Unable to parse '{part}' as an integer.");
            }
        }

        return numbers;
    }
}