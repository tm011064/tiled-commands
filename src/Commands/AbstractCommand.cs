using System;
using System.IO;
using System.Linq;
using TiledCommandRunner.CommandLine;
using TiledCommandRunner.Xml;

namespace TiledCommandRunner.Commands
{
  public abstract class AbstractCommand
  {
    protected void Save(Map map, Options options)
    {
      var mapManager = new MapManager();

      var fileInfo = new FileInfo(options.FilePath);
      var directoryInfo = new DirectoryInfo(options.BackupFolder);

      var backupFileName = GetFileNameWithoutExtension(fileInfo)
        + "." + DateTime.Now.ToString("yyyyMMdd_HHmmss")
        + fileInfo.Extension;

      var backupFilePath = Path.Combine(directoryInfo.FullName, backupFileName);

      File.Copy(options.FilePath, backupFilePath, true);

      map.ObjectGroups = map.ObjectGroups.OrderByDescending(g => g.Name).ToList();
      map.Layers = map.Layers.OrderByDescending(l => l.Name).ToList();

      mapManager.Save(map, options.FilePath);
    }

    private string GetFileNameWithoutExtension(FileInfo fileInfo)
    {
      if (string.IsNullOrWhiteSpace(fileInfo.Extension))
      {
        return fileInfo.Name;
      }

      var extensionIndex = fileInfo.Name.LastIndexOf(fileInfo.Extension);
      if (extensionIndex == 0)
      {
        return string.Empty;
      }

      return fileInfo.Name.Substring(0, extensionIndex);
    }
  }
}
