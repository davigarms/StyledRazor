using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Style;
using System;

namespace StyledRazor.Core;

public class Styled
{
  private CssRules CssRules { get; set; }

  public string Id { get; private set; }

  public string Element { get; private set; }

  public string Css { get; private set; }


  internal Styled(IComponent component, string element, string baseCss)
  {
    Id = SetId(component.GetType().Name);
    Element = element;
    CssRules = CssRulesFactory.Create(baseCss, SetScope(Id, Element));
    UpdateCss();
  }

  public void Update(Styled styled)
  {
    Id = styled.Id;
    Element = styled.Element;
    CssRules = styled.CssRules;
    UpdateCss();
  }

  public void UpdateStyle(string selector, string property, string value)
  {
    CssRules.Get(selector).SetProperty(property, value);
    UpdateCss();
  }

  private void UpdateCss() => Css = CssRules?.ToString();

  private static string SetScope(string componentId, string baseElement = "") => $"{baseElement}[{componentId}]";

  private static string SetId(string name) =>
    $"{(name == null ? "w" : name.ToLower() + "_")}{Guid.NewGuid().ToString().Replace("-", "")[..10]}";
}   