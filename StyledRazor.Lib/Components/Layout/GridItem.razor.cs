using Microsoft.AspNetCore.Components;
using StyledRazor.Core;
using StyledRazor.Core.Component;

namespace StyledRazor.Lib.Components.Layout;

public class GridItem : StyledBase
{
  public override Styled Base => Create.Div();
}