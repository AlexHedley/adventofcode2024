#load "../utils/utils.csx"

public class Day2
{
    bool logToConsole = true;
    bool logToFile = true;

    // Utils.Log($"{}", logToConsole, logToFile);

    public void Part1(string[] lines)
    {
        var status = new List<string>();
        foreach (var line in lines)
        {
            var levels = line.Split(' ', StringSplitOptions.TrimEntries).Select(e => long.Parse(e)).ToList(); // switch { var n => ( long.Parse(n) ) };
            // levels.ForEach(Console.WriteLine);
            var diffs = new List<long>();
            for (var l = 0; l < levels.Count; l++)
            {
                if (l == levels.Count - 1) break;
                var diff = levels[l+1] - levels[l];
                Utils.Log($"diff {diff}", logToConsole, logToFile);
                diffs.Add(diff);
            }
            var allNegatives = diffs.All(n => n < 0);
            var allPositives = diffs.All(n => n >= 0);
            var greaterThanThree = diffs.Any(n => Math.Abs(n) > 3);
            var greaterThanOne = diffs.All(n => Math.Abs(n) >= 1);
            Utils.Log($"{allNegatives} | - {allPositives} | >3 {greaterThanThree} | >1 {greaterThanOne}", logToConsole, logToFile);
            
            if (allNegatives || allPositives)
            {
                if (!greaterThanThree)
                {
                    if (greaterThanOne)
                    {
                        status.Add("Safe");
                    }
                }
            }
        }
        Utils.Answer($"Safe # {status.Count}", logToConsole, logToFile);
    }

    // public void Part2(string[] lines)
    // {
    // }
}

Utils.Log("-- Day 2 --", true, true);
Utils.Log("-----------", true, true);

var day2 = new Day2();

// string fileName = @"input-sample.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day2.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day2.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();