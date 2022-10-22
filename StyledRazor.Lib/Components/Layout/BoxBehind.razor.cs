using Microsoft.AspNetCore.Components;
using StyledRazor.Core;
using StyledRazor.Core.Components;
using StyledRazor.Lib.Styles.Base;

namespace StyledRazor.Lib.Components.Layout;

public class BoxBase : ElementBase
{
	[Parameter] public string Height { get; set; } = Tokens.Initial;
  [Parameter] public string Width { get; set; } = Tokens.Initial;
  [Parameter] public string Padding { get; set; } = Tokens.SpacingM;

  protected override Styled Element => H1(
	  @"{
				height: var(--height);
				padding: var(--padding);
				width: var(--width)
			}
		");
}