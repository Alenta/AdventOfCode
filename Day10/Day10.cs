using System;
using System.Collections.Generic;
using System.Numerics;

class Day10
{
    static void Main(string[] args)
    {
        string filePath = "test10.txt";
        string[] lines = File.ReadAllLines(filePath);
        int rows = lines.Length; // Number of rows
        int cols = lines[0].Length; // Number of columns
        char[,] grid = new char[rows, cols];

        // Load grid from file
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                grid[i, j] = lines[i][j];
            }
        }

        FindAllPaths(grid);
    }

    static void FindAllPaths(char[,] grid)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        List<List<Vector2>> allPaths = new List<List<Vector2>>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i, j] == '0')
                {
                    // Start searching from each '0'
                    List<Vector2> currentPath = new List<Vector2>();
                    HashSet<Vector2> visited = new HashSet<Vector2>();
                    ExplorePaths(grid, i, j, 1, currentPath, visited, allPaths);
                }
            }
        }

        Console.WriteLine($"Total paths found: {allPaths.Count}");
        foreach (var path in allPaths)
        {
            Console.WriteLine(string.Join(" -> ", path));
        }
    }

    static void ExplorePaths(char[,] grid, int row, int col, int lookingFor, List<Vector2> currentPath, HashSet<Vector2> visited, List<List<Vector2>> allPaths)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        // Add current cell to the path
        currentPath.Add(new Vector2(row, col));
        visited.Add(new Vector2(row, col));

        if (lookingFor > 9)
        {
            // If the path is complete, save it
            allPaths.Add(new List<Vector2>(currentPath));
        }
        else
        {
            // Explore all four directions
            int[] dRow = { -1, 1, 0, 0 }; // North, South, East, West
            int[] dCol = { 0, 0, -1, 1 };

            for (int dir = 0; dir < 4; dir++)
            {
                int newRow = row + dRow[dir];
                int newCol = col + dCol[dir];

                if (IsValid(grid, newRow, newCol, lookingFor, visited))
                {
                    ExplorePaths(grid, newRow, newCol, lookingFor + 1, currentPath, visited, allPaths);
                }
            }
        }

        // Backtrack: remove the current cell from the path and visited set
        currentPath.RemoveAt(currentPath.Count - 1);
        visited.Remove(new Vector2(row, col));
    }

    static bool IsValid(char[,] grid, int row, int col, int lookingFor, HashSet<Vector2> visited)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        return row >= 0 && row < rows &&
               col >= 0 && col < cols &&
               grid[row, col] - '0' == lookingFor &&
               !visited.Contains(new Vector2(row, col));
    }
}
