using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using static StyledRazor.Core.Utils.Css;

namespace StyledRazor.Core.Style;

public class StyleDictionary : Dictionary<string, StyleProperties>
{
  private static readonly JsonSerializerOptions JsonOptions = new()
  {
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
  };

  public StyleProperties Get(string selector)
  {
    var property = this.FirstOrDefault(x => x.Key.Contains(selector));
    if (property.Value != null) return property.Value;
    throw new NullReferenceException($"'{selector}' selector not found");
  }

  public new string ToString() => Serialize(false);

  public string Serialize() => Serialize(true);

  public string Serialize(bool restrictScope)
  {
    var baseElement = restrictScope ? Keys.ToArray()[0] + " " : "";
    var json = JsonSerializer.Serialize(this, JsonOptions);
    return json.ToCss(baseElement);
  }

  public static StyleDictionary Deserialize(string css)
  {
    if (string.IsNullOrEmpty(css)) return null;

    var json = css.Minify().ToJson();
    return JsonSerializer.Deserialize<StyleDictionary>(json);
  }
}