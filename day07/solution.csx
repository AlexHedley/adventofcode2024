#load "../utils/utils.csx"

using System.Data;
using System.Linq;

public class Day7
{
    bool logToConsole = true;
    bool logToFile = true;

    // Utils.Log($"{}", logToConsole, logToFile);

    public void Part1(string[] lines)
    {
        var sum = 0L;
        var operators = new List<string>() { "+", "*" };

        foreach (var line in lines)
        {
            var equation = GetEquation(line);

            var count = equation.numbers.Count - 1;
            Utils.Log($"#: {count}", logToConsole, logToFile);

            List<char[]> result = SolveMe(count, "+*").ToList();
            var toCheck = result.Where(r => r.Length == count).ToList();
            // var report = string.Join(", ", toCheck.Select(item => string.Concat(item)));
            // Utils.Log($"Report: {report}", logToConsole, logToFile);

            var matched = Calculate(equation, toCheck);

            if (matched)
            {
                Utils.Info($"Found", logToConsole, logToFile);
                sum += long.Parse(equation.test);

                Utils.Error($"Sum: {sum}", logToConsole, logToFile);
            }
        }

        Utils.Answer($"Sum: {sum}", logToConsole, logToFile);
    }

    private bool Calculate((string test, List<string> numbers) equation, List<char[]> toCheck)
    {
        var matched = false;

        foreach (var check in toCheck)
        {
            var merge = Merge(equation.numbers.ToArray(), check);
            var merged = string.Join("", merge);
            Utils.Log($"Merged: {merged}", logToConsole, logToFile);

            // Expression e = new Expression(merged);
            DataTable dt = new DataTable();
            var counter = 0;
            var itemToCalculate = "";

            for (var i = 0; i < merge.Length; i++)
            {
                Utils.Log($"{counter}: Item To Calculate {itemToCalculate} | Merge Item {merge[i]}", logToConsole, logToFile);
                counter++;
                itemToCalculate += merge[i];

                if (counter == 3)
                {
                    Utils.Log($"Item To Calculate {itemToCalculate}", logToConsole, logToFile);

                    // System.OverflowException: Value is either too large or too small for Type 'Int32'.
                    // var v = dt.Compute(itemToCalculate, "");
                    // Utils.Log($"Answer: {v}", logToConsole, logToFile);

                    var calc = 0L;
                    if (itemToCalculate.Contains("*"))
                    {
                        var items = itemToCalculate.Split("*").Select(item => long.Parse(item)).ToList();
                        calc = items[0] * items[1];
                        Utils.Log($"Calc: {calc}", logToConsole, logToFile);
                    }
                    else if (itemToCalculate.Contains("+"))
                    {
                        var items = itemToCalculate.Split("+").Select(item => long.Parse(item)).ToList();
                        calc = items[0] + items[1];
                        Utils.Log($"Calc: {calc}", logToConsole, logToFile);
                    }
                    else
                    {
                        var items = itemToCalculate.Split("|").Select(item => long.Parse(item)).ToList();
                        calc = long.Parse($"{items[0]}{items[1]}");
                        Utils.Log($"Calc: {calc}", logToConsole, logToFile);
                    }

                    itemToCalculate = calc.ToString();
                    counter -= 2;
                }
            }

            Utils.Info($"itemToCalculate: {itemToCalculate}", logToConsole, logToFile);
            if (equation.test == itemToCalculate)
            {
                Utils.Info($"Found", logToConsole, logToFile);
                matched = true;
            }
        }

        return matched;
    }

    private (string test, List<string> numbers) GetEquation(string line)
    {
        //190: 10 19
        // (long test, List<long> numbers) equation = line.Split(":", StringSplitOptions.TrimEntries) switch { var n => ( long.Parse(n[0]), (n[1].Split(" ", StringSplitOptions.TrimEntries)).Select(item => long.Parse(item)).ToList() ) };
        (string test, List<string> numbers) equation = line.Split(":", StringSplitOptions.TrimEntries) switch { var n => (n[0], (n[1].Split(" ", StringSplitOptions.TrimEntries).ToList())) };
        Utils.Log($"{equation.test}", logToConsole, logToFile);
        Utils.Log($"{String.Join(" | ", equation.numbers)}", logToConsole, logToFile);
        // equation.numbers.ForEach(Console.WriteLine);

        return equation;
    }

    // All combinations and permutations of a set of characters to a given length in c#
    // https://stackoverflow.com/a/76874843/2895831
    private static IEnumerable<T[]> SolveMe<T>(int maxLength, IEnumerable<T> chars)
    {
        if (maxLength < 0)
            throw new ArgumentOutOfRangeException(nameof(maxLength));

        if (chars is null)
            throw new ArgumentNullException(nameof(chars));

        var letters = chars.Distinct().ToArray();

        if (letters.Length == 0)
            yield break;

        for (var agenda = new Queue<T[]>(new[] { Array.Empty<T>() });
                agenda.Peek().Length < maxLength;)
        {
            var current = agenda.Dequeue();

            foreach (var letter in letters)
            {
                var next = current.Append(letter).ToArray();

                agenda.Enqueue(next);

                yield return next;
            }
        }
    }

    // C# Merging two arrays alternatively [duplicate]s
    // https://stackoverflow.com/a/64627668/2895831
    public string[] Merge(string[] numbers, char[] operations)
    {
        // string[] numbers = { "1", "2", "3", "4" };
        // string[] operations = { "+", "+", "+" };

        string[] merged = new string[numbers.Length + operations.Length];
        merged[0] = numbers[0];

        for (int i = 1, j = 1; i < numbers.Length; i++)
        {
            merged[j++] = operations[i - 1].ToString();
            merged[j++] = numbers[i];
        }

        return merged;
    }

    // 2021 Day 22
    // Perms
    // 2023 Day 11
    // GetPermutations

    public void Part2(string[] lines)
    {
        var sum = 0L;
        var operators = new List<string>() { "+", "*" };

        foreach (var line in lines)
        {
            var equation = GetEquation(line);

            var count = equation.numbers.Count - 1;
            Utils.Log($"#: {count}", logToConsole, logToFile);

            List<char[]> result = SolveMe(count, "+*").ToList();
            var toCheck = result.Where(r => r.Length == count).ToList();
            // var report = string.Join(", ", toCheck.Select(item => string.Concat(item)));
            // Utils.Log($"Report: {report}", logToConsole, logToFile);

            var matched = Calculate(equation, toCheck);

            if (matched)
            {
                Utils.Info($"Found", logToConsole, logToFile);
                sum += long.Parse(equation.test);

                Utils.Error($"Sum: {sum}", logToConsole, logToFile);
            }
            else
            {
                Utils.Warning($"Equation to Check: {equation}", logToConsole, logToFile);
                result = SolveMe(count, "+*|").ToList();
                toCheck = result.Where(r => r.Length == count).ToList();

                matched = Calculate(equation, toCheck);

                if (matched)
                {
                    Utils.Info($"Found", logToConsole, logToFile);
                    sum += long.Parse(equation.test);

                    Utils.Error($"Sum: {sum}", logToConsole, logToFile);
                }
            }

        }

        Utils.Answer($"Sum: {sum}", logToConsole, logToFile);
    }
}

Utils.Log("-- Day 7 --", true, true);
Utils.Log("-----------", true, true);

var day7 = new Day7();

// string fileName = @"input-sample.txt";
string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
// Utils.Log("Part 1", true, true);
// day7.Part1(lines);

// Part 2
Utils.Log("Part 2", true, true);
day7.Part2(lines);

Console.Beep();

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();