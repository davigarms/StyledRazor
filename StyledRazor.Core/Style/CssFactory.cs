using static StyledRazor.Core.Utils.Css;

namespace StyledRazor.Core.Style;

public static class CssFactory
{
  public static Css Create(string baseCss, string scope = "") => 
    Css.Deserialize(baseCss.AddScope(scope));
}