using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using static StyledRazor.Core.Utils.Css;

namespace StyledRazor.Core.Css;

public class CssRulesetDictionary : Dictionary<string, CssDeclarationDictionary>
{
  private static readonly JsonSerializerOptions JsonOptions = new()
  {
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
  };

  public CssDeclarationDictionary Get(string selector)
  {
    var property = TryGetValue(selector, out var declaration);

    if (!property)
    {
      declaration = new CssDeclarationDictionary();
      Add(selector, declaration);
    }

    return declaration;
  }

  public void Set(string selector, CssDeclarationDictionary declaration)
  {
    if (ContainsKey(selector))
    {
      this[selector] = declaration;
    }
    else
    {
      Add(selector, declaration);
    }
  }

  public void Set(string selector, string cssString)
  {
    var declaration = DeserializeDeclaration($"{selector}{cssString}");

    if (ContainsKey(selector))
    {
      this[selector] = declaration;
    }
    else
    {
      Add(selector, declaration);
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

  public static CssRulesetDictionary Deserialize(string cssString)
  {
    if (string.IsNullOrEmpty(cssString)) return null;

    var json = cssString.Minify().ToJson();
    return JsonSerializer.Deserialize<CssRulesetDictionary>(json);
  }

  public static CssDeclarationDictionary DeserializeDeclaration(string cssString)
  {
    if (string.IsNullOrEmpty(cssString)) return null;

    var json = cssString.Minify().ToJson();
    var css = JsonSerializer.Deserialize<CssRulesetDictionary>(json);
    return css?.Values.FirstOrDefault() ?? new CssDeclarationDictionary();
  }
}