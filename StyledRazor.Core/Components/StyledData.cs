using System;
using StyledRazor.Core.Model;

namespace StyledRazor.Core.Components;

public class StyledData : ElementBase
{
  protected string ComponentId;
  protected string Element;
  protected string Css;
  protected virtual string Style { get; set; }
  protected virtual Styled Base { get; } = new ();
  
  private Styled _styled;

  protected StyledData() => Init();

  private void Init()
  {
    Base.SetStyle(Style);
    _styled = Base;
  }

  protected override void OnInitialized() => ComponentStyleFrom(_styled);
  
  protected void ComponentStyleFrom(Styled styled)
  {
    Element = styled.Element;
    Css = styled.Css;
    ComponentId = styled.Id;
    Style = styled.Style;
  }
}