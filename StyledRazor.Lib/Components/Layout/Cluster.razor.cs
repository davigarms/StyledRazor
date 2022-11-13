using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Components;
using StyledRazor.Core.Model;
using StyledRazor.Lib.Styles;

namespace StyledRazor.Lib.Components.Layout;

public class Cluster : StyledBase
{
	[Parameter] public string Space { get; set; } = Tokens.Zero;
	[Parameter] public bool Wrap { get; set; }
	[Parameter] public bool WrapReverse { get; set; }
	[Parameter] public string Align { get; set; } = Tokens.AlignCenter;
	[Parameter] public string AlignContent { get; set; }
	[Parameter] public string Justify { get; set; } = Tokens.AlignFlexEnd;
	[Parameter] public bool NoPadding { get; set; }

	private string Padding => NoPadding ? Tokens.Zero : Space;
	private string FlexWrap => Wrap ? Tokens.FlexWrap :
															WrapReverse ? Tokens.FlexWrapReverse :
																Tokens.FlexNoWrap;
	
  protected override Styled Base => Div($@"
      {{
        display: flex;
        gap: var(--gap);
        flex-wrap: var(--flex-wrap);
        align-items: var(--align-items);
        align-content: var(--align-content);
        justify-content: var(--justify);
        padding: var(--padding);
      }}

			[stretch] {{
				height: inherit;
			}}

			> [start] {{
				align-self: flex-start;
			}}

			> [end] {{
				align-self: flex-end;
			}}

			> [center] {{
				align-self: center;
			}}

			> [baseline] {{
				align-self: baseline;
			}}

			> [stretch] {{
				align-self: stretch;
			}}
  ");

  protected override string Style => $@"
	  --gap: {Space};
	  --flex-wrap: {FlexWrap};
	  --align-items: {Align};
	  --align-content: {AlignContent};
	  --justify: {Justify};
	  --padding: {Padding};
  ";
}
