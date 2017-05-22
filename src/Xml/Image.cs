using System.Xml.Serialization;

namespace TiledCommandRunner.Xml
{
  [XmlRoot(ElementName = "image")]
  public class Image
  {
    [XmlAttribute(AttributeName = "source")]
    public string Source { get; set; }

    [XmlAttribute(AttributeName = "width")]
    public int Width { get; set; }

    [XmlAttribute(AttributeName = "height")]
    public int Height { get; set; }
  }
}
