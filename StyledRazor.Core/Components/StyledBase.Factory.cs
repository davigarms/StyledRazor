using StyledRazor.Core.Model;

namespace StyledRazor.Core.Components;

public partial class StyledBase
{
  private Styled Create(string baseElement, string baseCss) => new(baseElement, baseCss, this);
  protected Styled Div(string css = "") => Create("div", css);
  protected Styled H1(string css = "") => Create("h1", css);
  protected Styled A(string css = "") => Create("a", css);
  protected Styled UL(string css = "") => Create("ul", css);
  protected Styled LI(string css = "") => Create("li", css);
}