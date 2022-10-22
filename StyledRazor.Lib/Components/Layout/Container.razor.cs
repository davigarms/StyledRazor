using StyledRazor.Core;
using StyledRazor.Core.Components;

namespace StyledRazor.Lib.Components.Layout;

public class ContainerBase : ElementBase
{
  protected override Styled Element => Div(
    @"
			{
				margin-left: auto;
				margin-right: auto;
				max-width: var(--max-width);
				width: var(--width);
				position: relative;
			}
		"
  );
}