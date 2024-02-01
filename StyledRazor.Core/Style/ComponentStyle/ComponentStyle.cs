using static StyledRazor.Core.Style.Css.CssHelper;
using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Component.Styled;
using StyledRazor.Core.Style.Css;
using System;

namespace StyledRazor.Core.Style.ComponentStyle;

public class ComponentStyle
{
  private CssRuleset Css { get; set; }

  public string Id { get; private set; }

  public string Element { get; private set; }

  public string CssString { get; private set; }
  
  public Type Type { get; private set; }

  internal ComponentStyle(IComponent component, string element, string baseCss)
  {
    Type = component.GetType();
    Id = IdFrom(Type.Name);
    Element = element;
    Css = CssFactory.Create(baseCss, ScopeFrom(Id, Element));
    UpdateCss();
  }

  public void Update(ComponentStyle componentStyle)
  {
    Type = componentStyle.Type;
    Id = componentStyle.Id;
    Element = componentStyle.Element;
    Css = componentStyle.Css;
    UpdateCss();
  }

  public CssStyleDeclaration Get(string selector)
  {
    var scopedSelector = $"{ScopeFrom(Id, Element)}{selector}";
    var declaration = Css.Get(scopedSelector);
    declaration.OnChange += UpdateCss;
    return declaration;
  }

  private void UpdateCss() => CssString = Css?.ToString();
}