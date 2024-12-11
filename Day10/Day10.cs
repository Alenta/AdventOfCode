using System;
using System.Collections.Generic;

class Day10
{
    static void Main(string[] args)
    {
        string filePath = "test10.txt";
        string[] lines = File.ReadAllLines(filePath);
        int rows = lines.Length;
        int cols = lines[0].Length;
        char[,] grid = new char[rows, cols];

        // Load grid
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                grid[i, j] = lines[i][j];
            }
        }

        FindPathsAndCalculateValues(grid);
    }

    static void FindPathsAndCalculateValues(char[,] grid)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        int totalPathValue = 0;
        Dictionary<(int, int), int> pathsPerStart = new Dictionary<(int, int), int>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i, j] == '0')
                {
                    HashSet<string> visitedStates = new HashSet<string>();
                    int pathCount = CountPaths(grid, i, j, 1, new HashSet<(int, int)>(), visitedStates);
                    totalPathValue += pathCount;
                    pathsPerStart[(i, j)] = pathCount;
                }
            }
        }

        // Output results
        Console.WriteLine("Paths per starting '0':");
        foreach (var entry in pathsPerStart)
        {
            Console.WriteLine($"Start at <{entry.Key.Item1}, {entry.Key.Item2}>: {entry.Value} paths");
        }

        Console.WriteLine($"Total Path Value (sum of all paths): {totalPathValue}");
    }

    static int CountPaths(char[,] grid, int row, int col, int lookingFor, HashSet<(int, int)> visited, HashSet<string> uniqueStates)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        // Create a unique state representation for the current position and visited set
        string state = $"{row},{col}:{string.Join(",", visited)}";

        // If this state has already been processed, skip it
        if (uniqueStates.Contains(state))
        {
            return 0; // This path has already been validated
        }

        // Mark the state as processed
        uniqueStates.Add(state);

        // Mark the current cell as visited
        visited.Add((row, col));
        Console.WriteLine($"Visiting ({row}, {col}), looking for {lookingFor}");

        if (lookingFor > 9)
        {
            // Path complete
            Console.WriteLine($"Path complete at ({row}, {col})");
            visited.Remove((row, col)); // Backtrack
            return 1;
        }

        int pathCount = 0;

        // Explore all four directions
        int[] dRow = { -1, 1, 0, 0 }; // North, South, East, West
        int[] dCol = { 0, 0, -1, 1 };

        for (int dir = 0; dir < 4; dir++)
        {
            int newRow = row + dRow[dir];
            int newCol = col + dCol[dir];

            if (IsValid(grid, newRow, newCol, lookingFor, visited))
            {
                pathCount += CountPaths(grid, newRow, newCol, lookingFor + 1, visited, uniqueStates);
            }
        }

        // Backtrack: unmark the current cell as visited for other paths
        Console.WriteLine($"Backtracking from ({row}, {col})");
        visited.Remove((row, col));
        return pathCount;
    }


    static bool IsValid(char[,] grid, int row, int col, int lookingFor, HashSet<(int, int)> visited)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        return row >= 0 && row < rows &&
               col >= 0 && col < cols &&
               grid[row, col] - '0' == lookingFor &&
               !visited.Contains((row, col));
    }
}
