using CommandLine;

namespace TiledCommandRunner.CommandLine
{
  public class Options
  {
    [Option('f', "file", Required = true, HelpText = "file path")]
    public string FilePath { get; set; }

    [Option('b', "backupFolder", Required = true, HelpText = "backup folder")]
    public string BackupFolder { get; set; }
  }
}
