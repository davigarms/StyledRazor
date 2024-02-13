using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Style.Css;
using StyledRazor.Core.Style.StyleSheet;
using static StyledRazor.Core.Style.Css.CssHelper;

namespace StyledRazor.Core.Components;

public class Styled : StyledBase
{
  private CssRuleset Css { get; set; }

  public string Id { get; private set; }

  public string Element { get; private set; }

  public string CssString { get; private set; }
  
  protected override Styled BaseComponent => this;
  
  public Styled() {}

  internal Styled(IComponent component, string element, string baseCss)
  {
    StyleSheetService.Add(this);
    Id = IdFrom(component.GetType().Name);
    Element = element;
    Css = CssFactory.Create(baseCss, ScopeFrom(Id, Element));
    UpdateCss();
  }

  public void Update(Styled styled)
  {
    Id = styled.Id;
    Element = styled.Element;
    Css = styled.Css;
    UpdateCss();
  }

  public CssStyleDeclaration Get(string selector)
  {
    var scopedSelector = $"{ScopeFrom(Id, Element)}{selector}";
    var declaration = Css.Get(scopedSelector);
    declaration.OnChange += UpdateCss;
    return declaration;
  }

  private void UpdateCss()
  {
    CssString = Css?.ToString();
    StyleSheetService.OnUpdate?.Invoke();
  }
}