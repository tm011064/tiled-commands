using System.IO;
using System.Xml.Serialization;
using TiledCommandRunner.Xml;

namespace TiledCommandRunner.Commands
{
  public class MapManager
  {
    public Map Load(string filePath)
    {
      var mapSerializer = new XmlSerializer(typeof(Map));

      using (var reader = new StreamReader(filePath))
      {
        return (Map)mapSerializer.Deserialize(reader);
      }
    }

    public void Save(Map map, string filePath)
    {
      var mapSerializer = new XmlSerializer(typeof(Map));

      var xmlNameSpaces = new XmlSerializerNamespaces();
      xmlNameSpaces.Add("", "");

      using (var memoryStream = new MemoryStream())
      using (var streamWriter = new StreamWriter(memoryStream, System.Text.Encoding.UTF8))
      {
        mapSerializer.Serialize(streamWriter, map, xmlNameSpaces);

        File.WriteAllBytes(filePath, memoryStream.ToArray());
      }
    }
  }
}
