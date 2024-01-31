using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Component;
using StyledRazor.Core.Model;

namespace StyledRazor.Lib.Components.Layout;

public class GridItem : StyledBase
{
  public override Styled ComponentStyle => Create.Div();
}