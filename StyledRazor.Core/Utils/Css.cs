namespace StyledRazor.Core.Utils;

public static class Css
{

  public static string AddScope(this string css, string scope = "")
  {
    css = css.Trim()
      .Insert(0, $"{scope}")
      .Replace("}", $"}}\n{scope}")
      .Replace("\r", "")
      .Replace("\n\n", "")
      .Replace($"{scope}", scope);

    return css
      .Insert(css.Length, "\n")
      .Replace($"{scope}\n", "");
  }
  
  
  public static string Minify(this string css)
  {
    css = css
      .Replace("  ", "")
      .Replace("\r", "\n")
      .Replace(" \n", "\n")
      .Replace("\t", "")
      .Replace(": ", ":")
      .Replace(" {", "{")
      .Replace(" }", "}")
      .Replace(" > ", ">")
      .Replace("\n", "");

    return css;
  }
  
  public static string ToJson(this string css)
  {
    var json = css.Insert(css.Length - 1, "}");
    return json.Replace(":", "\":\"").Replace("{", "\":{\"").Replace("}", "\"},\"").Replace(";", "\",\"").Replace(",\"\"", "").Replace(" \":\"", ":").Replace("}},\"", "}}").Insert(0, "{\"");
  }

  public static string ToCss(this string json, string baseElement) => 
    json.Replace("},", $";}}{baseElement}").Replace("\"", "").Replace(",", ";").Replace(":{", "{").Replace("}}", ";}")[1..];
}