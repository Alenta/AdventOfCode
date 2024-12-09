using System.Text;

class Day9
{
    static void Main(string[] args)
    {
        string filePath = "test9.txt";
        int i = 0;
        long sum = 0;
        bool freeSpace = false;
        string line = File.ReadAllText(filePath).Trim(); // Trim unnecessary whitespace
        string newLine = "";
        string linePart = "";

        // Order the line
        foreach (var c in line)
        {
            if (char.IsControl(c)) continue; // Skip control characters like '\0'

            int nr = int.Parse(c.ToString());

            /*
            try
            {
            }
            catch
            {
                Console.WriteLine($"Cannot parse '{c}' to a pure number");
                break;
            }
            */
            if (!freeSpace)
            {
                linePart = i.ToString();
                i++;
                freeSpace = true;
            }
            else
            {
                freeSpace = false;
                linePart = ".";
            }

            for (int x = 0; x < nr; x++)
            {
                newLine += linePart;
            }
        }

        StringBuilder builder = new StringBuilder(newLine);

        // Runs until all '.' are at the end of the line
        while (!AreAllDotsAtEnd(builder.ToString()))
        {
            //Console.WriteLine($"Current builder state: {builder}");
            int firstDotIndex = builder.ToString().IndexOf(".");
            if (firstDotIndex != -1)
            {   // Get the index of the last char that is not a '.'
                int lastNonDotIndex = FindLastNonDotIndex(builder.ToString());
                if (lastNonDotIndex != -1)
                {
                    // Move the last non-dot character to the first dot's position
                    builder[firstDotIndex] = builder[lastNonDotIndex];
                    // Replace the last non-dot character with a dot
                    //builder.Remove(builder.Length-1,1);
                    builder[lastNonDotIndex] = '.';
                }
            }
        }
        // Console.WriteLine(builder.ToString());
        // Calculate the sum
        sum = CalculateSumBeforeDot(builder.ToString());
        Console.WriteLine($"Checksum: {sum}");
    }

    static bool AreAllDotsAtEnd(string line)
    {
        int firstDotIndex = line.IndexOf('.');
        if (firstDotIndex == -1) return true; // No dots at all

        for (int i = firstDotIndex; i < line.Length; i++)
        {
            if (line[i] != '.') return false;
        }

        return true;
    }

    static int FindLastNonDotIndex(string line)
    {
        for (int i = line.Length - 1; i >= 0; i--)
        {
            if (line[i] != '.') return i;
        }

        return -1; // No non-dot characters found
    }

    static long CalculateSumBeforeDot(string input)
    {
        long sum = 0;
        int id = 0;

        foreach (char c in input)
        {
            if (c == '.') break;

            if (char.IsDigit(c))
            {
                long digit = c - '0';
                sum += digit * id;
                id++;
            }
        }

        return sum;
    }
}
