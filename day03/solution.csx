#load "../utils/utils.csx"

using System.Text.RegularExpressions;

public class Day3
{
    bool logToConsole = true;
    bool logToFile = true;

    public void Part1(string[] lines)
    {
        var total = 0L;

        foreach (var line in lines)
        {
            var pattern = @"mul\((\d+),(\d+)\)";
            Regex reg = new Regex(pattern);
            MatchCollection matchedCommands = reg.Matches(line);
            for (int count = 0; count < matchedCommands.Count; count++)
            {
                Utils.Log($"{matchedCommands[count].Value}", logToConsole, logToFile);
                var number1 = long.Parse(matchedCommands[count].Groups[1].Value);
                // Utils.Log($"1: {number1}", logToConsole, logToFile);
                var number2 = long.Parse(matchedCommands[count].Groups[2].Value);
                // Utils.Log($"2: {number2}", logToConsole, logToFile);
                var multiplication = number1 * number2;
                Utils.Log($"{number1}*{number2}={multiplication}", logToConsole, logToFile);
                total += multiplication;
            }            
        }

        Utils.Answer($"Answer # {total}", logToConsole, logToFile);
    }
        
    public void Part2(string[] lines)
    {
        var total = 0L;

        bool dont = false;
        
        foreach (var line in lines)
        {
            // var pattern = @"(don\'t\(\))|(do\(\))";
            var pattern = @"(don\'t\(\))|(do\(\))|(mul\((\d+),(\d+)\))";
            Regex reg = new Regex(pattern);
            MatchCollection matchedCommands = reg.Matches(line);
            
            for (int count = 0; count < matchedCommands.Count; count++)
            {
                Utils.Log($"{matchedCommands[count].Value}", logToConsole, logToFile);

                if (matchedCommands[count].Value == @"don't()")
                {
                    dont=true;
                }
                if (matchedCommands[count].Value == @"do()")
                {
                    dont=false;
                    count++;
                }

                if (!dont)
                {
                    var number1 = long.Parse(matchedCommands[count].Groups[4].Value);
                    // Utils.Log($"1: {number1}", logToConsole, logToFile);
                    var number2 = long.Parse(matchedCommands[count].Groups[5].Value);
                    // Utils.Log($"2: {number2}", logToConsole, logToFile);
                    var multiplication = number1 * number2;
                    Utils.Log($"{number1}*{number2}={multiplication}", logToConsole, logToFile);

                    total += multiplication;                    
                }
            }
        }

        Utils.Answer($"Answer # {total}", logToConsole, logToFile);
    }
}

Utils.Log("-- Day 3 --", true, true);
Utils.Log("-----------", true, true);

var day3 = new Day3();

// string fileName = @"input-sample.txt";
// string fileName = @"input-sample-2.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
// Utils.Log("Part 1", true, true);
// day3.Part1(lines);

// Part 2
Utils.Log("Part 2", true, true);
day3.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();