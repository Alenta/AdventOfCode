using System.Numerics;
using System.Runtime.InteropServices;

class Day11
{
    static void Main(string[] args) {
        string filePath = "test11.txt";
        StreamReader sr = new StreamReader(filePath);
        string? line = sr.ReadLine();
        if(line == null) return;
        Blink(line, 25);
        Console.WriteLine(line);
        

    }

    static void Blink(string line, int n){
        /*
        Rules:
        
        The first stone, 0, becomes a stone marked 1 ---  
        0 Becomes 1
        The second stone, 1, is multiplied by 2024 to become 2024.
        Single digit numbers other than 0 is multiplied by 2024
        The third stone, 10, is split into a stone marked 1 followed by a stone marked 0.
        Two digit numbers are split
        The fourth stone, 99, is split into two stones marked 9.
        From 10 to 99, all numbers are split. 
        The fifth stone, 999, is replaced by a stone marked 2021976.
        All numbers from 100 and above is multiplied by 2024
        */
        int nr = 0;
        int temp = 0;
        int totalStones = 0;
        string finalLine = "";
        for (int i = 0; i < line.Length; i++)
        {          
            if(line[i] == ' '){ // This means we have read one full number
                if(nr == 0) nr = 1;
                else if(nr < 10) nr *= 2024;
                else if(nr < 99) {
                    temp = nr;
                    int result = (temp / (int)Math.Pow(10,1-2));
                    finalLine += result + " ";
                    result = (temp / (int)Math.Pow(10,1-1));
                    nr = result;
                }
                else if(nr > 99) nr*=2024;
            }
            nr+=line[i];
            finalLine += nr + " ";

        }
        Console.WriteLine(finalLine);

    }
}