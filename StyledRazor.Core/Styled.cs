using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Style;
using System;

namespace StyledRazor.Core;

public class Styled
{
  public StyleDictionary Style { get; private set; }

  public string Id { get; private set; }

  public string Element { get; private set; }

  public string Css { get; private set; }


  internal Styled(IComponent component, string element, string baseCss)
  {
    Id = SetId(component.GetType().Name);
    Element = element;
    Style = StyleFactory.Create(baseCss, SetScope(Id, Element));
    UpdateCss();
  }

  public void Update(Styled styled)
  {
    Id = styled.Id;
    Element = styled.Element;
    Style = styled.Style;
    UpdateCss();
  }

  public void UpdateStyle(string selector, string property, string value)
  {
    Style.Get(selector).SetProperty(property, value);
    UpdateCss();
  }

  private void UpdateCss() => Css = Style?.ToString();

  private static string SetScope(string componentId, string baseElement = "") => $"{baseElement}[{componentId}]";

  private static string SetId(string name) =>
    $"{(name == null ? "w" : name.ToLower() + "_")}{Guid.NewGuid().ToString().Replace("-", "")[..10]}";
}