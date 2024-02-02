using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Component.Styled;

namespace StyledRazor.Lib.Components.Layout;

public class GridItem : Styled
{
  protected override Styled ComponentBase => CreateStyled.Li();
}