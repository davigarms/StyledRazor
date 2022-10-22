using System;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace StyledRazor.Core;

public class Styled
{
  private readonly string _id = Guid.NewGuid().ToString().Replace("-", "")[..10];
  public string Name { get; }
  public string BaseElement { get; }
  public string ComponentId { get; }
  private string BaseElementId => $"{BaseElement}[{ComponentId}]";
  public string BaseCss { get; }
  
  public Styled(string baseElement, string baseCss, string name = "")
  {
    Name = name;
    BaseElement = baseElement;
    ComponentId = ComponentIdFrom(Name);
    BaseCss = Compressed(BaseElementId, baseCss);
  }
  
  public Styled(string baseElement, string baseCss, MemberInfo member) : this (baseElement, baseCss, member.Name) {}
  
  public Styled(string baseElement, string baseCss, ComponentBase component) : this (baseElement, baseCss, component.GetType().Name) {}

  private string ComponentIdFrom(string name = null) =>
    $"{(name == null ? "w" : name.ToLower() + "_")}{_id}";

  private static string Compressed(string baseElementId, string css)
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