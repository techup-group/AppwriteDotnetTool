using Helpers;

namespace HelpersTests;

public class UserSelectionTests
{
  [Fact]
  public void GetBooleanAnswer_UserEntersY_ReturnsTrue()
  {
    using (var sw = new StringWriter())
    {
      Console.SetOut(sw);
      using (var sr = new StringReader("y"))
      {
        Console.SetIn(sr);
        var result = UserSelection.GetBooleanAnswer("Test message");
        Assert.True(result);
      }
    }
  }

  [Fact]
  public void GetBooleanAnswer_UserEntersN_ReturnsFalse()
  {
    using (var sw = new StringWriter())
    {
      Console.SetOut(sw);
      using (var sr = new StringReader("n"))
      {
        Console.SetIn(sr);
        var result = UserSelection.GetBooleanAnswer("Test message");
        Assert.False(result);
      }
    }
  }

  [Fact]
  public void GetBooleanAnswer_UserEntersYWithWhitespace_ReturnsTrue()
  {
    using (var sw = new StringWriter())
    {
      Console.SetOut(sw);
      using (var sr = new StringReader("  y  "))
      {
        Console.SetIn(sr);
        var result = UserSelection.GetBooleanAnswer("Test message");
        Assert.True(result);
      }
    }
  }
}

