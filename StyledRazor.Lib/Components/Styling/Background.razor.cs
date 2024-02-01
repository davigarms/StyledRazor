using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Component;

namespace StyledRazor.Lib.Components.Styling;

public class Background : Styled
{
  [Parameter] public string Color { get; set; }

  protected override Styled Component => CreateStyled.Div(@"{
    background: var(--background-color);
    height: inherit; 
  }");

  protected override string InlineStyle => $@"
    --background-color: {Color};
  ";
}