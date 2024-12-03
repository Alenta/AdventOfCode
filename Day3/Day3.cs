using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

class Day3 {

    static void Main(string[] args) {
        List<int> totalNumbers = [];
        try
        {
            string? line;        
            // Regex pattern to match 'mul(x,y)' where x and y are whole numbers
            string mulPattern = @"mul\((\d+),(\d+)\)";
            using StreamReader sr = new StreamReader("input.txt");
            // Matching starts off true
            bool doMatch = true;

            while ((line = sr.ReadLine()) != null) {
                bool lineDone = false;
                int substrIndex = 0;

                while (!lineDone) {
                    Match match = Regex.Match(line.Substring(substrIndex), mulPattern);

                    if (match.Success) {
                        if (line.Count() > substrIndex + match.Index) {

                            int length = match.Index;
                            string substring = line.Substring(substrIndex, length);
                            if (substring.Contains("do()")) {
                                Console.WriteLine("We're matching again!");
                                doMatch = true;
                            }
                            if (substring.Contains("don't()")) {
                                Console.WriteLine("No more matching plz :c");
                                doMatch = false;
                            }
                        }

                        int x = int.Parse(match.Groups[1].Value);
                        int y = int.Parse(match.Groups[2].Value);
                        if(doMatch) totalNumbers.Add(x * y);

                        substrIndex = substrIndex + match.Index + match.Length;
                    }
                    else {
                        Console.WriteLine("No more matches on this line, line done");
                        lineDone = true;
                    }
                }                
            }
        }
        catch (System.Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }                  
        finally{
            int total = 0;
            foreach (int nr in totalNumbers) total += nr;
            Console.WriteLine(total + " THIS IS MY NUMBER");
        }
    }
}