using TiledCommandRunner.ConsoleMenu;
using TiledCommandRunner.Xml;

namespace TiledCommandRunner.Commands
{
  public class RunContext<TOptions> : IRunContext
  {
    public RunContext(Map map, TOptions options)
    {
      Map = map;
      Options = options;
    }

    public Map Map { get; set; }

    public TOptions Options { get; set; }
  }
}
