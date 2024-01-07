using StyledRazor.Core.Utils;

namespace StyledRazor.Core.Style;

internal static class CssFactory
{
  public static Css Create(string baseCss, string scope = "") => 
    Css.Deserialize(baseCss.AddScope(scope));
}