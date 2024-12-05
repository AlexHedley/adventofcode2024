#load "../utils/utils.csx"

public class Day5
{
    bool logToConsole = true;
    bool logToFile = true;

    // Utils.Log($"{}", logToConsole, logToFile);

    public void Part1(string[] lines)
    {
        var patterns = GetPatterns(lines);

        var rules = new List<(long, long)>();

        var pattern1 = patterns[0];
        foreach (var line in pattern1)
        {
            Utils.Log($"{line}", logToConsole, logToFile);
            (long pageX, long pageY) pages = line.Split('|') switch { var p => (long.Parse(p[0]), long.Parse(p[1])) };
            // Tuple.Create()
            Utils.Log($"X:{pages.pageX}|Y{pages.pageY}", logToConsole, logToFile);
            rules.Add(pages);
        }
        Utils.Log("---", logToConsole, logToFile);
        var pattern2 = patterns[1];
        foreach (var line in pattern2)
        {
            var updates = line.Split(',').Select(e => long.Parse(e)).ToList();
            var joined = string.Join("   ", updates);
            Utils.Log($"Joined: {joined}", logToConsole, logToFile);

            foreach (var update in updates)
            {
                Utils.Log($"To Check: {update}", logToConsole, logToFile);
                var matching = rules.Where(p => p.Item1 == update).ToList();
                // Utils.Log($"Matching #: {matching}", logToConsole, logToFile);
                Utils.Log($"Matching #: {matching.Count}", logToConsole, logToFile);

                // First Check
                Utils.Log($"{matching.Count} == {updates.Count}", logToConsole, logToFile);
                if (matching.Count != updates.Count)
                {
                    Utils.Error($"Not Matching", logToConsole, logToFile);
                    break;
                }

                foreach (var match in matching)
                {
                    Utils.Log($"{match}", logToConsole, logToFile);
                    // Utils.Log($"{match.Item1}", logToConsole, logToFile);
                    // Utils.Log($"{match.Item2}", logToConsole, logToFile);
                }

                // Check the other updates
                for (var u = 1; u < updates.Count; u++)
                {
                    Utils.Log($"{u}: {updates[u]}", logToConsole, logToFile);

                    var next = matching.Where(p => p.Item2 == updates[u]).ToList();
                    Utils.Log($"Match #: {next.Count}", logToConsole, logToFile);

                    var matchingSecond = rules.Where(p => p.Item1 == updates[u]).ToList();
                    foreach (var match in matchingSecond)
                    {
                        Utils.Log($"{match}", logToConsole, logToFile);
                        // Utils.Log($"{match.Item1}", logToConsole, logToFile);
                        // Utils.Log($"{match.Item2}", logToConsole, logToFile);
                    }

                    // 75,47,61,53,29
                    // Remove 75
                    var toCheck = updates.ToList();
                    toCheck.Remove(update);
                    // Remove 47
                    toCheck.Remove(updates[u]);
                    var left = string.Join("   ", toCheck);
                    Utils.Log($"Updates: {left}", logToConsole, logToFile);

                }

            }
        }
    }

    public List<List<string>> GetPatterns(string[] lines)
    {
        List<List<string>> patternMaps = new List<List<string>>();

        List<string> curSet = new List<string>();    
        foreach (var line in lines)
        {
            if (line.Trim().Length == 0)
            {
                if (curSet.Count > 0)
                {
                    patternMaps.Add(curSet);
                    curSet = new List<string>();
                }               
            }
            else
            {
                curSet.Add(line);
            }
        }

        if (curSet.Count > 0)
        {
            patternMaps.Add(curSet);
        }

        return patternMaps;
    }

    // public void Part2(string[] lines)
    // {
    // }
}

Utils.Log("-- Day 5 --", true, true);
Utils.Log("-----------", true, true);

var day5 = new Day5();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day5.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day5.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();