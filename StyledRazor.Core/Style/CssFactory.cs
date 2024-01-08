using static StyledRazor.Core.Utils.Css;

namespace StyledRazor.Core.Style;

public static class CssFactory
{
  public static CssRulesetDictionary Create(string baseCss, string scope = "") =>
    CssRulesetDictionary.Deserialize(baseCss.AddScope(scope));
}