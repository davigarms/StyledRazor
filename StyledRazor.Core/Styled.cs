using Microsoft.AspNetCore.Components;using Microsoft.VisualBasic.CompilerServices;
using StyledRazor.Core.Style;
using static StyledRazor.Core.Utils.Css;

namespace StyledRazor.Core;

public class Styled
{
  private CssRulesetDictionary Css { get; set; }

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

  public CssDeclarationDictionary Get(string selector)
  {
    var declaration = Css.Get(selector);
    declaration.OnChange += UpdateCss;
    return declaration;
  }
  
  private void UpdateCss() => CssString = Css?.ToString();
}