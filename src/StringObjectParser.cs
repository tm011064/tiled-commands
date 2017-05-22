using System;

namespace TiledCommandRunner
{
  public class StringObjectParser
  {
    public object Parse(Type type, string text)
    {
      if (type.IsEnum)
      {
        return Enum.Parse(type, text, true);
      }

      switch (type.Name)
      {
        case "String":
          return text;

        case "Int32":
          return int.Parse(text);

        case "Float":
          return float.Parse(text);

        case "Decimal":
          return decimal.Parse(text);
      }

      throw new NotSupportedException("Type " + type.Name + " not supported");
    }
  }
}
