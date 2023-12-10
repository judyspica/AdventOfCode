namespace AdventOfCode
{
    public class Day04
    {
        private static readonly string inputFile04 = "C:\\Users\\jeje7\\source\\repos\\AdventOfCode\\AdventOfCode\\inputs\\input04.txt";

        //List<string> list = new List<string>
        //{
        //    "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53",
        //    "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19",
        //    "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1",
        //    "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83",
        //    "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36",
        //    "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11"
        //};

        public static void Part1()
        {
            int points = 0;
            foreach (var line in File.ReadAllLines(inputFile04))
            {
                var bareLine = line[(line.IndexOf(':') + 2)..];
                var groups = bareLine.Split('|').Select(group => group.Trim().Split(' ')).ToList();
                var matches = groups[0].Intersect(groups[1]).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                points = (int)(points + Math.Pow(2, matches.Count - 1));
            }
            Console.WriteLine(points);
        }

        public static void Part2()
        {
            List<Tuple<int, int>> scratchCardtotal = new List<Tuple<int, int>>();

            foreach (var line in File.ReadAllLines(inputFile04))
            {
                var bareLine = line[(line.IndexOf(':') + 2)..];
                var groups = bareLine.Split('|').Select(group => group.Trim().Split(' ')).ToList();
                var matches = groups[0].Intersect(groups[1]).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                var matchCount = matches.Count;
                scratchCardtotal.Add(new Tuple<int, int>(1, matchCount));
            }

            for (int i = 0; i < scratchCardtotal.Count; i++)
            {
                int x = scratchCardtotal[i].Item1;
                int y = scratchCardtotal[i].Item2;
                if (y > 0 && x > 0)
                {
                    for (int j = i + 1; j <= i + scratchCardtotal[i].Item2 && j < scratchCardtotal.Count; j++)
                    {
                        scratchCardtotal[j] = Tuple.Create(scratchCardtotal[j].Item1 + x, scratchCardtotal[j].Item2);
                    }
                }
            }

            Console.WriteLine($"Sum of Item1: {scratchCardtotal.Sum(t => t.Item1)}");
        }
    }
}