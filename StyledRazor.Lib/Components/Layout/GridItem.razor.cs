using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Components.StyledComponent;

namespace StyledRazor.Lib.Components.Layout;

public class GridItem : Styled
{
  protected override Styled BaseComponent => CreateStyled.Li();
}