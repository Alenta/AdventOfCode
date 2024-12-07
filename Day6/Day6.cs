using System.Numerics;
using System.Runtime.InteropServices;

public enum Directions {North, East, South, West};

class Day6
{
    static void Main(string[] args) {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        // the code that you want to measure comes here
        
        string filePath = "Day6.txt";
        string[] lines = File.ReadAllLines(filePath);
        int rows = lines[0].Length;  
        int cols = lines.Length;
        char[,] chars = new char[cols,rows];
        Vector2 startPos = new Vector2(0,0);
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                chars[i,j] = lines[i][j];
                if(chars[i,j] == '^') { 
                    startPos.X = j; 
                    startPos.Y = i;
                    // Console.WriteLine(startPos.Y + ", "+startPos.X);
                }
            }
        }
        NavigateMaze(chars, startPos); 
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        Console.WriteLine(elapsedMs);
    }


    static void NavigateMaze(char[,] chars, Vector2 startPos)
    {
        bool MazeSolved = false;
        Directions dir = Directions.North;
        // steps is mostly for bug-finding, but can also be interesting for efficiency
        int steps = 0;

        Dictionary<Directions, Vector2> direction = new Dictionary<Directions, Vector2>
        {   // 2D Arrays are weird and annoying, so we have to flip the x and y axis
            { Directions.North, new Vector2(0, -1) },
            { Directions.East, new Vector2(1, 0) },
            { Directions.South, new Vector2(0, 1) },
            { Directions.West, new Vector2(-1, 0) }
        };

        while (!MazeSolved)
        {
            steps++;
            Vector2 offset = direction[dir];
            Vector2 nextPos = new Vector2(startPos.X + offset.X, startPos.Y + offset.Y);

            // Unified bouns check
            if (nextPos.X < 0 || nextPos.X >= chars.GetLength(1) || nextPos.Y < 0 || nextPos.Y >= chars.GetLength(0))
            {
                // Console.WriteLine($"Out of bounds: {startPos.Y},{startPos.X}");
                chars[(int)startPos.Y, (int)startPos.X] = 'X';
                MazeSolved = true;
                break;
            }

            char nextChar = chars[(int)nextPos.Y, (int)nextPos.X];
            // Console.WriteLine($"Next char: {nextChar}");

            if (nextChar == '#')
            {
                // Console.WriteLine($"Hit boundary to the {dir}, switching direction.");
                dir = GetNextDirection(dir); 
            }
            else
            {
                // Console.WriteLine($"Marking position {startPos.Y},{startPos.X} as X. Moving {dir}.");
                chars[(int)startPos.Y, (int)startPos.X] = 'X'; 
                startPos = nextPos; 
            }
        }

        int x = 0;
        for (int i = 0; i < chars.GetLength(0); i++)
        {
            for (int j = 0; j < chars.GetLength(1); j++)
            {
                if (chars[i, j] == 'X') x++;
            }
        }
        Console.WriteLine($"{x} x's, {steps} steps");
        // PrintGrid(chars);
    }
    // Helper function to cycle directions 
    static Directions GetNextDirection(Directions currentDir)
    {
        return currentDir switch
        {
            Directions.North => Directions.East,
            Directions.East => Directions.South,
            Directions.South => Directions.West,
            Directions.West => Directions.North,
            _ => throw new ArgumentOutOfRangeException(nameof(currentDir), "Invalid direction")
        };
    }

    // Helper for bug-finding and a fun visualizer
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
