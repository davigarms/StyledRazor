using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Component.Styled;

namespace StyledRazor.Lib.Components.Styling;

public class Background : Styled
{
  protected override Styled ComponentBase => CreateStyled.Div(@"{
    background: var(--background-color);
    height: inherit; 
  }");

  protected override string InlineStyle => $@"
    --background-color: {Color};
  ";

  [Parameter] public string Color { get; set; }
}