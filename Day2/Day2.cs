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
                    int prevNr = 0;
                    int dir = 0;
                    int problems = 0;
                    bool safe = true;

                    foreach (int nr in checkList) {
                        if (prevNr == 0) {
                            prevNr = nr;
                            continue;
                        }

                        if (Math.Abs(prevNr - nr) > 3 || Math.Abs(prevNr - nr) == 0) {
                            // Numbers must not be more than 3 apart, and not equal
                            problems +=1;
                            Console.WriteLine(problems + " problem/s detected");

                            if(problems > 1){ Console.WriteLine("Unsafe, breaking"); safe = false; break;}

                            //Console.WriteLine($"Unsafe: Numbers too far apart or equal in {string.Join(" ", checkList)}");
                        }

                        if (dir == 0) {
                            // Determine direction (ascending/descending)
                            dir = (nr > prevNr) ? 1 : -1;
                        } else {
                            // Check if direction is consistent
                            if ((dir == 1 && nr < prevNr) || (dir == -1 && nr > prevNr)) {
                                problems+=1;
                                Console.WriteLine(problems + " problem/s detected");
                                if(problems > 1){Console.WriteLine("Unsafe, breaking");  safe = false; break;}
                            }
                        }
                        prevNr = nr;
                    }
                    //if(problems > 1) safe = false;
                    if (safe) {
                        // Console.WriteLine($"Safe line: {string.Join(" ", checkList)}");
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
        //Console.WriteLine(s);
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == t) // Means the character we are at is a space
            {
                //if(currentT != 0) previousT = currentT;
                //currentT = i;
                if (count == n)
                {
                    return i;
                }
                count++;
            }
        }
        //Console.WriteLine($"Found no spaces");
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