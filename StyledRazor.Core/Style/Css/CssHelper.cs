using System;
using System.Linq;
using static System.String;

namespace StyledRazor.Core.Style.Css;

public static class CssHelper
{
  /// <summary>
  /// Convert CSS units to float (support for more units to be added).  
  /// </summary>
  /// <param name="value"></param>
  /// <returns>int</returns>
  /// <exception cref="InvalidOperationException"></exception>
  public static int ToInt(this string value)
  {
    var unit = CssUnit.ValidUnits.FirstOrDefault(x => value.Contains((string)x.Name));
    var validNames = $"\n- {Join("\n- ", CssUnit.ValidNames)}";

    if (unit is null)
      throw new InvalidOperationException($"Css value must be in one of the following units:{validNames}");

    return value.ConvertFrom(unit);
  }

  private static int ConvertFrom(this string value, CssUnit unit) =>
    (int)Math.Round(double.Parse(value.Replace(unit.Name, Empty).Trim()) * unit.Modifier);

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

  public static string Minify(this string unminifiedCss)
  {
    while (unminifiedCss.Contains("/*") && unminifiedCss.Contains("*/"))
    {
      var start = unminifiedCss.IndexOf("/*");
      var end = unminifiedCss.IndexOf("*/") + 2;
      unminifiedCss = unminifiedCss.Remove(start, end);
    }
    
    return unminifiedCss
           .Replace("  ", "")
           .Replace("\r", "\n")
           .Replace(" \n", "\n")
           .Replace("\t", "")
           .Replace(" : ", ": ")
           .Replace(" ;", ";")
           .Replace("; ", ";")
           .Replace(" {", "{")
           .Replace(" > ", ">")
           .Replace("\n", "");
  }

  public static string ToJson(this string minifiedCss)
  {
    var json = minifiedCss.Insert(minifiedCss.Length - 1, "}");
    return json
      .Replace(": ", "\": \"")
      .Replace("{", "\":{\"")
      .Replace("}", "\"},\"")
      .Replace(";", "\",\"")
      .Replace(",\"\"", "")
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

  public static string ScopeFrom(string componentId, string baseElement = "") => $"{baseElement}[{componentId}]";

  public static string IdFrom(string name) =>
    $"{(name == null ? "w" : name.ToLower() + "_")}{Guid.NewGuid().ToString().Replace("-", "")[..10]}";
}