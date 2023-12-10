namespace AdventOfCode
{
    public class Day02
    {
        private static readonly string inputFile02 = "C:\\Users\\jeje7\\source\\repos\\AdventOfCode\\AdventOfCode\\inputs\\input02.txt";

        public static void Part1()
        {
            int idSum = 0, idNumber = 0;

            foreach (var line in File.ReadAllLines(inputFile02))
            {
                idNumber++;
                string bareLine = line[(line.IndexOf(':') + 2)..];
                if (ValidateGame(bareLine)) { idSum = idNumber + idSum; }
            }
            Console.WriteLine(idSum);
        }

        static bool ValidateGame(string game)
        {
            var numberDict = new Dictionary<string, string>
        {
            {"red", "12" },
            {"green", "13" },
            {"blue", "14" },
        };

            foreach (var entry in numberDict)
            {
                game = game.Replace(entry.Key, entry.Value); // -->  7 13, 14 12, 5 14; 8 12, 4 13; 6 13, 18 12, 9 14
            }

            var isValid = game.Split(';').All(part =>
            {
                var numbers = part.Split(',')
                .Select(pair => pair.Trim().Split(' ').Select(int.Parse).ToArray())
                .ToArray();

                return numbers.All(pair => pair[0] <= pair[1]);
            });

            return isValid;
        }

        public static void Part2()
        {
            var ballSum = 0;
            foreach (var line in File.ReadAllLines(inputFile02))
            {
                string bareLine = line[(line.IndexOf(':') + 2)..]; // instead of substring
                var numberDict = new Dictionary<string, string>
            {
                {"red", "12" },
                {"green", "13" },
                {"blue", "14" },
            };

                foreach (var entry in numberDict)
                {
                    bareLine = bareLine.Replace(entry.Key, entry.Value);
                }
                ballSum += CalculateSum(bareLine);
            }
            Console.WriteLine(ballSum);
        }

        static int CalculateSum(string inputLine)
        {
            int[] constants = { 12, 13, 14 };
            var maxValues = constants.Select(constant =>
            {
                var values = inputLine.Split(';')
                    .Select(group => group.Split(',')
                        .Select(pair =>
                        {
                            var parts = pair.Trim().Split(' ');
                            return new { Key = int.Parse(parts[0]), Value = int.Parse(parts[1]) };
                        })
                        .Where(entry => entry.Value == constant)
                        .Select(entry => entry.Key)
                        .DefaultIfEmpty(0)
                        .Max())
                    .Max();
                return (Constant: constant, MaxValue: values); // converted to tuple
            });

            return maxValues.Aggregate(1, (currentProduct, result) => currentProduct * result.MaxValue);
        }

        //public static void Part1Enhanced(string inputFile02)
        //{
        //    int idSum = File.ReadAllLines(inputFile02)
        //        .Select((line, idNumber) =>
        //        {
        //            string bareLine = line.Substring(line.IndexOf(':') + 2);
        //            return ValidateGame(bareLine) ? idNumber + 1 : 0;
        //        })
        //        .Sum();

        //    Console.WriteLine(idSum);
        //}

        //static bool ValidateGame(string game)
        //{
        //    var numberDict = new Dictionary<string, string>
        //{
        //    {"red", "12" },
        //    {"green", "13" },
        //    {"blue", "14" },
        //};

        //    foreach (var entry in numberDict)
        //    {
        //        game = game.Replace(entry.Key, entry.Value);
        //    }

        //    return game.Split(';').All(part =>
        //        part.Split(',')
        //            .Select(pair => pair.Trim().Split(' ').Select(int.Parse).ToArray())
        //            .All(pair => pair[0] <= pair[1])
        //    );
        //}
    }
}