using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Component;

namespace StyledRazor.Core.Model;

public class StyledFactory
{
  private readonly IComponent _component;

  private StyledBase _styled;


  public StyledFactory(IComponent component)
  {
    _component = component;
  }

  private StyledBase Create(string baseElement, string baseCss) =>
    _styled ??= new StyledBase(new Styled(_component, baseElement, baseCss));
  
  public StyledBase Div(string css = "") => Create("div", css);

  public StyledBase H1(string css = "") => Create("h1", css);

  public StyledBase A(string css = "") => Create("a", css);

  public StyledBase Ul(string css = "") => Create("ul", css);

  public StyledBase Li(string css = "") => Create("li", css);
}
