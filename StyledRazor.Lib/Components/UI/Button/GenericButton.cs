using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Components;
using StyledRazor.Lib.Styles.UI;

namespace StyledRazor.Lib.Components.UI.Button;

public class GenericButton : UIComponents
{
  protected override string Style => $@"
    --margin: {(string.IsNullOrEmpty(Margin) ? ButtonMargin : Margin)};
    --padding: {(string.IsNullOrEmpty(Padding) ? ButtonPadding : Padding)}
  ";

  [Parameter]
  public string Margin { get; set; } = "0";

  [Parameter]
  public string Padding { get; set; }
}