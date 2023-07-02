using StyledRazor.Core.Utils;

namespace StyledRazor.Core.Style;

internal static class StyleFactory
{
  public static StyleDictionary Create(string baseCss, string scope = "") => 
    StyleDictionary.Deserialize(baseCss.AddScope(scope));
}