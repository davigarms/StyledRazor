using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Components;

namespace StyledRazor.Lib.Components.Styling;

public class Background : StyledBase
{
  protected override Styled BaseComponent => Create.Div(@"{
    background: var(--background-color);
    height: inherit; 
  }");

  protected override string InlineStyle => $@"
    --background-color: {Color};
  ";

  [Parameter] public string Color { get; set; }
}