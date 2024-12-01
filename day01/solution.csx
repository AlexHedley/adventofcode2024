#load "../utils/utils.csx"

public class Day1
{
    bool logToConsole = true;
    bool logToFile = true;

    public void Part1(string[] lines)
    {
        List<long> lhs = new List<long>();
        List<long> rhs = new List<long>();
        
        foreach (var line in lines)
        {
            (long lhs, long rhs) item = line.Split("  ", StringSplitOptions.TrimEntries) switch { var n => ( long.Parse(n[0]), long.Parse(n[^1]) ) };
            // Utils.Log($"item: {item}", logToConsole, logToFile);

            lhs.Add(item.lhs);
            rhs.Add(item.rhs);
        }

        long total = 0;
        for (var i = 0; i < lines.Length; i++)
        {
            var diff = Math.Abs(lhs.Max() - rhs.Max());
            // Utils.Log($"Diff: {diff} ({lhs.Max()} - {rhs.Max()})", logToConsole, logToFile);
            lhs.Remove(lhs.Max());
            rhs.Remove(rhs.Max());

            total += diff;
        }

        Utils.Log($"Total: {total}", logToConsole, logToFile);
        Utils.Answer($"{total}");
    }

    public void Part2(string[] lines)
    {
        List<long> lhs = new List<long>();
        List<long> rhs = new List<long>();
        
        foreach (var line in lines)
        {
            (long lhs, long rhs) item = line.Split("  ", StringSplitOptions.TrimEntries) switch { var n => ( long.Parse(n[0]), long.Parse(n[^1]) ) };
            // Utils.Log($"item: {item}", logToConsole, logToFile);

            lhs.Add(item.lhs);
            rhs.Add(item.rhs);
        }

        long similarity = 0;
        for (var i = 0; i < lines.Length; i++)
        {
            long occurrences = rhs.Count(x => x == lhs[i]);
            long calculation = lhs[i] * occurrences;
            similarity += calculation;
        }

        Utils.Log($"Similarity: {similarity}", logToConsole, logToFile);
        Utils.Answer($"{similarity}");
    }
}

Utils.Log("-- Day 1 --", true, true);
Utils.Log("-----------", true, true);

var day1 = new Day1();

// string fileName = @"input-sample.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day1.Part1(lines);

// Part 2
Utils.Log("Part 2", true, true);
day1.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();