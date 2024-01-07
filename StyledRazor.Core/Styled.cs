using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Style;
using System;

namespace StyledRazor.Core;

public class Styled
{
  private Css Css { get; set; }

  public string Id { get; private set; }

  public string Element { get; private set; }

  public string CssString { get; private set; }


  internal Styled(IComponent component, string element, string baseCss)
  {
    Id = SetId(component.GetType().Name);
    Element = element;
    Css = CssFactory.Create(baseCss, SetScope(Id, Element));
    UpdateCss();
  }

  public void Update(Styled styled)
  {
    Id = styled.Id;
    Element = styled.Element;
    Css = styled.Css;
    UpdateCss();
  }

  public CssDefinition Get(string selector)
  {
    var definition = Css.Get(selector);
    definition.OnChange += DefinitionHasChanged;
    return definition;
  }

  private void DefinitionHasChanged() => UpdateCss();
  
  private void UpdateCss() => CssString = Css?.ToString();

  private static string SetScope(string componentId, string baseElement = "") => $"{baseElement}[{componentId}]";

  private static string SetId(string name) =>
    $"{(name == null ? "w" : name.ToLower() + "_")}{Guid.NewGuid().ToString().Replace("-", "")[..10]}";
}