using System.Linq;

namespace StyledRazor.Core.MediaQuery;

public class Columns
{
  private readonly MediaQuery _mediaQuery = new();

  public Columns(int xs = 0, int sm = 0, int md = 0, int lg = 0, int xl = 0, int xxl = 0)
  {
    if (xs > 0) _mediaQuery.Add(BreakPoint.Xs, xs);
    if (sm > 0) _mediaQuery.Add(BreakPoint.Sm, sm);
    if (md > 0) _mediaQuery.Add(BreakPoint.Md, md);
    if (lg > 0) _mediaQuery.Add(BreakPoint.Lg, lg);
    if (xl > 0) _mediaQuery.Add(BreakPoint.Xl, xl);
    if (xxl > 0) _mediaQuery.Add(BreakPoint.Xxl, xxl);
  }

  public static Columns Set(int xs = 0, int sm = 0, int md = 0, int lg = 0, int xl = 0, int xxl = 0)
    => new(xs, sm, md, lg, xl, xxl);

  public int? NumberOfColumnsFor(int windowWidth)
  {
    if (_mediaQuery.Count == 0) return null;

    for (var i = 0; i < _mediaQuery.Count - 1; i++)
    {
      var minQuery = _mediaQuery.ElementAt(i);
      var maxQuery = _mediaQuery.ElementAt(i + 1);

      if (windowWidth < (int)minQuery.Key ||
          (windowWidth >= (int)minQuery.Key && windowWidth < (int)maxQuery.Key))
      {
        return minQuery.Value;
      }
    }

    var lastQuery = _mediaQuery.Last();
    return windowWidth > (int)lastQuery.Key ? lastQuery.Value : null;
  }
}