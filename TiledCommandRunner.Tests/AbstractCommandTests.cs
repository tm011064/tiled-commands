using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TiledCommandRunner.Commands;

namespace TiledCommandRunner.Tests
{
  [SetUpFixture]
  public class AbstractCommandTests
  {
    protected readonly MapManager MapManager = new MapManager();

    protected string FormatFilePath(string localPath)
    {
      return Path.Combine(
        TestContext.CurrentContext.TestDirectory,
        localPath);
    }
  }
}
