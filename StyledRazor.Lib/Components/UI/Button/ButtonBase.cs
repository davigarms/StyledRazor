using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Components;
using StyledRazor.Core.Model;
using StyledRazor.Lib.Styles.Base;

namespace StyledRazor.Lib.Components.UI.Button;

public class ButtonBase : StyledBase
{

	[Parameter] public string Margin { get; set; } = "0";
	[Parameter] public string Padding { get; set; }

	private const string ButtonMargin = $"{Tokens.SpacingS} {Tokens.SpacingM}";
	private const string ButtonPadding = $"{Tokens.SpacingS}";

	protected override string Style => $@"
          --margin: {(string.IsNullOrEmpty(Margin) ? ButtonMargin : Margin)};
          --padding: {(string.IsNullOrEmpty(Padding) ? ButtonPadding : Padding)}";

	private const string ButtonDefinition = $@"
			display: inline-block;
			border-radius: {Tokens.SpacingXS};
			background: transparent;
			color: black;
			border: 1px solid grey;
			text-align: center;
			cursor: pointer;
			margin: var(--margin);
	    padding: var(--padding);    
		";

	protected static readonly Styled DefaultButton = new("a", 
		$@"
			{{
		    {ButtonDefinition}                       
	    }}
		", "Button");

  protected static readonly Styled GreyButton = new("a",
    $@"
			{{
				{ButtonDefinition}
				background: grey;
				color: white;
			}}
		", "GreyButton");

  protected static readonly Styled AlertButton = new("a", 
    $@"
			{{
				{ButtonDefinition}
				background: red;
				color: white;
			}}°
		", "AlertButton");
}