using System.Collections.Generic;
using System.Xml.Serialization;

namespace TiledCommandRunner.Xml
{
  [XmlRoot(ElementName = "objecttypes")]
  public class ObjectTypeGroup
  {
    [XmlElement(ElementName = "objecttype")]
    public List<ObjectType> ObjectTypes { get; set; }
  }
}
