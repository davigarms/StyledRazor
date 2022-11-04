using Microsoft.AspNetCore.Components;
using StyledRazor.Core;
using StyledRazor.Core.Components;
using StyledRazor.Lib.Styles.Base;

namespace StyledRazor.Lib.Styles.UI;

public partial class UIComponents : StyledBase
{
	protected const string ButtonMargin = $"{Tokens.SpacingS} {Tokens.SpacingM}";
	protected const string ButtonPadding = $"{Tokens.SpacingS} 0";

	private const string ButtonDefinition = $@"
			display: inline-block;
			min-width: 11rem;
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