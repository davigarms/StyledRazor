using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Components;

namespace StyledRazor.Lib.Components.Layout;

public class Container : StyledBase
{
  protected override Styled BaseComponent => CreateStyled.Div(@"{
		margin-left: auto;
		margin-right: auto;
		max-width: var(--max-width);
		width: var(--width);
		position: relative;
	}");

  protected override string InlineStyle => $@"
		--max-width: {(Fluid ? Width : Tokens.Initial)};
		--width: {(Fluid ? Tokens.SizeTotal : Width)};
	";

  [Parameter] public bool Fluid { get; set; }

  [Parameter] public string Width { get; set; } = "initial";
}