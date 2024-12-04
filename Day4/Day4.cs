using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

class Day4
{
    static void Main(string[] args)
    {
        string filePath = "input4.txt";
        string wordToFind = "XMAS";
        int totalXMAS = 0;

        try
        {
            // Read the file into a 2D array of characters
            string[] lines = File.ReadAllLines(filePath);
            int cols = lines.Length;
            int rows = lines[0].Length;
            char[,] charArray = new char[cols, rows]; // 2D charArray defined by length of string array and length of line 1

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                { // Populate the charArray from the string array
                    charArray[i, j] = lines[i][j];
                }
            }

            // Direction vectors for 8 directions
            int[][] directions = [
                [-1, 0],  // Up
                [1, 0],   // Down
                [0, -1],  // Left
                [0, 1],   // Right
                [-1, -1], // Top-left diagonal
                [-1, 1],  // Top-right diagonal
                [1, -1],  // Bottom-left diagonal
                [1, 1]    // Bottom-right diagonal
            ];

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (charArray[i, j] == wordToFind[0]) // First letter identified
                    {
                        foreach (var dir in directions)
                        {
                            if (IsWordInDirection(charArray, i, j, dir[0], dir[1], wordToFind))
                            {
                                totalXMAS++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Total occurrences of '{wordToFind}': {totalXMAS}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    static bool IsWordInDirection(char[,] charArray, int startRow, int startCol, int dirRow, int dirCol, string word)
    {
        int rows = charArray.GetLength(0);
        int cols = charArray.GetLength(1);

        // Check if the word fits within the grid
        int endRow = startRow + (word.Length - 1) * dirRow;
        int endCol = startCol + (word.Length - 1) * dirCol;
        if (endRow < 0 || endRow >= rows || endCol < 0 || endCol >= cols)
        {
            return false;
        }

        for (int i = 0; i < word.Length; i++)
        {
            int newRow = startRow + i * dirRow;
            int newCol = startCol + i * dirCol;

            if (charArray[newRow, newCol] != word[i])
            {
                return false;
            }
        }

        return true;
    }
}
