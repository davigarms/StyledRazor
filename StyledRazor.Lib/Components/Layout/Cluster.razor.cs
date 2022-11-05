using Microsoft.AspNetCore.Components;
using StyledRazor.Core;
using StyledRazor.Core.Components;
using StyledRazor.Lib.Styles.Base;

namespace StyledRazor.Lib.Components.Layout;

public class Cluster : StyledBase
{

	private string Padding => NoPadding ? Tokens.Zero : Space;
	
  protected override Styled Base => Div($@"
      {{
        display: flex;
        gap: var(--space);
        min-width: var(--min-width);
        justify-content: var(--justify);
        align-items: var(--align);
        padding: var(--padding);
      }}
  ");

  protected override string Style => $@"
	  --space: {Space};
	  --justify: {Justify};
	  --align: {Align};
	  --padding: {Padding};
  ";

  [Parameter] 
  public string Space { get; set; } = Tokens.Zero;
  
  [Parameter] 
  public string Justify { get; set; } = Tokens.AlignFlexEnd;
  
  [Parameter] 
  public string Align { get; set; } = Tokens.AlignCenter;
  
  [Parameter] 
  public bool NoPadding { get; set; }
}
