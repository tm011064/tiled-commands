using System.Xml.Serialization;

namespace TiledCommandRunner.Xml
{
  [XmlRoot(ElementName = "polyline")]
  public class PolyLine
  {
    [XmlAttribute(AttributeName = "points")]
    public string Points { get; set; }
  }
}
