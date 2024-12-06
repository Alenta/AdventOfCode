using System.Numerics;

public enum Directions {North, East, South, West};

class Day6
{
    static void Main(string[] args) {
        string filePath = "day6.txt";
        string[] lines = File.ReadAllLines(filePath);
        int cols = lines.Length;
        int rows = lines[0].Length;
        char[,] chars = new char[rows,cols];
        Vector2 startPos = new Vector2(0,0);
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if(chars[i,j] == '^') { startPos.X = i; startPos.Y = i;}
                chars[i,j] = lines[i][j];
            }
        }    
        NavigateMaze(chars, startPos); 
    }


    static void NavigateMaze(char[,] chars, Vector2 startPos){
        bool MazeSolved = false;
        Directions dir = Directions.North;
        int steps = 0;
        Vector2 curPos = startPos;
        while (!MazeSolved)
        {
            if (curPos != startPos) steps++;

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
                    if (startPos.Y + 1 >= chars.GetLength(1) || chars[(int)startPos.X, (int)startPos.Y + 1] == '#')
                    {
                        Console.WriteLine("Hit boundary to the north, switching to east");
                        dir = Directions.East;
                    }
                    else if (chars[(int)startPos.X, (int)startPos.Y + 1] == '.')
                    {
                        chars[(int)startPos.X, (int)startPos.Y] = 'X';
                        Console.WriteLine($"Marking position {startPos} as X. Moving North.");
                        startPos.Y += 1;
                    }
                    break;

                case Directions.East:
                    if (startPos.X + 1 >= chars.GetLength(0) || chars[(int)startPos.X + 1, (int)startPos.Y] == '#')
                    {
                        Console.WriteLine("Hit boundary to the east, switching to south");
                        dir = Directions.South;
                    }
                    else if (chars[(int)startPos.X + 1, (int)startPos.Y] == '.')
                    {
                        chars[(int)startPos.X, (int)startPos.Y] = 'X';
                        Console.WriteLine($"Marking position {startPos} as X. Moving East.");
                        startPos.X += 1;
                    }
                    break;

                case Directions.South:
                    if (startPos.Y - 1 < 0 || chars[(int)startPos.X, (int)startPos.Y - 1] == '#')
                    {
                        Console.WriteLine("Hit boundary to the south, switching to west");
                        dir = Directions.West;
                    }
                    else if (chars[(int)startPos.X, (int)startPos.Y - 1] == '.')
                    {
                        chars[(int)startPos.X, (int)startPos.Y] = 'X';
                        Console.WriteLine($"Marking position {startPos} as X. Moving South.");
                        startPos.Y -= 1;
                    }
                    break;

                case Directions.West:
                    if (startPos.X - 1 < 0 || chars[(int)startPos.X - 1, (int)startPos.Y] == '#')
                    {
                        Console.WriteLine("Hit boundary to the west, switching to north");
                        dir = Directions.North;
                    }
                    else if (chars[(int)startPos.X - 1, (int)startPos.Y] == '.')
                    {
                        chars[(int)startPos.X, (int)startPos.Y] = 'X';
                        Console.WriteLine($"Marking position {startPos} as X. Moving West.");
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
                if(chars[i,j] == '^') x++;
            }
        }
        Console.WriteLine(x +" / "+ steps);
    }  
}
