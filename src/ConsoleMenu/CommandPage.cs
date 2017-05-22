using System;
using System.Linq;
using System.Reflection;

namespace TiledCommandRunner.ConsoleMenu
{
  public class CommandPage<TContext, TResult, TArgs> : IPage
  {
    private readonly string _indent;

    private readonly string _separator;

    private readonly TArgs _args;

    public CommandPage(
      IPage parent,
      ICommandRunner<TContext, TResult, TArgs> commandRunner,
      ConsoleKey key,
      string title,
      TArgs args,
      string optionText = null,
      string indent = "  ",
      string separator = ": ")
    {
      _indent = indent;
      _separator = separator;
      _args = args;

      Title = title;
      Key = key;
      CommandRunner = commandRunner;
      OptionText = string.IsNullOrWhiteSpace(optionText) ? title : optionText;
      Parent = parent;
    }

    public string Title { get; private set; }

    public string OptionText { get; private set; }

    public ConsoleKey Key { get; private set; }

    public ICommandRunner<TContext, TResult, TArgs> CommandRunner { get; private set; }

    public IPage Parent { get; private set; }

    public void Show()
    {
      Console.Clear();

      Console.WriteLine("Define settings:");
      Console.Write(Environment.NewLine);

      var options = CreateContext();

      Console.Write(Environment.NewLine);
      Console.Write("Press [Enter] to run or [Esc] to return: ");

      var key = Console.ReadKey();

      if (key.Key == ConsoleKey.Escape)
      {
        Parent.Show();
        return;
      }

      CommandRunner.Run(options, _args);
    }

    private TContext CreateContext()
    {
      var options = Activator.CreateInstance<TContext>();
      var parser = new StringObjectParser();

      var properties = typeof(TContext)
        .GetProperties(BindingFlags.Public | BindingFlags.Instance);

      foreach (var property in properties)
      {
        Console.Write(_indent);
        Console.Write(GetName(property));
        Console.Write(_separator);

        var input = ReadInput(property);

        var value = parser.Parse(property.PropertyType, input);

        property.SetValue(options, value);
      }

      return options;
    }

    private string ReadInput(PropertyInfo property)
    {
      if (!property.PropertyType.IsEnum)
      {
        return Console.ReadLine();
      }

      var cursorPos = new { Left = Console.CursorLeft, Top = Console.CursorTop };
      var names = Enum.GetNames(property.PropertyType).OrderBy(n => n).ToArray();
      var selectedName = names.First();
      var previousSelectedName = selectedName;
      var index = 0;

      Console.Write(selectedName);

      ConsoleKeyInfo key;
      while ((key = Console.ReadKey()).Key != ConsoleKey.Enter)
      {
        Console.SetCursorPosition(cursorPos.Left, cursorPos.Top);

        switch (key.Key)
        {
          case ConsoleKey.UpArrow:
            index = Math.Max(0, index - 1);
            previousSelectedName = selectedName;
            selectedName = names[index];
            break;

          case ConsoleKey.DownArrow:
            index = Math.Min(names.Length - 1, index + 1);
            previousSelectedName = selectedName;
            selectedName = names[index];
            break;

          default:
            var nameStartingWithKey = names.FirstOrDefault(
              n => n.StartsWith(key.KeyChar.ToString(), StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(nameStartingWithKey))
            {
              index = Array.IndexOf(names, nameStartingWithKey);

              previousSelectedName = selectedName;
              selectedName = names[index];
            }

            Console.Write(' ');
            break;
        }

        Console.Write(Enumerable.Repeat(' ', previousSelectedName.Count()).ToArray());

        Console.SetCursorPosition(cursorPos.Left, cursorPos.Top);
        Console.Write(selectedName);
      }

      Console.Write(Environment.NewLine);
      return selectedName;
    }

    private string GetName(PropertyInfo property)
    {
      if (property.PropertyType.IsEnum)
      {
        return property.Name + " [" + string.Join(", ", Enum.GetNames(property.PropertyType).OrderBy(n => n)) + "]";
      }

      return property.Name + " [" + property.PropertyType.Name + "]";
    }
  }
}
