#load "../utils/utils.csx"

public class Day4
{
    bool logToConsole = true;
    bool logToFile = true;

    public void Part1(string[] lines)
    {
        var rows = lines.Length;
        var cols = lines[0].Length;

        var matrix = Utils.GenerateMatrix<string>(lines, rows, cols);
        Utils.PrintMatrix(matrix, logToConsole, logToFile);

        var count = FindXMAS(lines, matrix);

        Utils.Answer($"Answer # {count}", logToConsole, logToFile);
    }

    // public void Part2(string[] lines)
    // {
    // }


    public long FindXMAS(string[] lines, string[,] matrix)
    {
        var total = 0L;

        int rowLength = matrix.GetLength(0);
        int colLength = matrix.GetLength(1);

        // Rows
        for (int i = 0; i < rowLength; i++)
        {
            var row = Utils.GetRow(matrix, i);
            var line = string.Join("", row);
            var reversed = string.Join("", line.Reverse());
            
            var rtol = line.Contains("XMAS");
            var ltor = reversed.Contains("XMAS");
            
            if (rtol) total += 1;
            if (ltor) total += 1;

            if (rtol || ltor) {
                Utils.Log($"{i}: {line} | {reversed}", logToConsole, logToFile);
                Utils.Log($"{i}: {rtol} | {ltor}", logToConsole, logToFile);
            }
        }
        
        // Columns
        for (int j = 1; j < colLength - 1; j++)
        {
            var row = Utils.GetColumn(matrix, j);
            var line = string.Join("", row);
            var reversed = string.Join("", line.Reverse());
            
            var rtol = line.Contains("XMAS");
            var ltor = reversed.Contains("XMAS");
            
            if (rtol) total += 1;
            if (ltor) total += 1;

            if (rtol || ltor) {
                Utils.Log($"{j}: {line} | {reversed}", logToConsole, logToFile);
                Utils.Log($"{j}: {rtol} | {ltor}", logToConsole, logToFile);
            }
        }

        Utils.Info($"H/V: {total}", logToConsole, logToFile);

        // Diagonals
        // https://stackoverflow.com/questions/42172408/get-all-diagonals-in-a-2-dimensional-matrix-using-linq
        // var d = matrix.SelectMany((row, rowIdx) =>
        //     row.Select((x, colIdx) => new { Key = rowIdx - colIdx, Value = x }))
        //     .GroupBy(x => x.Key, (key, values) => values.Select(i => i.Value).ToArray())
        //     .ToArray();
        // Utils.Log($"{d}", logToConsole, logToFile);

        // Diagonals
        var diagonals = CheckDiagonals(matrix);
        Utils.Info($"Diagonals: {diagonals}", logToConsole, logToFile);
        total += diagonals;

        // Swap Matrix
        Utils.Log($"Swapped", logToConsole, logToFile);
        var swappedMatrix = SwapMatrix(lines);
        Utils.PrintMatrix(swappedMatrix, logToConsole, logToFile);

        Utils.Log($"", logToConsole, logToFile);

        // Diagonals
        var reversedDiagonals = CheckDiagonals(swappedMatrix);
        Utils.Info($"Reversed Diagonals: {reversedDiagonals}", logToConsole, logToFile);
        total += reversedDiagonals;
        
        return total;
    }

    public long CheckDiagonals(string[,] matrix)
    {
        int rowLength = matrix.GetLength(0);
        int r = rowLength;
        int colLength = matrix.GetLength(1);
        int c = colLength;

        var total = 0L;

        var adjecents = new List<string>();

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength ; j++)
            {
                colLength = i;
                
                var k = i;
                var l = j;

                adjecents.Add(matrix[k, l]);
                // while (k < rowLength && l < colLength && (k+1 < rowLength && l+1 < colLength))
                // while (k < rowLength && l < colLength)
                while (k+1 < r && l+1 < c)
                {
                    adjecents.Add(matrix[k+1, l+1]);
                    k++;
                    l++;
                }

                var joined = string.Join("", adjecents);
                if (joined.Length >= 4)
                {
                    var line = string.Join("", adjecents);
                    var reversed = string.Join("", line.Reverse());
                    var rtol = line.Contains("XMAS");
                    var ltor = reversed.Contains("XMAS");
                    
                    if (rtol) total += 1;
                    if (ltor) total += 1;

                    // if (rtol || ltor) {
                        Utils.Log($"{joined}", logToConsole, logToFile);
                        Utils.Log($"{rtol} | {ltor}", logToConsole, logToFile);
                    // }
                }
                
                adjecents = new List<string>();
            }

            // rowLength -= (colLength-i);
            Utils.Log($"colLength{colLength} | r{r} | i{i}", logToConsole, logToFile);
            colLength = c;
            Utils.Log($"colLength{colLength} | r{r} | i{i}", logToConsole, logToFile);
            // [1,0] [1,1] [1,2]
            // 10 -= 10-1 = 1
            // 10 -= 10-2 = 2
            // On next row start from 0 and only go column length - row length
        }        
        
        return total;
    }

    public string[,] SwapMatrix(string[] lines)
    {
        var linesReversed = new List<string>();
        foreach(var line in lines)
        {
            var reversed = string.Join("", line.Reverse());
            // Utils.Log($"{reversed}", logToConsole, logToFile);
            linesReversed.Add(reversed);
        }

        var linesArray = linesReversed.ToArray();
        var rows = linesArray.Length;
        var cols = linesArray[0].Length;
        var matrix = Utils.GenerateMatrix<string>(linesArray, rows, cols);
        return matrix;
    }
}

Utils.Log("-- Day 4 --", true, true);
Utils.Log("-----------", true, true);

var day4 = new Day4();

string fileName = @"input-sample.txt";
// string fileName = @"input.txt";
var lines = Utils.GetLines(fileName);

// Part 1
Utils.Log("Part 1", true, true);
day4.Part1(lines);

// Part 2
// Utils.Log("Part 2", true, true);
// day4.Part2(lines);

Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();