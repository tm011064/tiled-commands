namespace TiledCommandRunner.ConsoleMenu
{
  public interface ICommandRunner<TContext, TResult, TArgs>
  {
    TResult Run(TContext context, TArgs args);
  }
}
