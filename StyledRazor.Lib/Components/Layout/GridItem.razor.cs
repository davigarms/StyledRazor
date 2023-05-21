using Microsoft.AspNetCore.Components;
using StyledRazor.Core;

namespace StyledRazor.Lib.Components.Layout;

public class GridItem : StyledBase
{
  public override Styled Base => Create.Li();
}