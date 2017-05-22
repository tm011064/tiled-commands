using System;
using CommandLine;
using TiledCommandRunner.CommandLine;
using TiledCommandRunner.Commands;
using TiledCommandRunner.Commands.Expand;
using TiledCommandRunner.Commands.Layers;
using TiledCommandRunner.ConsoleMenu;
using TiledCommandRunner.Xml;

namespace TiledCommandRunner
{
  class Program
  {
    static void Main(string[] args)
    {
      var options = new Options();

      Parser.Default.ParseArguments(args, options);

      if (options == null)
      {
        Console.WriteLine(@"Please provide a file path (app --f=""c:\folder\file.tmx"")");
        Console.ReadKey();
        return;
      }

      var menu = new Menu<Options>();

      menu.Build(m => m
        .WithArgs(options)
        .TitlePage("choose command [File='" + options.FilePath + "']")
            .WithCommandOption<ExpandContext, Map>(ConsoleKey.D1, "expand canvas")
            .WithCommandOption<CreateLayersContext, Map>(ConsoleKey.D2, "create layers"));

      menu.Show();
    }
  }
}
