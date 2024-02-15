using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Components;

namespace StyledRazor.Lib.Components.Layout;

public class Cluster : StyledBase
{
  protected override Styled BaseComponent => CreateStyled.Div(@"{
    display: flex;
    gap: var(--gap);
    flex-wrap: var(--flex-wrap);
    align-items: var(--align-items);
    align-content: var(--align-content);
    justify-content: var(--justify);
    padding: var(--padding);
  }

	[stretch] {
		height: inherit;
	}

	> * {
		flex-grow: var(--flex-grow);
		flex-basis: var(--flex-basis);
	}

	> [start] {
		align-self: flex-start;
	}

	> [end] {
		align-self: flex-end;
	}

	> [center] {
		align-self: center;
	}

	> [baseline] {
		align-self: baseline;
	}

	> [stretch] {
		align-self: stretch;
	}");

  protected override string InlineStyle => $@"
	  --gap: {Space ?? Tokens.Zero};
	  --flex-wrap: {FlexWrap};
	  --align-items: {Align ?? Tokens.AlignCenter};
	  --align-content: {AlignContent};
	  --justify: {Justify ?? Tokens.AlignFlexEnd};
	  --padding: {Padding};
  ";

  [Parameter] public string Space { get; set; }

  [Parameter] public bool Wrap { get; set; }

  [Parameter] public bool WrapReverse { get; set; }

  [Parameter] public string Align { get; set; }

  [Parameter] public string AlignContent { get; set; }

  [Parameter] public string Justify { get; set; }

  [Parameter] public bool NoPadding { get; set; }

  private string Padding => NoPadding ? Tokens.Zero : Space;

  private string FlexWrap => Wrap ? Tokens.FlexWrap :
                             WrapReverse ? Tokens.FlexWrapReverse :
                             Tokens.FlexNoWrap;
}