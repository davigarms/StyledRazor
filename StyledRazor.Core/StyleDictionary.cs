using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using static StyledRazor.Core.Utils.Css;

namespace StyledRazor.Core;

public class StyleDictionary : Dictionary<string, StyleProperties>
{
  public event UpdateHandler OnUpdate;

  public delegate void UpdateHandler();
  
  private void InvokeUpdate() => OnUpdate?.Invoke();

  private static readonly JsonSerializerOptions JsonOptions = new()
  {
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
  };
  
  public StyleProperties Get(string selector) => this.First(x => x.Key.Contains(selector)).Value;

  public new string ToString() => Serialize(false);

  public string Serialize() => Serialize(true);

  public string Serialize(bool restrictScope)
  {
    var baseElement = restrictScope ? Keys.ToArray()[0] + " " : "";
    var json = JsonSerializer.Serialize(this, JsonOptions);
    return JsonToCss(json, baseElement);
  }

  public StyleDictionary Deserialize(string css)
  {
    if (string.IsNullOrEmpty(css)) return null;

    var json = CssToJson(css);
    var styleDictionary = JsonSerializer.Deserialize<StyleDictionary>(json);

    if (styleDictionary == null) return null;
    
    foreach (var style in styleDictionary.Values)
      style.OnUpdate += InvokeUpdate;
    
    return styleDictionary;
  }
}