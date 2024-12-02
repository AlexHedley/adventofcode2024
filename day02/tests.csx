#load "nuget:ScriptUnit, 0.2.0"
#r "nuget:FluentAssertions, 6.12.2"

#load "../utils/utils.csx"
#load "solution.csx"

using static ScriptUnit;
using FluentAssertions;

return await AddTestsFrom<Day2Tests>().Execute();

public class Day2Tests : IDisposable
{
    public Day2 day2;

    public Day2Tests()
    {
        day2 = new Day2();
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

    // [Arguments()]
    // public void CheckLevels(List<long> levels)
    public void CheckLevelsTrue()
    {
        var input = new List<long>() { 7, 6, 4, 2, 1 };
        var result = day2.CheckLevels(input);
        result.Should().Be(true);
    }

    public void CheckLevelsFalse()
    {
        var input = new List<long>() { 1, 2, 7, 8, 9 };
        var result = day2.CheckLevels(input);
        result.Should().Be(false);
    }
}