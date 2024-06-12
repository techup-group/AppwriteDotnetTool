using Helpers;

namespace HelpersTests;

public class CommandLineArgumentParserTests
{
  [Fact]
  public void HasArgument_AllLowercaseArgumentName_ReturnsTrue()
  {
    // Arrange
    var args = new[] { "--test" };
    var argumentName = "--test";

    // Act
    var result = CommandLineArgumentParser.HasArgument(args, argumentName);

    // Assert
    Assert.True(result);
  }

  [Fact]
  public void HasArgument_AllUppercaseArgumentName_ReturnsTrue()
  {
    // Arrange
    var args = new[] { "--TEST" };
    var argumentName = "--test";

    // Act
    var result = CommandLineArgumentParser.HasArgument(args, argumentName);

    // Assert
    Assert.True(result);
  }

  [Fact]
  public void HasArgument_MixedCaseArgumentName_ReturnsTrue()
  {
    // Arrange
    var args = new[] { "--TeSt" };
    var argumentName = "--test";

    // Act
    var result = CommandLineArgumentParser.HasArgument(args, argumentName);

    // Assert
    Assert.True(result);
  }

  [Fact]
  public void HasArgument_ArgumentNameNotFound_ReturnsFalse()
  {
    // Arrange
    var args = new[] { "--other" };
    var argumentName = "--test";

    // Act
    var result = CommandLineArgumentParser.HasArgument(args, argumentName);

    // Assert
    Assert.False(result);
  }

  [Fact]
  public void HasArgument_ArgumentNameWithSingleHyphen_ReturnsTrue()
  {
    // Arrange
    var args = new[] { "-test" };
    var argumentName = "-test";

    // Act
    var result = CommandLineArgumentParser.HasArgument(args, argumentName);

    // Assert
    Assert.True(result);
  }

  [Fact]
  public void HasArgument_ArgumentNameWithDoubleHyphens_ReturnsTrue()
  {
    // Arrange
    var args = new[] { "--test" };
    var argumentName = "--test";

    // Act
    var result = CommandLineArgumentParser.HasArgument(args, argumentName);

    // Assert
    Assert.True(result);
  }

  [Fact]
  public void HasArgument_ArgumentNameWithoutHyphens_ReturnsTrue()
  {
    // Arrange
    var args = new[] { "test" };
    var argumentName = "test";

    // Act
    var result = CommandLineArgumentParser.HasArgument(args, argumentName);

    // Assert
    Assert.True(result);
  }

  [Fact]
  public void GetArgumentValue_ArgumentNameExists_ReturnsValue()
  {
    // Arrange
    var args = new[] { "--test=value" };
    var argumentName = "--test";

    // Act
    var result = CommandLineArgumentParser.GetArgumentValue(args, argumentName);

    // Assert
    Assert.Equal("value", result);
  }

  [Fact]
  public void GetArgumentValue_ArgumentNameDoesNotExist_ReturnsEmptyString()
  {
    // Arrange
    var args = new[] { "--other=value" };
    var argumentName = "--test";

    // Act
    var result = CommandLineArgumentParser.GetArgumentValue(args, argumentName);

    // Assert
    Assert.Equal("", result);
  }
}