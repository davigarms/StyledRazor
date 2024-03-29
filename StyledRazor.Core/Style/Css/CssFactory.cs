namespace StyledRazor.Core.Style.Css;

public static class CssFactory
{
  public static CssRuleset Create(string baseCss, string scope = "") =>
    CssRuleset.Deserialize(baseCss.AddScope(scope));
}