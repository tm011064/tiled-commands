using System.Text;
using TiledCommandRunner.CommandLine;
using TiledCommandRunner.Xml;

namespace TiledCommandRunner.Commands.Expand
{
  public class ExpandRightStrategy : AbstractExpandStrategy
  {
    public ExpandRightStrategy(ExpandContext context, Options options)
      : base(context, options)
    {
    }

    public Map Expand()
    {
      var map = LoadMap();

      var expandedWidth = (Context.Pixels / map.TileWidth);

      foreach (var layer in map.Layers)
      {
        layer.Width += expandedWidth;
        layer.Data.Text = ExpandLayerData(map, layer.Data.Text);
      }

      map.Width = map.Width + expandedWidth;

      return map;
    }

    private string ExpandLayerData(Map map, string data)
    {
      var numberOfItems = Context.Pixels / map.TileWidth;

      var expansion = string.Join(",", new int[numberOfItems]);

      var expanded = new StringBuilder();

      expanded.Append('\n');

      foreach (var line in GetLines(data.Trim() + ','))
      {
        expanded.Append(line);
        expanded.Append(expansion);
        expanded.Append(',');
        expanded.Append('\n');
      }

      expanded.Remove(expanded.Length - 2, 1);

      return expanded.ToString();
    }
  }
}
