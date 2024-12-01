using System.Text.RegularExpressions;
using System.Linq;

class Program
{
    static void Main(string[] args){
        // int CompleteDifference = 0; Not needed anymore?
        int SimilarityScore = 0;
        List<int> list1 = [];
        List<int> list2 = [];
        SeparateLists();
        void SeparateLists() {
            
            try {
                // Regex to output two numbers, separated by three spaces (Stolen from chatGPT)
                Regex regex = new(@"^(\d+)\s{3}(\d+)$");

                using StreamReader sr = new StreamReader("Day1-Input.txt");
                string? line;
                
                while ((line = sr.ReadLine()) != null) {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    Match match = regex.Match(line);
                    if (match.Success)
                    {
                        list1.Add(ParseInt(match.Groups[1].Value));
                        list2.Add(ParseInt(match.Groups[2].Value));
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally{
                list1.Sort();
                list2.Sort();
                
                foreach (int nr in list1)
                {
                    int count = list2.Count(n => n == nr);
                    if(count > 0) SimilarityScore += nr * count;
                }
                Console.WriteLine(SimilarityScore);
            }
        }
    
    }
    public static int FindDifference(int nr1, int nr2) { // Not needed anymore?
        Console.WriteLine("Absolute: " + nr1+ " - " +nr2 +" = " + Math.Abs(nr1 - nr2));
        return Math.Abs(nr1 - nr2);
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