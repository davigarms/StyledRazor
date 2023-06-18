using Microsoft.AspNetCore.Components;
using System;
using static StyledRazor.Core.Utils.Css;

namespace StyledRazor.Core;

public class Styled
{
  public StyleDictionary Style { get; private set; }

  public string Id { get; private set; }

  public string Element { get; private set; }

  public string Css { get; private set; }


  internal Styled(IComponent component, string element, string baseCss)
  {
    Id = IdFrom(component.GetType().Name);
    Element = element;
    Style = new StyleDictionary().Deserialize(Minify(baseCss, ElementIdFrom(Element, Id)));

    if (Style == null) return;
    
    Css = Style.ToString();
    Style.OnUpdate += UpdateCss;
  }

  public void Update(Styled styled)
  {
    Id = styled.Id;
    Element = styled.Element;
    Style = styled.Style;
    Css = styled.Css;
  }

  private void UpdateCss() => Css = Style.ToString();

  private static string ElementIdFrom(string baseElement, string componentId) => $"{baseElement}[{componentId}]";

  private static string IdFrom(string name) =>
    $"{(name == null ? "w" : name.ToLower() + "_")}{Guid.NewGuid().ToString().Replace("-", "")[..10]}";
}