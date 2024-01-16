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
    var propertyExists = TryGetValue(selector, out var declaration);
    return propertyExists ? declaration : CreateEmptyDeclarationFor(selector);
  }
  
  private CssDeclarationDictionary CreateEmptyDeclarationFor(string selector)
  {
    var declaration = new CssDeclarationDictionary();
    Add(selector, declaration);
    return declaration;
  }

  public void Set(string selector, CssDeclarationDictionary declaration)
  {
    if (declaration.Count == 0) return;
    
    if (ContainsKey(selector))
      this[selector] = declaration;
    else
      Add(selector, declaration);
  }

  public void Set(string selector, string declarationString)
  {
    if (string.IsNullOrEmpty(declarationString)) return;
    
    var declaration = DeserializeCssDeclaration($"{selector}{declarationString}");

    if (ContainsKey(selector))
      this[selector] = declaration;
    else
      Add(selector, declaration);
  }

  public new string ToString() => Serialize(false);

  public string Serialize() => Serialize(true);

  public string Serialize(bool scoped)
  {
    var baseElement = scoped ? Keys.ToArray()[0] + " " : "";
    var json = JsonSerializer.Serialize(this, JsonOptions);
    return Count == 0 ? string.Empty : json.ToCss(baseElement);
  }

  public static CssRulesetDictionary Deserialize(string cssString)
  {
    if (string.IsNullOrEmpty(cssString)) return new CssRulesetDictionary();

    var json = cssString.Minify().ToJson();

    try
    {
      return JsonSerializer.Deserialize<CssRulesetDictionary>(json);
    }
    catch (JsonException e)
    {
      throw new JsonException($"{json} \n\n Exception: {e}");
    }
  }
  
  public static CssDeclarationDictionary DeserializeCssDeclaration(string cssRule)
  {
    if (string.IsNullOrEmpty(cssRule)) return new CssDeclarationDictionary();

    var json = cssRule.Minify().ToJson();
    var cssRuleset = JsonSerializer.Deserialize<CssRulesetDictionary>(json);
    return cssRuleset?.Values.FirstOrDefault() ?? new CssDeclarationDictionary();
  }
}