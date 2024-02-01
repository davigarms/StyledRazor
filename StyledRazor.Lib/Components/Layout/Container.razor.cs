using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Component;
using StyledRazor.Core.Model;

namespace StyledRazor.Lib.Components.Layout;

public class Container : Styled
{
  [Parameter] public bool Fluid { get; set; }
  
  [Parameter] public string Width { get; set; } = "initial";

  protected override Styled Component => CreateStyled.Div(@"{
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
}