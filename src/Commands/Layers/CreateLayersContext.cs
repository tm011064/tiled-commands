namespace TiledCommandRunner.Commands.Layers
{
  public enum CreateLayerType
  {
    CameraModifier,
    TileLayers,
    FullScreenScrollers,
    Checkpoints,
    SaveGameAreas,
    WorldSwitchPortals,
    FrontPortals,
    SceneTransitionDoors,
    Items,
    HouseDoors,
    All
  }

  public enum UniverseType
  {
    Real,
    Alternate,
    RealAndAlternate,
    Global
  }

  public enum Universe
  {
    RealWorld,
    AlternateWorld,
    Global
  }

  public class CreateLayersContext
  {
    public CreateLayerType Type { get; set; }

    public UniverseType Universe { get; set; }
  }
}
