using Microsoft.AspNetCore.Components;
using StyledRazor.Core;

namespace StyledRazor.Lib.Components.Layout;

public class GridItem : StyledBase
{
  protected override Styled Base => Create.Li();
}