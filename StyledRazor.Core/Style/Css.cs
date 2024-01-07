using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using StyledRazor.Core.Utils;

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
  
  public void SetCss(string selector, CssDefinition definition)
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
}