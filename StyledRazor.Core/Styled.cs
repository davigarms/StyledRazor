using System;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace StyledRazor.Core;

public class Styled
{
  public string Name { get; }
  public string BaseElement { get; }
  public readonly string ComponentId;
  public string BaseCss { get; }
  public string Style { get; set; }

  public Styled(string baseElement, string baseCss, string name = "")
  {
    var id = Guid.NewGuid().ToString().Replace("-", "")[..10];
    Name = name;
    ComponentId = ComponentIdFrom(id, Name);
    BaseElement = baseElement;
    
    var baseElementId = BaseElementIdFrom(BaseElement, ComponentId);
    BaseCss = Compressed(baseCss, baseElementId);
  }

  private static string BaseElementIdFrom(string baseElement, string componentId) => $"{baseElement}[{componentId}]";

  private static string ComponentIdFrom(string id, string name) => $"{(name == null ? "w" : name.ToLower() + "_")}{id}";

  public Styled(string baseElement, string baseCss, MemberInfo member) : 
    this (baseElement, baseCss, member.Name) {}
  
  public Styled(string baseElement, string baseCss, ComponentBase component) : 
    this (baseElement, baseCss, component.GetType().Name) {}

  private string Compressed(string css, string baseElementId)
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