using System.Xml.Serialization;

namespace TiledCommandRunner.Xml
{
  [XmlRoot(ElementName = "property")]
  public class Property
  {
    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "value")]
    public string Value { get; set; }
  }
}
