using System.Collections.Generic;
using System.Xml.Serialization;

namespace TiledCommandRunner.Xml
{
  [XmlRoot(ElementName = "properties")]
  public class PropertyGroup
  {
    [XmlElement(ElementName = "property")]
    public List<Property> Properties { get; set; }
  }
}
