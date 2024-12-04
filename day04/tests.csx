#load "nuget:ScriptUnit, 0.2.0"
#r "nuget:FluentAssertions, 6.12.2"

#load "../utils/utils.csx"
#load "solution.csx"

using static ScriptUnit;
using FluentAssertions;

return await AddTestsFrom<Day4Tests>().Execute();

public class Day4Tests : IDisposable
{
    public Day4 day4;

    public Day4Tests()
    {
        day4 = new Day4();
    }

    public void Dispose() { }

    // public void Success()
    // {
    //     "Ok".Should().Be("Ok");
    // }

    // public void Fail()
    // {
    //     "Ok".Should().NotBe("Ok");
    // }

    // public void FindXMAS()
    // {
    //     var input = new List<long>() { 7, 6, 4, 2, 1 };
    //     var result = day4.FindXMAS(input);
    //     result.Should().Be(true);
    // }

    // CheckDiagonals
    // SwapMatrix

}