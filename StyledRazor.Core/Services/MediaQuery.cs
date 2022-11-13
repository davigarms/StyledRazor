using System;
using System.Collections.Generic;
using System.Linq;
using StyledRazor.Core.Model;

namespace StyledRazor.Core.Services;

public class MediaQuery
{
  private const int BreakPointXs = 0;
  private const int BreakPointS = 480;
  private const int BreakPointM = 768;
  private const int BreakPointL = 960;
  private const int BreakPointXl = 1280;
  private const int BreakPointXxl = 1536;

  public static string ScreenXs => $"{BreakPointXs}px";
  public static string ScreenS => $"{BreakPointS}px";
  public static string ScreenM => $"{BreakPointM}px";
  public static string ScreenL => $"{BreakPointL}px";
  public static string ScreenXl => $"{BreakPointXl}px";
  public static string ScreenXxl => $"{BreakPointXxl}px";

  private static readonly BreakPoints BreakPoints = new()
  {
    { "XS", BreakPointXs },
    { "S", BreakPointS },
    { "M", BreakPointM },
    { "L", BreakPointL },
    { "XL", BreakPointXl },
    { "XXL", BreakPointXxl }
  };

  private readonly Dictionary<int, int> _mediaQuery;

  public MediaQuery(BreakPoints responsiveColumns)
  {
    if (responsiveColumns == null) return;

    _mediaQuery = new Dictionary<int, int>();

    foreach (var breakpoint in BreakPoints)
    {
      foreach (var col in responsiveColumns.Where(col => col.Key == breakpoint.Key))
      {
        _mediaQuery.Add(breakpoint.Value, col.Value);
      }
    }
  }

  public int? NumberOfColumnsFor(int windowWidth)
  {
    if (_mediaQuery == null) return null;
    for (var i = 0; i < _mediaQuery.Count - 1; i++)
    {
      var minQuery = _mediaQuery.ElementAt(i);
      var maxQuery = _mediaQuery.ElementAt(i + 1);

      if (windowWidth < minQuery.Key ||
          (windowWidth >= minQuery.Key && windowWidth < maxQuery.Key))
      {
        return minQuery.Value;
      }
    }

    var last = _mediaQuery.Last();

    return windowWidth > last.Key ? last.Value : null;
  }
}