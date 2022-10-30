using Microsoft.AspNetCore.Components;

namespace StyledRazor.Core.Components;

public class ElementBase : ComponentBase
{
  private Styled Element(string baseElement, string baseCss) => new(baseElement, baseCss, this);
  protected Styled Div(string css) => Element("div", css);
  protected Styled H1(string css) => Element("h1", css);
  protected Styled A(string css) => Element("a", css);
  protected Styled UL(string css) => Element("ul", css);
}