#load "../utils/utils.csx"

public class Day8
{
    bool logToConsole = true;
    bool logToFile = true;

    // Utils.Log($"{}", logToConsole, logToFile);

    public void Part1(string[] lines)
    {

    }

    // public void Part2(string[] lines)
    // {
    // }
}

Utils.Log("-- Day 8 --", true, true);
Utils.Log("-----------", true, true);

var day8 = new Day8();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day8.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day8.Part2(lines);

Console.Beep();

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();