using System;
using System.Collections.Generic;
using System.Linq;
using TiledCommandRunner.Commands;

namespace TiledCommandRunner.ConsoleMenu
{
  public class Page<TArgs> : IPage
  {
    public static Page<TArgs> Create(string title)
    {
      return new Page<TArgs>(title, default(TArgs));
    }

    private readonly List<IPage> _pages = new List<IPage>();

    private string _indent;

    private string _separator;

    private string _title;

    private TArgs _args;
    
    public Page(
      string title,
      TArgs args,
      string optionText = null,
      string indent = "  ",
      string separator = ". ")
    {
      _args = args;
      _indent = indent;
      _separator = separator;
      _title = title;
      OptionText = string.IsNullOrWhiteSpace(optionText) ? title : optionText;
    }

    public ConsoleKey Key { get; set; }

    public IPage Parent { get; set; }

    public string OptionText { get; set; }

    public void Show()
    {
      Console.Clear();
      Console.WriteLine(_title);

      Console.Write(Environment.NewLine);

      foreach (var page in _pages)
      {
        Console.Write(_indent);
        Console.Write(page.Key.ToString());
        Console.Write(_separator);
        Console.Write(page.OptionText);
        Console.Write(Environment.NewLine);
      }

      Console.Write(Environment.NewLine);

      var escapeText = Parent == null ? "exit" : "go back";
      Console.WriteLine(_indent + "[ESC] - " + escapeText);
      Console.Write(Environment.NewLine);
      Console.Write("choose option: ");

      var key = Console.ReadKey();

      if (key.Key == ConsoleKey.Escape)
      {
        if (Parent == null)
        {
          return;
        }

        Parent.Show();
        return;
      }

      var selectedPage = _pages.FirstOrDefault(p => p.Key == key.Key);
      if (selectedPage != null)
      {
        selectedPage.Show();
        return;
      }

      Show();
    }

    public Page<TArgs> WithIndent(string indent)
    {
      _indent = indent;
      return this;
    }

    public Page<TArgs> WithSeparator(string separator)
    {
      _separator = separator;
      return this;
    }

    public Page<TArgs> WithTitle(string title)
    {
      _title = title;
      return this;
    }

    public Page<TArgs> Build()
    {
      return (Page<TArgs>)Parent;
    }

    public Page<TArgs> WithCommandOption<TContext, TResult>(ConsoleKey key, string title)
    {
      _pages.Add(new CommandPage<TContext, TResult, TArgs>(
        this,
        CommandRunnerFactory.Create<TContext, TResult, TArgs>(),
        key,
        title,
        _args));

      return this;
    }

    public Page<TArgs> WithSubPage(ConsoleKey key, Page<TArgs> page)
    {
      page.Parent = this;
      page.Key = key;
      page._args = _args;

      _pages.Add(page);

      return page;
    }
  }
}
