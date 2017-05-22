﻿using System.Xml.Serialization;

namespace TiledCommandRunner.Xml
{
  [XmlRoot(ElementName = "object")]
  public class TiledObject
  {
    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "type")]
    public string Type { get; set; }

    [XmlAttribute(AttributeName = "x")]
    public int X { get; set; }

    [XmlAttribute(AttributeName = "y")]
    public int Y { get; set; }

    [XmlAttribute(AttributeName = "width")]
    public int Width { get; set; }

    [XmlAttribute(AttributeName = "height")]
    public int Height { get; set; }

    [XmlElement(ElementName = "gid", IsNullable = true)]
    public long? Gid { get; set; }

    [XmlElement(ElementName = "properties")]
    public PropertyGroup PropertyGroup { get; set; }

    [XmlElement(ElementName = "polyline")]
    public PolyLine PolyLine { get; set; }
  }
}
