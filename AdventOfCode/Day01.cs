namespace AdventOfCode
{
    public class Day01
    {
        private static readonly string inputFile01 = "input01.txt";

        public static void Part1()
        {
            var result = File.ReadAllLines(inputFile01)
        .Select(line => ConcatFirstAndLastNumber(line))
        .Where(numberString => !string.IsNullOrEmpty(numberString))
        .Select(numberString => int.TryParse(numberString, out int number) ? number : 0)
        .Sum();

            Console.WriteLine(result);
        }

        public static void Part2()
        {
            var NumbersList = new List<string>();
            foreach (var line in File.ReadAllLines(inputFile01))
            {
                var numericalStringReplaced = ReplaceDigitWordsWithNumbers(line);
                var combilenNumbers = ConcatFirstAndLastNumber(numericalStringReplaced);
                NumbersList.Add(combilenNumbers);
            }

            int result = AddNumbersFound(NumbersList);
            Console.WriteLine(result);
        }

        private static string ReplaceDigitWordsWithNumbers(string line)
        {
            var numberDict = new Dictionary<string, string>
            {
                {"oneight", "18" },
                {"twone", "21" },
                {"eightwo", "82" },
                {"one", "1"},
                {"two", "2"},
                {"three", "3"},
                {"four", "4"},
                {"five", "5" },
                {"six", "6"},
                {"seven", "7"},
                {"eight", "8"},
                {"nine", "9"}
            };

            foreach (var entry in numberDict)
            {
                line = line.Replace(entry.Key, entry.Value);
            }
            return line;
        }

        private static int AddNumbersFound(List<string> numbersList) => numbersList
        .Where(numberString => !string.IsNullOrEmpty(numberString))
        .Select(numberString => int.TryParse(numberString, out int number) ? number : 0)
        .Sum();

        private static string ConcatFirstAndLastNumber(string inputString) => inputString
            .FirstOrDefault(char.IsDigit).ToString() + inputString.LastOrDefault(char.IsDigit).ToString();
    }
}