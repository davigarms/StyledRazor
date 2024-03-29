using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Components;
using StyledRazor.Core.Style.DesignTokens;

namespace StyledRazor.Lib.Components.UI.Button;

public class Button : StyledBase
{
  public Button() {}

  public Button(ITokens tokens) : base(tokens) {}

  protected override Styled BaseComponent => CreateStyled.A($@"{{
    {ButtonDefinition}                       
  }}");

  protected override string InlineStyle => $@"
	  --margin: {(string.IsNullOrEmpty(Margin) ? ButtonMargin : Margin)};
	  --padding: {(string.IsNullOrEmpty(Padding) ? ButtonPadding : Padding)}
	";

  protected string ButtonDefinition => $@"
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

  [Parameter] public string Margin { get; set; } = "0";

  [Parameter] public string Padding { get; set; }

  private string ButtonMargin => $"{Tokens.SpacingS} {Tokens.SpacingM}";

  private string ButtonPadding => $"{Tokens.SpacingS}";
}

public class GreyButton : Button
{
	public GreyButton() {}
  
	public GreyButton(ITokens tokens) : base(tokens) {}

	protected override Styled BaseComponent => CreateStyled.A($@"{{
		{ButtonDefinition}
		background: grey;
		color: white;
	}}");
}

public class AlertButton : Button
{
	public AlertButton() {}
	
  public AlertButton(ITokens tokens) : base(tokens) {}

  protected override Styled BaseComponent => CreateStyled.A($@"{{
		{ButtonDefinition}
		background: red;
		color: white;
	}}");
}