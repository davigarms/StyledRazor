using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Components;
using StyledRazor.Core.Model;
using StyledRazor.Lib.Styles;

namespace StyledRazor.Lib.Components.UI.Button;

public class Button : StyledBase
{
	[Parameter] public string Margin { get; set; } = "0";
	[Parameter] public string Padding { get; set; }

	protected override Styled Base => DefaultButton;
	
	protected override string Style => $@"
	  --margin: {(string.IsNullOrEmpty(Margin) ? ButtonMargin : Margin)};
	  --padding: {(string.IsNullOrEmpty(Padding) ? ButtonPadding : Padding)}
	";
	
	private const string ButtonMargin = $"{Tokens.SpacingS} {Tokens.SpacingM}";
	
	private const string ButtonPadding = $"{Tokens.SpacingS}";

	private const string ButtonDefinition = $@"
		display: inline-block;
		border-radius: {Tokens.SpacingXs};
		background: transparent;
		color: black;
		border: 1px solid grey;
		text-align: center;
		cursor: pointer;
		margin: var(--margin);
    padding: var(--padding);    
	";

	public static readonly Styled DefaultButton = new("A", 
	$@"
		{{
	    {ButtonDefinition}                       
    }}
	", "Button");

  public static readonly Styled GreyButton = new("A",
  $@"
		{{
			{ButtonDefinition}
			background: grey;
			color: white;
		}}
	", "GreyButton");

  public static readonly Styled AlertButton = new("A", 
  $@"
		{{
			{ButtonDefinition}
			background: red;
			color: white;
		}}°
	", "AlertButton");
}