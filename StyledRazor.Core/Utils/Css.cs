namespace StyledRazor.Core.Utils;

public static class Css
{
  public static string Minify(string css, string baseElementId = "")
  {
    css = css
      .Insert(0, $"{baseElementId}")
      .Insert(0, "\n")
      .Replace("  ", "")
      .Replace("\r", "\n")
      .Replace(" \n", "\n")
      .Replace("\t", "")
      .Replace(": ", ":")
      .Replace(" {", "{")
      .Replace(" }", "}")
      .Replace(" > ", ">")
      .Replace("}", $"}}{baseElementId}")
      .Replace("\n", "");

    return css
      .Insert(css.Length, "\n")
      .Replace($"{baseElementId}\n", "");
  }
  
  public static string CssToJson(string css)
  {
    var json = css.Insert(css.Length - 1, "}");
    return json.Replace(":", "\":\"").Replace("{", "\":{\"").Replace("}", "\"},\"").Replace(";", "\",\"").Replace(",\"\"", "").Replace(" \":\"", ":").Replace("}},\"", "}}").Insert(0, "{\"");
  }

  public static string JsonToCss(string json, string baseElement) => 
    json.Replace("},", $";}}{baseElement}").Replace("\"", "").Replace(",", ";").Replace(":{", "{").Replace("}}", ";}")[1..];
}