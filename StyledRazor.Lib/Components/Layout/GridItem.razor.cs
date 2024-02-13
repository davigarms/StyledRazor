using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Component;
using StyledRazor.Core.Model;

namespace StyledRazor.Lib.Components.Layout;

public class GridItem : StyledBase
{
  protected override Styled BaseComponent => Create.Div();
}