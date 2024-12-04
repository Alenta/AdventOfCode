using System.Text.RegularExpressions;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
class Day2 {
    static void Main(string[] args) {
        int safeLines = 0;
        int allLines = 0;
        try {
            using StreamReader sr = new StreamReader("test.txt");
            string? line;

            while ((line = sr.ReadLine()) != null) {
                List<int> checkList = new List<int>();
                int i = 0, j = 0;
                bool stringDone = false;

                // Parse the line into the checkList
                while (!stringDone) {
                    int charLocation = GetNthIndex(line, ' ', j);
                    if (charLocation > 0) {
                        int parsedNumber = ParseInt(line.Substring(i, 2));
                        if (parsedNumber == 0) {
                            Console.WriteLine("Parsing failed; exiting.");
                            return;
                        }
                        checkList.Add(parsedNumber);
                        //Console.WriteLine("Adding " + parsedNumber);
                        i = charLocation + 1;
                    } else {
                        checkList.Add(ParseInt(line.Substring(i)));
                        stringDone = true;
                    }
                    j++;
                }

                if (checkList.Count > 0) {
                    Console.WriteLine("Checking: "+ string.Join(";", checkList));
                    int prevNr = 0;
                    int dir = 0;
                    int problems = 0;
                    bool safe = true;

                    int? problemID = null; // Track the number causing the first problem
                    bool directionReset = false; // Track if the direction has been reset

                    foreach (int nr in checkList)
                    {
                        if (prevNr == 0)
                        {
                            prevNr = nr;
                            continue;
                        }

                        // Check the absolute difference rule
                        if (Math.Abs(prevNr - nr) > 3 || Math.Abs(prevNr - nr) == 0)
                        {
                            problems++;

                            if (problems >= 2)
                            {
                                // Attempt to resolve by replacing prevNr with problematicNr
                                if (problemID != null && Math.Abs(problemID.Value - nr) <= 3)
                                {
                                    Console.WriteLine($"Resolving with problematicNr {problemID}. Updating prevNr.");
                                    prevNr = problemID.Value;
                                    problemID = null;
                                    problems--; // Reduce problem count as we resolved one
                                    continue;
                                }

                                Console.WriteLine($"Unsafe due to: {nr} | Problems: {problems}");
                                safe = false;
                                break;
                            }

                            if (problems == 1)
                            {
                                // Log the problematic number
                                problemID = nr;

                                if (dir == 0 || (dir == 1 && nr > prevNr) || (dir == -1 && nr < prevNr))
                                {
                                    Console.WriteLine($"Ignoring {nr}, updating prevNr to {nr}");
                                    prevNr = nr; // Allow replacement of prevNr if it resolves the issue
                                }
                                else
                                {
                                    Console.WriteLine($"Ignoring {nr}, keeping prevNr as {prevNr}");
                                }

                                continue;
                            }
                        }

                        // Check direction consistency
                        if (dir == 0)
                        {
                            // Establish direction
                            dir = (nr > prevNr) ? 1 : -1;
                        }
                        else
                        {
                            // Validate the direction
                            if ((dir == 1 && nr < prevNr) || (dir == -1 && nr > prevNr))
                            {
                                problems++;
                                Console.WriteLine($"Problem with direction at {nr}: prevNr = {prevNr}, dir = {dir}");

                                if (problems >= 2)
                                {
                                    // Check if replacing prevNr with problematicNr resolves the issue
                                    if (problemID != null && Math.Abs(problemID.Value - nr) <= 3)
                                    {
                                        Console.WriteLine($"Resolving with problematicNr {problemID}. Updating prevNr.");
                                        prevNr = problemID.Value;
                                        problemID = null;
                                        problems--; // Reduce problem count as we resolved one
                                        continue;
                                    }

                                    safe = false;
                                    break;
                                }

                                if (problems == 1)
                                {
                                    Console.WriteLine($"Resetting direction for {nr}. Ignoring this number.");
                                    problemID = nr; // Track the reset number
                                    dir = 0; // Reset direction
                                    directionReset = true;
                                    continue;
                                }
                            }
                        }

                        // Update prevNr only if no new problems are detected
                        prevNr = nr;

                        // Handle direction reset case
                        if (directionReset)
                        {
                            Console.WriteLine($"Direction reset with {nr}. Updating direction.");
                            dir = (nr > prevNr) ? 1 : -1; // Re-establish direction
                            directionReset = false;
                        }
                    }




                    // After the loop, check if the line is safe
                    if (safe) {
                        safeLines++;
                    }

                    allLines++;
                }
            }            
                Console.WriteLine($"{safeLines} <- Safelines, all lines {allLines}");
            }
            catch (Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally{
                Console.WriteLine(safeLines);

            }
    }

    public static int GetNthIndex(string s, char t, int n)
    {
        int count = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == t) // Means the character we are at is a space
            {
                if (count == n)
                {
                    return i;
                }
                count++;
            }
        }
        return -1;
    }

    public static int ParseInt(string stringToParse)
    {
        try
        {
            int number = Int32.Parse(stringToParse);
            return number;
        }
        catch (FormatException)
        {
            Console.WriteLine($"Cannot parse {stringToParse} to a pure number");
            return 0;
        }
    }
    

}