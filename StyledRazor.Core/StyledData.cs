using Microsoft.AspNetCore.Components;

namespace StyledRazor.Core;

public class StyledData : ComponentBase
{
  protected virtual Styled Base => Element();
  protected virtual string Style { get; set; }
  protected string ComponentId;
  protected string BaseElement;
  protected string BaseCss;
  
  private Styled _styled;

  protected StyledData() => Init();

  private void Init()
  {
    _styled = Base;
    _styled.Style = Style;
  }

  protected override void OnInitialized()
  {
    if (_styled == null) return;

    SetStyle(_styled);
  }

  protected void SetStyle(Styled styled)
  {
    BaseElement = styled.BaseElement;
    BaseCss = styled.BaseCss;
    ComponentId = styled.ComponentId;
    Style = _styled.Style;
  }

  private Styled Element(string baseElement = "div", string baseCss = "") => new(baseElement, baseCss, this);
  protected Styled Div(string css) => Element("div", css);
  protected Styled H1(string css) => Element("h1", css);
  protected Styled A(string css) => Element("a", css);
}