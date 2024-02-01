using StyledRazor.Core.Component.Styled;

namespace StyledRazor.Lib.Components.Layout;

public class Layout : Styled
{
  protected override Styled Component => CreateStyled.Div();
}