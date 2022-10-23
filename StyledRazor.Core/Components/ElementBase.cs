using Microsoft.AspNetCore.Components;

namespace StyledRazor.Core.Components;

public class ElementBase : ComponentBase
{
  protected Styled Element(string baseElement = "div", string baseCss = "") => new(baseElement, baseCss, this);
  protected Styled Div(string css) => Element("div", css);
  protected Styled H1(string css) => Element("h1", css);
  protected Styled A(string css) => Element("a", css);
}