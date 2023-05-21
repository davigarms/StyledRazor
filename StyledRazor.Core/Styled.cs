using Microsoft.AspNetCore.Components;
using System;
using System.Reflection;

namespace StyledRazor.Core;

public class Styled
{
  public string Id { get; }
  public string Name { get; }
  public string Element { get; }
  public string Css { get; }

  public Styled(string element, string baseCss, IComponent component)
  {
    Name = component.GetType().Name;
    Id = IdFrom(Name);
    Element = element;
    
    var baseElementId = ElementIdFrom(Element, Id);
    Css = Compressed(baseCss, baseElementId);
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