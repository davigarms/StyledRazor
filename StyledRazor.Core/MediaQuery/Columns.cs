using StyledRazor.Core.Style.DesignTokens;
using System.Collections.Generic;
using System.Linq;

namespace StyledRazor.Core.MediaQuery;

public class Columns
{
  private readonly ITokens _tokens;

  private Dictionary<int, int> _mediaQuery = new();

  public Columns(ITokens tokens)
  {
    _tokens = tokens;
  }

  public Columns Set(int xs = 0,
                     int sm = 0,
                     int md = 0,
                     int lg = 0,
                     int xl = 0,
                     int xxl = 0
  )
  {
    int[] columns = { xs, sm, md, lg, xl, xxl };
    _mediaQuery = new Dictionary<int, int>();
    var breakPoints = new[]
                      {
                        _tokens.BreakPointXs,
                        _tokens.BreakPointSm,
                        _tokens.BreakPointMd,
                        _tokens.BreakPointLg,
                        _tokens.BreakPointXl,
                        _tokens.BreakPointXxl,
                      };

    for (var i = 0; i < breakPoints.Length; i++)
      if (columns[i] > 0)
        _mediaQuery[breakPoints[i]] = columns[i];

    return this;
  }

  public int? NumberOfColumnsFor(int windowWidth)
  {
    if (_mediaQuery.Count == 0) return null;

    for (var i = 0; i < _mediaQuery.Count - 1; i++)
    {
      var minWidth = _mediaQuery.ElementAt(i);
      var maxWidth = _mediaQuery.ElementAt(i + 1);

      if (windowWidth < minWidth.Key ||
          windowWidth >= minWidth.Key && windowWidth < maxWidth.Key)
      {
        return minWidth.Value;
      }
    }

    var lastWidth = _mediaQuery.Last();
    return windowWidth > lastWidth.Key ? lastWidth.Value : null;
  }
}