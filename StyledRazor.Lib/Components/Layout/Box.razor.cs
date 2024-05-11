using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Components;
using StyledRazor.Core.Style.DesignTokens;

namespace StyledRazor.Lib.Components.Layout;

public class Box : StyledBase
{
	protected override Styled BaseComponent => CreateStyled.Div(@"{
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

  [Parameter] public string Height { get; set; } = "initial";

  [Parameter] public string Width { get; set; } = "initial";

  [Parameter] public string Padding
  {
	  get => _padding ?? Tokens.SpacingS;
	  set => _padding = value;
  }

  [Parameter] public string Left { get; set; }

  [Parameter] public string Top { get; set; }

  [Parameter] public string Right { get; set; }

  [Parameter] public string Bottom { get; set; }

  [Parameter] public string Horizontal { get; set; }

  [Parameter] public string Vertical { get; set; }

  private bool HasIndividualPadding => !string.IsNullOrEmpty(Left) || !string.IsNullOrEmpty(Top) ||
                                       !string.IsNullOrEmpty(Right) || !string.IsNullOrEmpty(Bottom);

  private bool HasMirroredPadding => !string.IsNullOrEmpty(Horizontal) || !string.IsNullOrEmpty(Vertical);

  private string ShorthandPadding => HasIndividualPadding ? $"--padding: {IndividualPadding};" :
                                     HasMirroredPadding ? $"--padding: {MirroredPadding};" :
                                     $"--padding: {Padding};";

  private string IndividualPadding => $"{Top ?? Zero} {Right ?? Zero} {Bottom ?? Zero} {Left ?? Zero}";
  
  private string MirroredPadding => $"{Vertical ?? Zero} {Horizontal ?? Zero}";

  private string Zero => Tokens.Zero;

  private string _padding;
}