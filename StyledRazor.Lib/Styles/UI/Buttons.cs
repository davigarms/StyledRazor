using Microsoft.AspNetCore.Components;
using StyledRazor.Core;
using StyledRazor.Lib.Styles.Base;

namespace StyledRazor.Lib.Styles.UI;

public partial class UiComponents : ComponentBase
{
	protected static string ButtonMargin => $"{Tokens.SpacingS} {Tokens.SpacingM}";
	protected static string ButtonPadding => $"{Tokens.SpacingS} 0";
  private static string ButtonDefinition => 
    $@"
			display: inline-block;
			min-width: 11rem;
			border-radius: {Tokens.SpacingXs};
			background: transparent;
			color: black;
			border: 1px solid grey;
			text-align: center;
			cursor: pointer;
			margin: var(--margin);
	    padding: var(--padding);    
			--margin: {ButtonMargin};
			--padding: {ButtonPadding};
		";

  protected static Styled DefaultButton => new("a", 
		$@"
			{{
		    {ButtonDefinition}                       
	    }}
		", "Button");

  protected static Styled GreyButton => new("a",
    $@"
			{{
				{ButtonDefinition}
				background: grey;
				color: white;
			}}
		", "GreyButton");

  protected static Styled AlertButton => new("a", 
    $@"
			{{
				{ButtonDefinition}
				background: red;
				color: white;
			}}
		", "AlertButton");
}