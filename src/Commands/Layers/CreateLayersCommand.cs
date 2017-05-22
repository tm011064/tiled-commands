using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledCommandRunner.CommandLine;
using TiledCommandRunner.ConsoleMenu;
using TiledCommandRunner.Xml;

namespace TiledCommandRunner.Commands.Layers
{
  public class CreateLayersCommand : AbstractCommand, ICommandRunner<CreateLayersContext, Map, Options>
  {
    public Map Run(CreateLayersContext context, Options options)
    {
      var mapManager = new MapManager();
      var map = mapManager.Load(options.FilePath);
      
      map = AddTags(map, context.Type, context.Universe);
      
      Save(map, options);

      return map;
    }

    private Map AddTags(Map map, CreateLayerType layerType, UniverseType universe)
    {
      switch (layerType)
      {
        case CreateLayerType.All:
          var allLayerTypes = Enum
            .GetValues(typeof(CreateLayerType))
            .Cast<CreateLayerType>()
            .Except(new[] { CreateLayerType.All });

          foreach (var enumType in allLayerTypes)
          {
            AddTags(map, enumType, universe);
          }

          return map;

        case CreateLayerType.TileLayers:
          if (universe == UniverseType.Real || universe == UniverseType.RealAndAlternate)
          {
            AddLayerIfNotExists(map, "Real Background", Universe.RealWorld, "Background");
            AddLayerIfNotExists(map, "Real Platforms", Universe.RealWorld, "Platform");
            AddLayerIfNotExists(map, "Real One Way Platforms", Universe.RealWorld, "OneWayPlatform");
          }

          if (universe == UniverseType.Alternate || universe == UniverseType.RealAndAlternate)
          {
            AddLayerIfNotExists(map, "Alternate Background", Universe.AlternateWorld, "Background");
            AddLayerIfNotExists(map, "Alternate Platforms", Universe.AlternateWorld, "Platform");
            AddLayerIfNotExists(map, "Alternate One Way Platforms", Universe.AlternateWorld, "OneWayPlatform");
          }

          if (universe == UniverseType.Global)
          {
            AddLayerIfNotExists(map, "Global Background", Universe.Global, "Background");
            AddLayerIfNotExists(map, "Global Platforms", Universe.Global, "Platform");
            AddLayerIfNotExists(map, "Global One Way Platforms", Universe.Global, "OneWayPlatform");
          }

          return map;

        case CreateLayerType.CameraModifier:
          return AddObjectGroupsForUniverse(map, "Camera Modifier", universe, "CameraModifier");

        case CreateLayerType.Checkpoints:
          return AddObjectGroupsForUniverse(map, "Checkpoints", universe);

        case CreateLayerType.FrontPortals:
          return AddObjectGroupsForUniverse(map, "Front Portals", universe);

        case CreateLayerType.FullScreenScrollers:
          return AddObjectGroupsForUniverse(map, "Full Screen Scrollers", universe);

        case CreateLayerType.HouseDoors:
          return AddObjectGroupsForUniverse(map, "House Doors", universe);

        case CreateLayerType.Items:
          return AddObjectGroupsForUniverse(map, "Items", universe);

        case CreateLayerType.SaveGameAreas:
          return AddObjectGroupsForUniverse(map, "Save Game Areas", universe);

        case CreateLayerType.SceneTransitionDoors:
          return AddObjectGroupsForUniverse(map, "Scene Transition Doors", universe);

        case CreateLayerType.WorldSwitchPortals:
          return AddObjectGroupsForUniverse(map, "World Switch Portals", universe);
      }

      throw new NotSupportedException(layerType.ToString());
    }

    private Map AddObjectGroupsForUniverse(Map map, string name, UniverseType universeType, string type = "PrefabGroup")
    {
      switch (universeType)
      {
        case UniverseType.Alternate:
          AddObjectGroupIfNotExists(map, UniverseType.Alternate.ToString() + " " + name, Universe.AlternateWorld, type);
          break;

        case UniverseType.Real:
          AddObjectGroupIfNotExists(map, UniverseType.Real.ToString() + " " + name, Universe.RealWorld, type);
          break;

        case UniverseType.Global:
          AddObjectGroupIfNotExists(map, UniverseType.Global.ToString() + " " + name, Universe.Global, type);
          break;

        case UniverseType.RealAndAlternate:
          AddObjectGroupIfNotExists(map, UniverseType.Alternate.ToString() + " " + name, Universe.AlternateWorld, type);
          AddObjectGroupIfNotExists(map, UniverseType.Real.ToString() + " " + name, Universe.RealWorld, type);
          break;
      }

      return map;
    }

    private void AddObjectGroupIfNotExists(
      Map map,
      string name,
      Universe universe,
      string type = "PrefabGroup",
      string commands = "DestroyPrefab")
    {
      if (map.ObjectGroups.Any(og => string.Equals(og.Name, name, StringComparison.OrdinalIgnoreCase)))
      {
        return;
      }

      var group = new ObjectGroup();

      group.Name = name;
      group.PropertyGroup = new PropertyGroup();
      group.PropertyGroup.Properties = new List<Property>();
      group.PropertyGroup.Properties.Add(new Property { Name = "Universe", Value = universe.ToString() });

      if (!string.IsNullOrWhiteSpace(commands))
      {
        group.PropertyGroup.Properties.Add(new Property { Name = "Commands", Value = commands });
      }

      if (!string.IsNullOrWhiteSpace(type))
      {
        group.PropertyGroup.Properties.Add(new Property { Name = "Type", Value = type });
      }

      map.ObjectGroups.Add(group);
    }

    private void AddLayerIfNotExists(
      Map map,
      string name,
      Universe universe,
      string type,
      string commands = null)
    {
      if (map.Layers.Any(og => string.Equals(og.Name, name, StringComparison.OrdinalIgnoreCase)))
      {
        return;
      }

      var layer = new Layer();

      layer.Name = name;
      layer.PropertyGroup = new PropertyGroup();
      layer.PropertyGroup.Properties = new List<Property>();
      layer.PropertyGroup.Properties.Add(new Property { Name = "Universe", Value = universe.ToString() });
      layer.PropertyGroup.Properties.Add(new Property { Name = "Type", Value = type });

      if (!string.IsNullOrWhiteSpace(commands))
      {
        layer.PropertyGroup.Properties.Add(new Property { Name = "Commands", Value = commands });
      }

      map.Layers.Add(layer);
    }
  }
}
