using System.Collections.Generic;
using System.Linq;

namespace StyledRazor.Core.Css;

public class CssUnit
{
  public string Name { get; }
  public int Modifier { get; }

  private CssUnit (string name, int modifier)
  {
    Name = name;
    Modifier = modifier;
  }

  public static IEnumerable<string> ValidNames => ValidUnits.Select(x => x.Name);

  public static IEnumerable<CssUnit> ValidUnits => new List<CssUnit>
  {
    new("rem", 16),
    new("em", 16),
    new("px", 1),
  };
}