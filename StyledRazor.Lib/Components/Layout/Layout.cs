using StyledRazor.Core.Component;
using StyledRazor.Core.Model;

namespace StyledRazor.Lib.Components.Layout;

public class Layout : Styled
{
  protected override Styled Component => CreateStyled.Div();
}