using System.Collections.Generic;
using System.IO;
using TiledCommandRunner.CommandLine;
using TiledCommandRunner.Xml;

namespace TiledCommandRunner.Commands.Expand
{
  public abstract class AbstractExpandStrategy
  {
    protected readonly ExpandContext Context;

    protected readonly Options Options;

    protected AbstractExpandStrategy(
      ExpandContext context,
      Options options)
    {
      Context = context;
      Options = options;
    }

    protected Map LoadMap()
    {
      var mapManager = new MapManager();
      return mapManager.Load(Options.FilePath);
    }

    protected IEnumerable<string> GetLines(string data)
    {
      using (var reader = new StringReader(data))
      {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
          yield return line;
        }
      }
    }
  }
}
