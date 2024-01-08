using Microsoft.AspNetCore.Components;using Microsoft.VisualBasic.CompilerServices;
using StyledRazor.Core.Style;
using static StyledRazor.Core.Utils.Css;

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
    definition.OnChange += UpdateCss;
    return definition;
  }
  
  private void UpdateCss() => CssString = Css?.ToString();
}