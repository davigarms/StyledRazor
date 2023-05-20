using System;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace StyledRazor.Core.Model;

public class Styled
{
  public readonly string Id;
  public string Name { get; }
  public string Element { get; }
  public string Css { get; }
  public string Style { get; private set; }

  public Styled(string element = "div", string baseCss = "", string name = "")
  {
    Name = name;
    Id = IdFrom(Name);
    Element = element;
    
    var baseElementId = ElementIdFrom(Element, Id);
    Css = Compressed(baseCss, baseElementId);
  }

  public Styled(string element, string baseCss, MemberInfo member) :
    this(element, baseCss, member.Name)
  {
  }

  public Styled(string element, string baseCss, IComponent component) :
    this(element, baseCss, component.GetType().Name)
  {
  }

  public void SetStyle(string style) => Style = style;

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