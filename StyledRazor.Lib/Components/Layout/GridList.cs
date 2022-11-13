using StyledRazor.Core.Model;

namespace StyledRazor.Lib.Components.Layout;

public class GridList : Grid
{
  protected override Styled Base => UL(Css);
}