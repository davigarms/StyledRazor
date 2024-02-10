using System.Collections.Generic;
using System.Linq;

namespace StyledRazor.Core.MediaQuery;

public class ColumnsService
{
  private BreakPoints BreakPoints { get; }

  private Dictionary<int, int> ColumnsByBreakpoint { get; set; } = new();

  public ColumnsService(BreakPoints breakPoints)
  {
    BreakPoints = breakPoints;
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

    for (var i = 0; i < BreakPoints.Values.Length; i++)
      if (columns[i] > 0)
        ColumnsByBreakpoint[BreakPoints.Values[i]] = columns[i];

    return this;
  }

  public int? NumberOfColumnsFor(int windowWidth)
  {
    if (ColumnsByBreakpoint.Count == 0) return null;

    for (var i = 0; i < ColumnsByBreakpoint.Count - 1; i++)
    {
      var minWidth = ColumnsByBreakpoint.ElementAt(i).Key;
      var maxWidth = ColumnsByBreakpoint.ElementAt(i + 1).Key;
      var minWidthColumns = ColumnsByBreakpoint.ElementAt(i).Value;

      if (windowWidth < minWidth ||
          windowWidth >= minWidth && windowWidth < maxWidth)
      {
        return minWidthColumns;
      }
    }

    var lastWidth = ColumnsByBreakpoint.Last().Key;
    var lastWidthColumns = ColumnsByBreakpoint.Last().Value;
    return windowWidth > lastWidth ? lastWidthColumns : null;
  }
}