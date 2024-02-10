using StyledRazor.Core.Style.DesignTokens;
using System.Collections.Generic;
using System.Linq;

namespace StyledRazor.Core.MediaQuery;

public class ColumnsService
{
  private readonly ITokens _tokens;

  public Dictionary<int, int> ColumnsByBreakpoint { get; private set; } = new();

  public ColumnsService(ITokens tokens)
  {
    _tokens = tokens;
  }

  public ColumnsService Set(int xs = 0,
                            int sm = 0,
                            int md = 0,
                            int lg = 0,
                            int xl = 0,
                            int xxl = 0
  )
  {
    int[] columns = { xs, sm, md, lg, xl, xxl };
    ColumnsByBreakpoint = new Dictionary<int, int>();
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
        ColumnsByBreakpoint[breakPoints[i]] = columns[i];

    return this;
  }

  public int? NumberOfColumnsFor(int windowWidth)
  {
    if (ColumnsByBreakpoint.Count == 0) return null;

    for (var i = 0; i < ColumnsByBreakpoint.Count - 1; i++)
    {
      var minWidth = ColumnsByBreakpoint.ElementAt(i);
      var maxWidth = ColumnsByBreakpoint.ElementAt(i + 1);

      if (windowWidth < minWidth.Key ||
          windowWidth >= minWidth.Key && windowWidth < maxWidth.Key)
      {
        return minWidth.Value;
      }
    }

    var lastWidth = ColumnsByBreakpoint.Last();
    return windowWidth > lastWidth.Key ? lastWidth.Value : null;
  }
}