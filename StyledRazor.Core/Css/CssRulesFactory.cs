using StyledRazor.Core.Utils;

namespace StyledRazor.Core.Style;

internal static class CssRulesFactory
{
  public static CssRules Create(string baseCss, string scope = "") => 
    CssRules.Deserialize(baseCss.AddScope(scope));
}