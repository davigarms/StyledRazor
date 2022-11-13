using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Components;
using StyledRazor.Core.Model;
using StyledRazor.Lib.Styles;

namespace StyledRazor.Lib.Components.Layout;

public class Container : StyledBase
{
	[Parameter] public bool Fluid { get; set; }
	[Parameter] public string Width { get; set; } = "initial";

	protected override Styled Base => Div(
		@"
		{
			margin-left: auto;
			margin-right: auto;
			max-width: var(--max-width);
			width: var(--width);
			position: relative;
		}
	"
	);

	protected override string Style => $@"
		--max-width: {(Fluid ? Width : Tokens.Initial)};
		--width: {(Fluid ? Tokens.SizeTotal : Width)};
	";
}