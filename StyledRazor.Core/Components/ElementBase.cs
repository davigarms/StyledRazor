using System.Reflection;

namespace StyledRazor.Core.Components;

public class ElementBase : StyledBase
{
  /*
  protected virtual Styled Div { get; set; }
  protected virtual Styled H1 { get; set; }
  protected virtual Styled H2 { get; set; }
  protected override Styled Element => Div ?? H1 ?? H2;
  */

  protected ElementBase()
  {
    if (base.Element != null)
      SetCss(base.Element.Name, base.Element.Css, Name);
  }

  protected Styled Div(string css) => new (MethodBase.GetCurrentMethod()?.Name, css, this);
  protected Styled H1(string css) => new (MethodBase.GetCurrentMethod()?.Name, css, this);
}