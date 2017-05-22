using NUnit.Framework;
using TiledCommandRunner.CommandLine;
using TiledCommandRunner.Commands.Expand;

namespace TiledCommandRunner.Tests
{
  [TestFixture]
  public class ExpandCommandTests : AbstractCommandTests
  {
    private ExpandCommand _subject;

    private Options _options;

    [SetUp]
    public void SetUp()
    {
      _subject = new ExpandCommand();
      _options = new Options { FilePath = @"Files\map.tmx" };
    }

    [Test]
    public void ExpandLeft()
    {
      var context = new ExpandContext
      {
        Direction = Direction.Left,
        Pixels = 384
      };

      _subject.Run(context, _options);
    }

    [Test]
    public void ExpandRight()
    {
      var context = new ExpandContext
      {
        Direction = Direction.Right,
        Pixels = 384
      };

      _subject.Run(context, _options);
    }

    [Test]
    public void ExpandTop()
    {
      var context = new ExpandContext
      {
        Direction = Direction.Top,
        Pixels = 240
      };

      _subject.Run(context, _options);
    }

    [Test]
    public void ExpandBottom()
    {
      var context = new ExpandContext
      {
        Direction = Direction.Bottom,
        Pixels = 240
      };

      _subject.Run(context, _options);
    }
  }
}
