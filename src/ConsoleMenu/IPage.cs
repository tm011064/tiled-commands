using System;

namespace TiledCommandRunner.ConsoleMenu
{
  public interface IPage
  {
    string OptionText { get; }

    IPage Parent { get; }

    ConsoleKey Key { get; }

    void Show();
  }
}
