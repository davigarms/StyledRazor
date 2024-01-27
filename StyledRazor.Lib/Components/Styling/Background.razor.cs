using Microsoft.AspNetCore.Components;
using StyledRazor.Core;
using StyledRazor.Core.Component;

namespace StyledRazor.Lib.Components.Styling;

public class Background : StyledBase
{
  [Parameter] public string Color { get; set; }

  public override Styled Base => Create.Div(@"{
    background: var(--background-color);
    height: inherit; 
  }");

  protected override string Style => $@"
    --background-color: {Color};
  ";
}