#load "../utils/utils.csx"

public class Day6
{
    bool logToConsole = true;
    bool logToFile = true;

    // Utils.Log($"{}", logToConsole, logToFile);

    public void Part1(string[] lines)
    {
        var rows = lines.Length;
        var cols = lines[0].Length;

        var matrix = Utils.GenerateMatrix<string>(lines, rows, cols);
        Utils.PrintMatrix(matrix, logToConsole, logToFile);

        var startingPosition = Utils.CoordinatesOf(matrix, "^").ToValueTuple(); //.ToTuple();
        Utils.Log($"SP{startingPosition} '{matrix[startingPosition.Item1, startingPosition.Item2]}'", logToConsole, logToFile);

        bool canMove = true;
        var currentPositionR = startingPosition.Item1;
        while (canMove)
        {
            // Move forward
            if (currentPositionR-1 > -1)
            {
                if (matrix[currentPositionR-1, startingPosition.Item2] == "#")
                {
                    Utils.UpdatePosition(matrix, startingPosition.Item1, startingPosition.Item2, ".");
                    Utils.UpdatePosition(matrix, currentPositionR, startingPosition.Item2, "^");
                    canMove = false;
                    break;
                }

                canMove = true;
                Utils.Log($"UP", logToConsole, logToFile);
                currentPositionR -=1;
            }
            else
            {
                canMove = false;
            }
        }

        Utils.Log($"CP{currentPositionR} ({currentPositionR},{startingPosition.Item2}) '{matrix[currentPositionR, startingPosition.Item2]}'", logToConsole, logToFile);
        Utils.PrintMatrix(matrix, logToConsole, logToFile);

        // turn right 90 degrees
        TurnRight(matrix, currentPositionR, startingPosition.Item2);
    }

    public void TurnRight(string[,] matrix, int rowIndex, int colIndex)
    {
        var currentDirection = matrix[rowIndex, colIndex];
        Utils.Log($"CD '{currentDirection}'", logToConsole, logToFile);

        var newDirection = "";
        switch (currentDirection)
        {
            case "^":
                newDirection = ">";
                break;
            case ">":
                newDirection = "v";
                break;
            case "v":
                newDirection = "<";
                break;
            case "<":
                newDirection = "^";
                break;
            default:
                throw new Exception();
                break;
        }

        Utils.Log($"ND '{newDirection}'", logToConsole, logToFile);

        Utils.UpdatePosition(matrix, rowIndex, colIndex, newDirection);
    }

    // public void Part2(string[] lines)
    // {
    // }
}

Utils.Log("-- Day 6 --", true, true);
Utils.Log("-----------", true, true);

var day6 = new Day6();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day6.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day6.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();