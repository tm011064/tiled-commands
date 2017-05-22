using System;
using TiledCommandRunner.Commands.Expand;
using TiledCommandRunner.Commands.Layers;
using TiledCommandRunner.ConsoleMenu;

namespace TiledCommandRunner.Commands
{
  public class CommandRunnerFactory
  {
    public static ICommandRunner<TContext, TResult, TArgs> Create<TContext, TResult, TArgs>()
    {
      var type = typeof(TContext);

      if (type == typeof(ExpandContext))
      {
        return (ICommandRunner<TContext, TResult, TArgs>)(new ExpandCommand());
      }

      if (type == typeof(CreateLayersContext))
      {
        return (ICommandRunner<TContext, TResult, TArgs>)(new CreateLayersCommand());
      }

      throw new NotImplementedException();
    }
  }
}
