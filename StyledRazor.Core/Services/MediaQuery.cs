using System.Collections.Generic;
using System.Linq;
using StyledRazor.Core.Collections;

namespace StyledRazor.Core.Services;

public class MediaQuery
{
  private readonly List<KeyValuePair<BreakPoint, int>> _orderedColumns;

  public MediaQuery(BreakPoints responsiveColumns)
  {
    if (responsiveColumns == null) return;
    _orderedColumns = responsiveColumns.OrderBy(col => (int) col.Key).ToList();
  }

  public int? NumberOfColumnsFor(int windowWidth)
  {
    if (_orderedColumns == null) return null;
    for (var i = 0; i < _orderedColumns.Count - 1; i++)
    {
      var minQuery = _orderedColumns.ElementAt(i);
      var maxQuery = _orderedColumns.ElementAt(i + 1);

      if (windowWidth < (int) minQuery.Key ||
          (windowWidth >= (int) minQuery.Key && windowWidth < (int) maxQuery.Key))
      {
        return minQuery.Value;
      }
    }

    var last = _orderedColumns.Last();
    return windowWidth > (int) last.Key ? last.Value : null;
  }
}