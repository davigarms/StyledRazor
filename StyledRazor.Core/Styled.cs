using Microsoft.AspNetCore.Components;
using System;

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
    Css = Compressed(baseCss, baseElementId);
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

  private static string Compressed(string css, string baseElementId)
  {
    css = css
      .Insert(0, $"{baseElementId}")
      .Insert(0, "\n")
      .Replace("  ", "")
      .Replace("\r", "\n")
      .Replace(" \n", "\n")
      .Replace("\t", "")
      .Replace(": ", ":")
      .Replace(" {", "{")
      .Replace(" }", "}")
      .Replace(" > ", ">")
      .Replace("}", $"}}{baseElementId}")
      .Replace("\n", "");

    return css
      .Insert(css.Length, "\n")
      .Replace($"{baseElementId}\n", "");
  }
}