namespace StyledRazor.Core.Components;

public class StyledElements : StyledBase
{
  protected virtual Styled Div { get; set; }
  protected virtual Styled H1 { get; set; }
  protected virtual Styled H2 { get; set; }
  protected override Styled Styled => Div ?? H1 ?? H2;

  protected StyledElements()
  {
    if (Styled != null)
      SetCss(Styled.BaseElement, Styled.BaseCss, Name);
  }
}