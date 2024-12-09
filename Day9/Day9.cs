using System.Numerics;
using System.Runtime.InteropServices;

class Day9
{
    static void Main(string[] args) {
        string filePath = "test9.txt";
        int i = 0;
        bool freeSpace = false;
        string line = File.ReadAllText(filePath);
        string newLine = "";
        string linePart = "";
        foreach (var c in line)
        {
            if(c == '\0') break;
            // Console.WriteLine("Char: " + c);
            int nr = 0;
            try{
                nr = int.Parse(c.ToString());
            }
            catch{
                Console.WriteLine($"Cannot parse {c} to a pure number");
                break;
            }

            if(!freeSpace) {          
                Console.WriteLine(i + " <- int to convert, "+" int to str: " +i.ToString());
                linePart = i.ToString();
                i++;
                freeSpace = true;
            }
            else {
                freeSpace = false; 
                linePart = ".";
            }

            
            for (int x = 0; x < nr; x++)
            {
                Console.WriteLine("Adding: " + linePart);
                newLine += linePart;
            }
            
        }
        Console.WriteLine(newLine);
    }
}