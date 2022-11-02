using Microsoft.AspNetCore.Components;
using StyledRazor.Core;
using StyledRazor.Core.Components;

namespace StyledRazor.Lib.Components.Styling;

public class Background : StyledBase
{
  [Parameter] public string Color { get; set; }
  
  protected override Styled Base => Div(@"
    {
      background: var(--background-color);
    }
  ");

  protected override string Style => $@"
    --background-color: {Color};
  ";
}