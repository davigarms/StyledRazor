using System;
using System.Collections.Generic;
using System.Linq;

namespace StyledRazor.Core.MediaQuery;

public enum BreakPoint
{
  Xs = 0,
  Sm = 480,
  Md = 768,
  Lg = 960,
  Xl = 1280,
  Xxl = 1536,
}

public class Columns
{
  private readonly Dictionary<BreakPoint, int> _mediaQuery = new();

  public Columns(int xs = 0,
                 int sm = 0,
                 int md = 0,
                 int lg = 0,
                 int xl = 0,
                 int xxl = 0
  )
  {
    int[] breakpoints = { xs, sm, md, lg, xl, xxl };
    var index = 0;

    foreach (BreakPoint breakPoint in Enum.GetValues(typeof(BreakPoint)))
    {
      var value = breakpoints[index];
      if (value > 0) _mediaQuery.Add(breakPoint, value);
      index++;
    }
  }

  public static Columns Set(int xs = 0,
                            int sm = 0,
                            int md = 0,
                            int lg = 0,
                            int xl = 0,
                            int xxl = 0
  ) => new(xs, sm, md, lg, xl, xxl);

  public int? NumberOfColumnsFor(int windowWidth)
  {
    if (_mediaQuery.Count == 0) return null;

    for (var i = 0; i < _mediaQuery.Count - 1; i++)
    {
      var minWidth = _mediaQuery.ElementAt(i);
      var maxWidth = _mediaQuery.ElementAt(i + 1);

      if (windowWidth < (int)minWidth.Key ||
          windowWidth >= (int)minWidth.Key && windowWidth < (int)maxWidth.Key)
      {
        return minWidth.Value;
      }
    }

    var lastWidth = _mediaQuery.Last();
    return windowWidth > (int)lastWidth.Key ? lastWidth.Value : null;
  }
}