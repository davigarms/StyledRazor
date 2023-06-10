using Microsoft.AspNetCore.Components;
using System;
using static StyledRazor.Core.Utils.Css;

namespace StyledRazor.Core;

public class Styled
{
  public string Id { get; private set; }

  public string Element { get; private set; }

  public string Css { get; private set; }


  internal Styled(IComponent component, string element, string baseCss)
  {
    Id = IdFrom(component.GetType().Name);
    Element = element;

    var baseElementId = ElementIdFrom(Element, Id);
    Css = Minify(baseCss, baseElementId);
  }

  public void UpdateStyle(Styled styled)
  {
    Id = styled.Id;
    Css = styled.Css;
    Element = styled.Element;
  }

  private static string ElementIdFrom(string baseElement, string componentId) => $"{baseElement}[{componentId}]";

  private static string IdFrom(string name) =>
    $"{(name == null ? "w" : name.ToLower() + "_")}{Guid.NewGuid().ToString().Replace("-", "")[..10]}";
}