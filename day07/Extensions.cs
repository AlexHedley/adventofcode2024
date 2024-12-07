// All combinations and permutations of a set of characters to a given length in c#
// https://stackoverflow.com/a/76874843/2895831
using System.Linq;

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

// // All solutions as a list
// List<char[]> result = SolveMe(3, "ab").ToList();

// var report = string.Join(", ", result.Select(item => string.Concat(item)));

// Console.Write(report);

// > a, b, aa, ab, ba, bb, aaa, aab, aba, abb, baa, bab, bba, bbb


// https://stackoverflow.com/a/58826787/2895831
public static class PermutationExtension
{
    public static IEnumerable<T[]> Permutations<T>(this IEnumerable<T> source)
    {
        var sourceArray = source.ToArray();
        var results = new List<T[]>();
        Permute(sourceArray, 0, sourceArray.Length - 1, results);
        return results;
    }

    private static void Swap<T>(ref T a, ref T b)
    {
        T tmp = a;
        a = b;
        b = tmp;
    }

    private static void Permute<T>(T[] elements, int recursionDepth, int maxDepth, ICollection<T[]> results)
    {
        if (recursionDepth == maxDepth)
        {
            results.Add(elements.ToArray());
            return;
        }

        for (var i = recursionDepth; i <= maxDepth; i++)
        {
            Swap(ref elements[recursionDepth], ref elements[i]);
            Permute(elements, recursionDepth + 1, maxDepth, results);
            Swap(ref elements[recursionDepth], ref elements[i]);
        }
    }
}
