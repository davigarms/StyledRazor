using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using static StyledRazor.Core.Utils.Css;

namespace StyledRazor.Core.Style;

public class Css : Dictionary<string, CssDefinition>
{
  private static readonly JsonSerializerOptions JsonOptions = new()
  {
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
  };

  public CssDefinition Get(string selector)
  {
    var property = TryGetValue(selector, out var definition);
    
    if (!property)
    {
      definition = new CssDefinition();
      Add(selector, definition);
    }
    
    return definition;
  }
  
  public void Set(string selector, CssDefinition definition)
  {
    if (ContainsKey(selector))
    {
      this[selector] = definition;
    }
    else
    {
      Add(selector, definition);
    }
  }
  public void Set(string selector, string cssString)
  {
    var deserializeDefinition = DeserializeDefinition($"{selector}{cssString}");

    if (ContainsKey(selector))
    {
      this[selector] = deserializeDefinition;
    }
    else
    {
      Add(selector, deserializeDefinition);
    }
  }
  
  public new string ToString() => Serialize(false);

  public string Serialize() => Serialize(true);

  public string Serialize(bool restrictScope)
  {
    var baseElement = restrictScope ? Keys.ToArray()[0] + " " : "";
    var json = JsonSerializer.Serialize(this, JsonOptions);
    return json.ToCss(baseElement);
  }

  public static Css Deserialize(string cssString)
  {
    if (string.IsNullOrEmpty(cssString)) return null;

    var json = cssString.Minify().ToJson();
    return JsonSerializer.Deserialize<Css>(json);
  }
  
  public static CssDefinition DeserializeDefinition(string cssString)
  {
    if (string.IsNullOrEmpty(cssString)) return null;

    var json = cssString.Minify().ToJson();
    var css = JsonSerializer.Deserialize<Css>(json);
    return css?.Values.FirstOrDefault() ?? new CssDefinition();
  }
}