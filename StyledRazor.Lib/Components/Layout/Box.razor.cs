using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Component;
using StyledRazor.Core.Model;

namespace StyledRazor.Lib.Components.Layout;

public class Box : StyledBase
{
	[Parameter] public string Height { get; set; } = "initial";
	
	[Parameter] public string Width { get; set; } = "initial";
	
	[Parameter] public string Padding { get; set; }
	
	[Parameter] public string Left { get; set; }
	
	[Parameter] public string Top { get; set; }
	
	[Parameter] public string Right { get; set; }
	
	[Parameter] public string Bottom { get; set; }
	
	[Parameter] public string Horizontal { get; set; }
	
	[Parameter] public string Vertical { get; set; }
	
	private bool HasIndividualPadding => !string.IsNullOrEmpty(Left) || !string.IsNullOrEmpty(Top) ||
	                                     !string.IsNullOrEmpty(Right) || !string.IsNullOrEmpty(Bottom);

	private bool HasMirroredPadding => !string.IsNullOrEmpty(Horizontal) || !string.IsNullOrEmpty(Vertical);

	private string ShorthandPadding => HasIndividualPadding ? string.Empty :
		HasMirroredPadding ? $"--padding: {Vertical ?? Tokens.Zero} {Horizontal ?? Tokens.Zero};" : $"--padding: {Padding ?? Tokens.Zero};";

	protected override StyledBase Component => Create.Div(@"{
		height: var(--height);
		width: var(--width);
		padding-left: var(--left);
		padding-top: var(--top);
		padding-right: var(--right);	
		padding-bottom: var(--bottom);
		padding: var(--padding);
	}");

  protected override string InlineStyle => $@"
		{ShorthandPadding}
		--height: {Height};
		--width: {Width};
		--left: {Left};
		--top: {Top};
		--right: {Right};
		--bottom: {Bottom};
	";
}