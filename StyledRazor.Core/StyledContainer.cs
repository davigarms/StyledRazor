using Microsoft.AspNetCore.Components;

namespace StyledRazor.Core;

public class StyledContainer : ComponentBase
{
  protected virtual Styled Base { get; set; }
  protected virtual string Style { get; private set; }
  protected string ComponentId;
  protected string BaseElement;
  protected string BaseCss;
  
  private Styled _styled;

  protected StyledContainer() => Init();

  private void Init() => _styled = Base;

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
  }

  private Styled Element(string baseElement, string baseCss) => new(baseElement, baseCss, this);
  protected Styled Div(string css) => Element("div", css);
  protected Styled H1(string css) => Element("h1", css);
  protected Styled A(string css) => Element("a", css);
}