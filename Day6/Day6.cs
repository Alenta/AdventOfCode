using System.Numerics;
using System.Runtime.InteropServices;

public enum Directions {North, East, South, West};

class Day6
{
    static void Main(string[] args) {
        string filePath = "test6.txt";
        string[] lines = File.ReadAllLines(filePath);
        int rows = lines[0].Length;  
        int cols = lines.Length;
        char[,] chars = new char[rows,cols];
        Vector2 startPos = new Vector2(0,0);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                chars[i,j] = lines[i][j];
                if(chars[i,j] == 'v') { 
                    startPos.X = i; 
                    startPos.Y = j;
                    Console.WriteLine(startPos);
                }
            }
        }
        PrintGrid(chars);
        NavigateMaze(chars, startPos); 
    }


    static void NavigateMaze(char[,] chars, Vector2 startPos){
        bool MazeSolved = false;
        Directions dir = Directions.South;
        int steps = 0;
        while (!MazeSolved)
        {
            steps++;

            // Unified bounds check
            if (startPos.X < 0 || startPos.X >= chars.GetLength(0) || startPos.Y < 0 || startPos.Y >= chars.GetLength(1))
            {
                Console.WriteLine($"Out of bounds: {startPos}");
                MazeSolved = true;
                break;
            }

            switch (dir)
            {
                case Directions.North:
                    if (startPos.Y + 1 >= chars.GetLength(1) || chars[(int)startPos.X, (int)startPos.Y - 1] == '#')
                    {
                        Console.WriteLine("Hit boundary to the north, switching to east");
                        dir = Directions.East;
                    }
                    else
                    {
                        Console.WriteLine($"Marking position {startPos} as X. Moving North.");
                        chars[(int)startPos.X, (int)startPos.Y] = 'X';
                        startPos.Y += 1;
                    }
                    break;

                case Directions.East:
                    if (startPos.X + 1 >= chars.GetLength(0) || chars[(int)startPos.X + 1, (int)startPos.Y] == '#')
                    {
                        Console.WriteLine("Hit boundary to the east, switching to south");
                        dir = Directions.South;
                    }
                    else
                    {
                        Console.WriteLine($"Marking position {startPos} as X. Moving East.");
                        chars[(int)startPos.X, (int)startPos.Y] = 'X';
                        startPos.X += 1;
                    }
                    break;

                case Directions.South:
                    if (startPos.Y - 1 < 0 || chars[(int)startPos.X, (int)startPos.Y + 1] == '#')
                    {
                        Console.WriteLine("Hit boundary to the south, switching to west");
                        dir = Directions.West;
                    }
                    else
                    {
                        Console.WriteLine($"Marking position {startPos} as X. Moving South.");
                        chars[(int)startPos.X, (int)startPos.Y] = 'X';
                        startPos.Y -= 1;
                    }
                    break;

                case Directions.West:
                    if (startPos.X - 1 < 0 || chars[(int)startPos.X - 1, (int)startPos.Y] == '#')
                    {
                        Console.WriteLine("Hit boundary to the west, switching to north");
                        dir = Directions.North;
                    }
                    else
                    {
                        Console.WriteLine($"Marking position {startPos} as X. Moving West.");
                        chars[(int)startPos.X, (int)startPos.Y] = 'X';
                        startPos.X -= 1;
                    }
                    break;
            }
        }

        int x = 0;
        for (int i = 0; i < chars.GetLength(0); i++)
        {
            for (int j = 0; j < chars.GetLength(1); j++)
            {
                if(chars[i,j] == 'X') x++;
            }
        }
        Console.WriteLine(x +" / "+ steps);
    }
    static void PrintGrid(char[,] grid)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Console.Write(grid[i, j]);
            }   
            Console.WriteLine();
        }      
    }
}
