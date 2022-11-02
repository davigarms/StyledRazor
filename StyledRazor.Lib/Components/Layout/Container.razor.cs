using StyledRazor.Core;
using StyledRazor.Core.Components;

namespace StyledRazor.Lib.Components.Layout;

public class ContainerBase : StyledBase
{
	protected override Styled Base => Div(@"
		{
			margin-left: auto;
			margin-right: auto;
			max-width: var(--max-width);
			width: var(--width);
			position: relative;
		}
	");
}