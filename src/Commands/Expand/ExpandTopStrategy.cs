using System.Linq;
using System.Text;
using TiledCommandRunner.CommandLine;
using TiledCommandRunner.Xml;

namespace TiledCommandRunner.Commands.Expand
{
  public class ExpandTopStrategy : AbstractExpandStrategy
  {
    public ExpandTopStrategy(ExpandContext context, Options options)
      : base(context, options)
    {
    }

    public Map Expand()
    {
      var map = LoadMap();

      var expandedHeight = (Context.Pixels / map.TileHeight);

      foreach (var layer in map.Layers)
      {
        layer.Height += expandedHeight;
        layer.Data.Text = ExpandLayerData(map, layer.Data.Text);
      }

      map.Height = map.Height + expandedHeight;

      foreach (var tiledObject in map.ObjectGroups.SelectMany(g => g.Objects))
      {
        tiledObject.Y += Context.Pixels;
      }

      return map;
    }

    private string ExpandLayerData(Map map, string data)
    {
      var expansion = string.Join(",", new int[map.Width]) + ",\n";

      var numberOfRowsToAdd = Context.Pixels / map.TileHeight;

      var expanded = new StringBuilder();

      expanded.Append('\n');

      for (var i = 0; i < numberOfRowsToAdd; i++)
      {
        expanded.Append(expansion);
      }

      return expanded.ToString() + data.TrimStart();
    }
  }
}
