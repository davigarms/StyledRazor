using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Style.ComponentStyle;

namespace StyledRazor.Core.Component.Styled;

public class StyledFactory
{
  private readonly IComponent _component;

  private Component.Styled.Styled _styled;


  public StyledFactory(IComponent component)
  {
    _component = component;
  }

  private Component.Styled.Styled Create(string baseElement, string baseCss) =>
    _styled ??= new Component.Styled.Styled(new ComponentStyle(_component, baseElement, baseCss));
  
  public Component.Styled.Styled Div(string css = "") => Create("div", css);

  public Component.Styled.Styled H1(string css = "") => Create("h1", css);

  public Component.Styled.Styled A(string css = "") => Create("a", css);

  public Component.Styled.Styled Ul(string css = "") => Create("ul", css);

  public Component.Styled.Styled Li(string css = "") => Create("li", css);
}
