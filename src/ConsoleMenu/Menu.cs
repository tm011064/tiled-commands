using System;
using System.Collections.Generic;
using System.Linq;

namespace TiledCommandRunner.ConsoleMenu
{
  public class Menu<TArgs>
  {
    private Page<TArgs> _titlePage;

    private TArgs _args;

    public void Show()
    {
      _titlePage.Show();
    }

    public Page<TArgs> TitlePage(string title)
    {
      _titlePage = new Page<TArgs>(title, _args);
      return _titlePage;
    }

    public Menu<TArgs> WithArgs(TArgs args)
    {
      _args = args;
      return this;
    }

    public void Build(Action<Menu<TArgs>> action)
    {
      action(this);
    }
  }
}
