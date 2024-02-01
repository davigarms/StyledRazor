using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Component;

namespace StyledRazor.Lib.Components.Layout;

public class GridItem : Styled
{
  protected override Styled Component => CreateStyled.Div();
}