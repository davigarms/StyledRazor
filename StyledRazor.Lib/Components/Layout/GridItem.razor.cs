using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Components;

namespace StyledRazor.Lib.Components.Layout;

public class GridItem : StyledBase
{
  protected override Styled BaseComponent => Create.Div();
}