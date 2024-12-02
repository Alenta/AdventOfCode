using System.Text.RegularExpressions;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
class Day2 {
    static void Main(string[] args) {
        try {
                // Regex regex = new(@"^(\d+)\s{3}(\d+)$");

                using StreamReader sr = new StreamReader("Day2-Input.txt");
                string? line;
                line = sr.ReadLine();
                
                int charLocation;
                if(line == null) return;
                List<int> checkList = [];
                while ((line = sr.ReadLine()) != null) {
                    bool stringDone = false;
                    int i = 0;
                    int j = 0;
                    int k = 0;
                    if(checkList.Count > 0){
                        int prevNr = 0;
                        int dir = 0;
                        foreach (int nr in checkList)
                        {
                            if(prevNr == 0) prevNr = nr;
                            else{
                                if(Math.Abs(prevNr-nr) > 3 || Math.Abs(prevNr-nr) == 0) { 
                                    // Not                               
                                }
                                if(dir == 0){
                                    if(prevNr > nr){
                                        
                                    }

                                }
                                else{

                                }
                            }
                        }
                        k++;
                    }
                    Console.WriteLine("MASTER LOOP TICK");
                    while (stringDone != true) {
                        charLocation = GetNthIndex(line, ' ', j);
                        if (charLocation > 0)
                        {
                            Console.WriteLine(line.Substring(i, 2) + " added");
                            checkList.Add(ParseInt(line.Substring(i, 2)));
                            if(checkList[^1] == 0) { Console.WriteLine("nulled out");return; }                
                        }
                        else { Console.WriteLine("nulled out"); stringDone = true; }
                        i=charLocation+1;
                        j++;
                    }
                }            
            }
            catch (Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally{

            }
    }

    public static int GetNthIndex(string s, char t, int n)
    {
        int count = 0;
        Console.WriteLine(s);
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
        Console.WriteLine($"Found no spaces");
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