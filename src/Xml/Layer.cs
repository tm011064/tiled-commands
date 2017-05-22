using System.Xml.Serialization;

namespace TiledCommandRunner.Xml
{
  [XmlRoot(ElementName = "layer")]
  public class Layer
  {
    [XmlElement(ElementName = "data")]
    public Data Data { get; set; }

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "width")]
    public int Width { get; set; }

    [XmlAttribute(AttributeName = "height")]
    public int Height { get; set; }

    [XmlElement(ElementName = "properties")]
    public PropertyGroup PropertyGroup { get; set; }
  }
}
