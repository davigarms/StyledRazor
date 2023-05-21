using Microsoft.AspNetCore.Components;
using StyledRazor.Core;
using StyledRazor.Lib.Styles;

namespace StyledRazor.Lib.Components.UI.Button;

public class Button : StyledBase
{
	[Parameter] public string Margin { get; set; } = "0";
	[Parameter] public string Padding { get; set; }

	public override Styled Base => Create.A($@"
	{{
    {ButtonDefinition}                       
  }}");
	
	protected override string Style => $@"
	  --margin: {(string.IsNullOrEmpty(Margin) ? ButtonMargin : Margin)};
	  --padding: {(string.IsNullOrEmpty(Padding) ? ButtonPadding : Padding)}
	";
	
	private const string ButtonMargin = $"{Tokens.SpacingS} {Tokens.SpacingM}";
	
	private const string ButtonPadding = $"{Tokens.SpacingS}";

	protected const string ButtonDefinition = $@"
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
}

public class GreyButton : Button
{
	public override Styled Base => Create.A($@"
	{{
		{ButtonDefinition}
		background: grey;
		color: white;
	}}");
}

public class AlertButton : Button
{
	public override Styled Base => Create.A($@"
	{{
		{ButtonDefinition}
		background: red;
		color: white;
	}}");
}