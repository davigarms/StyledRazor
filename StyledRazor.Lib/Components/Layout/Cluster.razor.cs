using StyledRazor.Core;
using StyledRazor.Core.Components;
using StyledRazor.Lib.Styles.Base;

namespace StyledRazor.Lib.Components.Layout;

public class Cluster : StyledBase
{
  protected override Styled Base => Div($@"
		{{
			margin-right: {Tokens.Zero};
			margin-left: {Tokens.Auto};
			width: fit-content;
	  }}
	");
}