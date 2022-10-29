namespace StyledRazor.Core.Components;

public class StyledData : ElementBase
{
  protected string ComponentId;
  protected string ComponentElement;
  protected string ComponentCss;
  protected virtual string ComponentStyle { get; set; }
  protected virtual Styled Base => new ();
  
  private Styled _styled;

  protected StyledData() => Init();

  private void Init()
  {
    Base.SetStyle(ComponentStyle);
    _styled = Base;
  }

  protected override void OnInitialized()
  {
    if (_styled == null) return;

    ComponentStyleFrom(_styled);
  }

  protected void ComponentStyleFrom(Styled styled)
  {
    ComponentElement = styled.Element;
    ComponentCss = styled.Css;
    ComponentId = styled.Id;
    ComponentStyle = styled.Style;
  }
}