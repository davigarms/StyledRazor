namespace StyledRazor.Core.Components;

public class StyledData : ElementBase
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
}