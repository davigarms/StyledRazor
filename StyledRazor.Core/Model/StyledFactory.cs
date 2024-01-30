using Microsoft.AspNetCore.Components;

namespace StyledRazor.Core.Model;

public class StyledFactory
{
  private readonly IComponent _component;

  private Styled _styled;


  public StyledFactory(IComponent component)
  {
    _component = component;
  }

  private Styled Create(string baseElement, string baseCss) =>
    _styled ??= new Styled(_component, baseElement, baseCss);
  
  public Styled Div(string css = "") => Create("div", css);

  public Styled H1(string css = "") => Create("h1", css);

  public Styled A(string css = "") => Create("a", css);

  public Styled Ul(string css = "") => Create("ul", css);

  public Styled Li(string css = "") => Create("li", css);
}
