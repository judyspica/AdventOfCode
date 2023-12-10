using System.Text.RegularExpressions;

public class Day03
{
    private static readonly string inputFile03 = "input03.txt";

    public static void Part1()
    {
        string[] lines = File.ReadAllLines(inputFile03);
        int rows = lines.Length;
        int cols = lines[0].Length;
        char[,] grid = new char[rows, cols];
        char[,] gridCopy = new char[rows, cols];
        InitGrid(rows, cols, grid, lines);
        InitGrid(rows, cols, gridCopy, lines);

        ReplaceNumbersAdjacentToSymbolsWithX(grid);

        var textRows = ConvertArrayToTextStrings(grid);
        var validNumbers = ExtractValidNumbers(textRows);

        var textRowsCopy = ConvertArrayToTextStrings(gridCopy);
        var allNumbers = ExtractAllNumbers(textRowsCopy);

        int difference = allNumbers.Sum() - validNumbers.Sum();
        Console.WriteLine("difference is : " + difference);
    }

    static void ReplaceNumbersAdjacentToSymbolsWithX(char[,] grid)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        char[] symbols = { '#', '*', '$', '+', '-', '%', '&', '=', '@', '/' };

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i, j] == '.' || !char.IsDigit(grid[i, j]))
                    continue;

                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int newX = i + dx;
                        int newY = j + dy;

                        if (newX >= 0 && newX < rows && newY >= 0 && newY < cols && symbols.Contains(grid[newX, newY]))
                            grid[i, j] = 'X';
                    }
                }
            }
        }
    }
    private static List<int> ExtractAllNumbers(List<string> chatLinesCopy) => chatLinesCopy
        .SelectMany(line => Regex.Matches(line, @"\d+").Cast<Match>())
        .Select(match => int.Parse(match.Value))
        .ToList();

    static List<int> ExtractValidNumbers(List<string> lines) => lines
            .SelectMany(line => line.Split('.'))
            .Where(s => !s.Contains('X') && int.TryParse(s, out _))
            .Select(s => int.Parse(s))
            .ToList();

    static List<string> ConvertArrayToTextStrings(char[,] chatArray) =>
        Enumerable.Range(0, chatArray.GetLength(0))
            .Select(i => new string(Enumerable.Range(0, chatArray.GetLength(1)).Select(j => chatArray[i, j]).ToArray())).ToList();

    private static void InitGrid(int rows, int cols, char[,] grid, string[] lines)
    {
        for (int x = 0; x < rows; x++)
            for (int y = 0; y < cols; y++)
                grid[x, y] = lines[x][y];
    }
    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    // source https://github.com/ZephyrSquall/AdventOfCode/blob/main/2023/Solvers/Day03Solver.cs
    public static void Part2()
    {
        List<string> lines = new(File.ReadAllLines(inputFile03));
        int count = 0;

        for (int i = 0; i < lines.Count; i++)
        {
            string line = lines[i];
            //Console.Write(line);
            for (int j = 0; j < line.Length; j++)
            {
                char character = line[j];
                if (character == '*')
                {
                    var numbers = getAdjacentNumbers(lines, i, j);
                    if (numbers.Count == 2)
                    {
                        count += numbers[0] * numbers[1];
                    }
                }
            }
            //Console.WriteLine();
        }
        Console.WriteLine(count);
    }

    static List<int> getAdjacentNumbers(List<string> lines, int startingLine, int startingPosition)
    {
        List<int> numbers = new();

        for (int i = startingLine - 1; i <= startingLine + 1; i++)
        {
            if (i >= 0 && i < lines.Count)
            {
                if (Char.IsDigit(lines[i][startingPosition]))
                {
                    int number = GetNumberValue(lines[i], startingPosition);
                    numbers.Add(number);
                }
                else
                {
                    int[] remainingPositions = { startingPosition - 1, startingPosition + 1 };
                    numbers.AddRange(from int j in remainingPositions
                                     where j >= 0 && j < lines[i].Length
                                     let character = lines[i][j]
                                     where char.IsDigit(character)
                                     select GetNumberValue(lines[i], j));
                }
            }
        }
        return numbers;
    }

    static int GetNumberValue(string line, int linePosition)
    {
        int startingPosition = linePosition;
        int endingPosition = linePosition;

        while (startingPosition > 0 && Char.IsDigit(line[startingPosition - 1]))
        {
            startingPosition--;
        }

        while (endingPosition < line.Length - 1 && Char.IsDigit(line[endingPosition + 1]))
        {
            endingPosition++;
        }
        
        string numberString = line.Substring(startingPosition, endingPosition - startingPosition + 1);
        int number = int.Parse(numberString);
        return number;
    }
}