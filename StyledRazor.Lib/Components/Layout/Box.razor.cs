using System;
using Microsoft.AspNetCore.Components;
using StyledRazor.Core;
using StyledRazor.Core.Components;
using StyledRazor.Lib.Styles.Base;

namespace StyledRazor.Lib.Components.Layout;

public class Box : StyledBase
{
	[Parameter] public string Height { get; set; } = "unset";
	[Parameter] public string Width { get; set; } = "initial";
  [Parameter] public string Padding { get; set; } = "0";

  protected override Styled Base => Div($@"
		{{
			height: var(--height);
			width: var(--width);
			padding: var(--padding);
		}}
	");
  
  protected override string Style => $@"
		--height: {Height};
		--width: {Width};
		--padding: {Padding};
	";
}