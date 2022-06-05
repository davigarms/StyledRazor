using StyledRazor.Lib.Styles.Base;
using Styled = StyledRazor.Core.Styled;

namespace StyledRazor.Lib.Styles.UI;

public partial class Elements : Tokens
{
    private static string ButtonDefinition =>
        $@"display: inline-block;
		min-width: 11rem;
		border-radius: {SpacingXs};
		background: transparent;
		color: black;
		border: 1px solid grey;
		text-align: center;
		cursor: pointer;
		margin: var(--margin);
        padding: var(--padding);    
		--margin: {ButtonMargin};
		--padding: {ButtonPadding};
		";

    protected static string ButtonMargin => $"{SpacingS} {SpacingM}";

    protected static string ButtonPadding => $"{SpacingS} 0";

    protected static Styled DefaultButton =>
        new($@"a{{
					    {ButtonDefinition}                       
				    }}
			", "Button");

    protected static Styled GreyButton =>
        new($@"
				a {{
					{ButtonDefinition}
					background: grey;
					color: white;
				}}
			", "GreyButton");

    protected static Styled AlertButton =>
        new($@"
				a {{
					{ButtonDefinition}
					background: red;
					color: white;
				}}
			", "AlertButton");
}