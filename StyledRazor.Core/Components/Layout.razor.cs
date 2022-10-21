using System.Linq;
using Microsoft.AspNetCore.Components;

namespace StyledRazor.Core.Components;

public class Layout : StyledBase
{
  [Parameter] public Styled _ { get; set; }
  
  protected override void OnParametersSet() => SetCss(_.BaseElement, _.BaseCss, _.Name);
}