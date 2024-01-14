using System;

namespace StyledRazor.Core.Utils;

public static class Css
{
  /// <summary>
  /// Convert CSS units to float (support for more units to be added).  
  /// </summary>
  /// <param name="value"></param>
  /// <returns>float</returns>
  /// <exception cref="InvalidOperationException"></exception>
  public static float ToInt(this string value)
  {
    if (value.Contains("px"))
      return float.Parse(value.Replace("px", "").Trim());

    if (value.Contains("rem"))
      return float.Parse(value.Replace("rem", "").Trim()) * 16;

    if (value.Contains("em"))
      return float.Parse(value.Replace("em", "").Trim()) * 16;

    throw new InvalidOperationException("value must be in 'px', 'rem' or 'em'."); 
  }

  public static string AddScope(this string cssString, string scope)
  {
    cssString = cssString.Trim()
      .Insert(0, $"{scope}")
      .Replace("}", $"}}\n{scope}")
      .Replace("\r", "")
      .Replace("\n\n", "")
      .Replace($"{scope}", scope);

    return cssString
      .Insert(cssString.Length, "\0")
      .Replace($"{scope}\0", "");
  }
  
  public static string Minify(this string cssString)
  {
    cssString = cssString
      .Replace("  ", "")
      .Replace("\r", "\n")
      .Replace(" \n", "\n")
      .Replace("\t", "")
      .Replace(": ", ":")
      .Replace(" :", ":")
      .Replace(" ;", ";")
      .Replace("; ", ";")
      .Replace(" {", "{")
      .Replace(" > ", ">")
      .Replace("\n", "");

    return cssString;
  }
  
  public static string ToJson(this string cssString)
  {
    var json = cssString.Insert(cssString.Length - 1, "}");
    return json
      .Replace(":", "\":\"")
      .Replace("{", "\":{\"")
      .Replace("}", "\"},\"")
      .Replace(";", "\",\"")
      .Replace(",\"\"", "")
      .Replace(" \":\"", ":")
      .Replace("}},\"", "}}")
      .Insert(0, "{\"");
  }

  public static string ToCss(this string json, string baseElement) => 
    json
      .Replace("},", $";}}{baseElement}")
      .Replace("\"", "")
      .Replace(",", ";")
      .Replace(":{", "{")
      .Replace("}}", ";}")[1..];
  
  public static string SetScope(string componentId, string baseElement = "") => $"{baseElement}[{componentId}]";

  public static string SetId(string name) =>
    $"{(name == null ? "w" : name.ToLower() + "_")}{Guid.NewGuid().ToString().Replace("-", "")[..10]}";
  
}