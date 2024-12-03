using System.Text.RegularExpressions;

class Day3 {

    static void Main(string[] args) {
        List<int> totalTall = [];

        try
        {

            string? line;        
            // Regex pattern to match 'mul(x,y)' where x and y are whole numbers
            string pattern = @"mul\((\d+),(\d+)\)";
                   
            // Match the first occurrence
            using StreamReader sr = new StreamReader("input.txt");
            int totalValue = 0;
            
            while ((line = sr.ReadLine()) != null) {
                bool lineDone = false;
                int substrIndex = 0;

                while (!lineDone)
                {
                    Match match = Regex.Match(line.Substring(substrIndex), pattern);

                    if (match.Success)
                    {
                        int x = int.Parse(match.Groups[1].Value);
                        int y = int.Parse(match.Groups[2].Value);
                        totalTall.Add(x * y); // Example operation

                        substrIndex = substrIndex + match.Index + match.Length;
                    }
                    else
                    {
                        Console.WriteLine("No more matches on this line, line done");
                        lineDone = true;
                    }
                }

                //Console.WriteLine("Total calculations: " + string.Join(", ", totalTall));
                
            }
        }
        catch (System.Exception)
        {
            throw;
        }                  
        finally{
            int total = 0;
            foreach (int nr in totalTall)
            {
                total += nr;
            }
            Console.WriteLine(total + " THIS IS MY NUMBER");
        }
    }

    public static int MultNumbers(int a, int b){
        return a*b;
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