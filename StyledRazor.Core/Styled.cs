using static StyledRazor.Core.Css.CssHelper;
using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Css;

namespace StyledRazor.Core;

public class Styled
{
  private CssRuleset Css { get; set; }

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

  public CssStyleDeclaration Get(string selector)
  {
    var scopedSelector = $"{SetScope(Id, "div")}{selector}";
    var declaration = Css.Get(scopedSelector);
    declaration.OnChange += UpdateCss;
    return declaration;
  }
  
  private void UpdateCss() => CssString = Css?.ToString();
}