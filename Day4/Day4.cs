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
            int rows = lines.Length;
            int cols = lines[0].Length;
            char[,] charArray = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    charArray[i, j] = lines[i][j];
                }
            }

            // Direction vectors for 8 directions
            int[][] directions = {
                new int[] { -1, 0 },  // Up
                new int[] { 1, 0 },   // Down
                new int[] { 0, -1 },  // Left
                new int[] { 0, 1 },   // Right
                new int[] { -1, -1 }, // Top-left diagonal
                new int[] { -1, 1 },  // Top-right diagonal
                new int[] { 1, -1 },  // Bottom-left diagonal
                new int[] { 1, 1 }    // Bottom-right diagonal
            };

            // Search for the word
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (charArray[i, j] == wordToFind[0]) // Found the first letter
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

        for (int k = 0; k < word.Length; k++)
        {
            int newRow = startRow + k * dirRow;
            int newCol = startCol + k * dirCol;

            if (charArray[newRow, newCol] != word[k])
            {
                return false;
            }
        }

        return true;
    }
}
