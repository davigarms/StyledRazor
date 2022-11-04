using System;
using Microsoft.AspNetCore.Components;
using StyledRazor.Core;
using StyledRazor.Core.Components;
using StyledRazor.Lib.Styles.Base;

namespace StyledRazor.Lib.Components.Layout;

public class Box : StyledBase
{
	[Parameter] public string Height { get; set; } = "inherit";
	[Parameter] public string Width { get; set; } = "inherit";
  [Parameter] public string Padding { get; set; } = Tokens.Zero;
  [Parameter] public string Left { get; set; }
  [Parameter] public string Top { get; set; }
  [Parameter] public string Right { get; set; }
  [Parameter] public string Bottom { get; set; }
  [Parameter] public string Horizontal { get; set; }
  [Parameter] public string Vertical { get; set; }
  
  protected override Styled Base => Div($@"
		{{
			height: var(--height);
			width: var(--width);
			padding-left: var(--left);
			padding-top: var(--top);
			padding-right: var(--right);	
			padding-bottom: var(--bottom);
			{ShorthandPadding};
		}}
	");

  protected override string Style => $@"
		--height: {Height};
		--width: {Width};
		--padding: {Padding};
		--left: {Left};
		--top: {Top};
		--right: {Right};
		--bottom: {Bottom};
		--horizontal: {(string.IsNullOrEmpty(Horizontal) ? Tokens.Zero : Horizontal)};
		--vertical: {(string.IsNullOrEmpty(Vertical) ? Tokens.Zero : Vertical)};
	";



  private bool HasIndividualPadding => !string.IsNullOrEmpty(Left) || !string.IsNullOrEmpty(Top) ||
                                       !string.IsNullOrEmpty(Right) || !string.IsNullOrEmpty(Bottom);

  private bool HasMirroredPadding => !string.IsNullOrEmpty(Horizontal) || !string.IsNullOrEmpty(Vertical);

  private string ShorthandPadding =>
	  HasIndividualPadding ? string.Empty :
	  HasMirroredPadding ? "padding: var(--vertical) var(--horizontal)" : "padding: var(--padding)";

  protected override void OnParametersSet() => ComponentStyleFrom(Base);
}