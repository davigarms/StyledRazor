using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Components;
using StyledRazor.Core.Model;

namespace StyledRazor.Lib.Components.Layout;

public class GridItem : StyledBase
{
  protected override Styled Base => Create.Li();
}