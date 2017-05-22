using System;
using TiledCommandRunner.ConsoleMenu;
using TiledCommandRunner.Xml;
using TiledCommandRunner.CommandLine;

namespace TiledCommandRunner.Commands.Expand
{
  public class ExpandCommand : ICommandRunner<ExpandContext, Map, Options>
  {
    public Map Run(ExpandContext context, Options options)
    {
      switch (context.Direction)
      {
        case Direction.Left:
          return new ExpandLeftStrategy(context, options).Expand();

        case Direction.Right:
          return new ExpandRightStrategy(context, options).Expand();

        case Direction.Top:
          return new ExpandTopStrategy(context, options).Expand();

        case Direction.Bottom:
          return new ExpandBottomStrategy(context, options).Expand();
      }

      throw new NotSupportedException("Direction " + context.Direction + " not supported");
    }
  }
}
