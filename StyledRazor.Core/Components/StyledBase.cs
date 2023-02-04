using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using StyledRazor.Core.Model;

namespace StyledRazor.Core.Components;

public partial class StyledBase : ComponentBase
{
  [Parameter] public Styled Styled { get; set; }

  [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> Params { get; set; }

  [Parameter] public RenderFragment ChildContent { get; set; }

  private Styled _styled;
  private string _componentId;
  private string _element;
  private string _css;

  protected ElementReference ElementRef { get; set; }
  protected virtual string Style { get; set; }
  protected virtual Styled Base { get; } = new();

  protected StyledBase()
  {
    Init();
  }

  private void Init()
  {
    Base.SetStyle(Style);
    _styled = Base;
  }

  private void ComponentStyleFrom(Styled styled)
  {
    _element = styled.Element;
    _css = styled.Css;
    _componentId = styled.Id;
    Style = styled.Style;
  }

  protected override void OnInitialized() => ComponentStyleFrom(_styled);

  protected override void OnParametersSet()
  {
    if (Styled == null) return;

    ComponentStyleFrom(Styled);
  }
  
  protected override void BuildRenderTree(RenderTreeBuilder builder)
  {
    builder.OpenElement(0, _element);
    BuildComponentId(builder);
    BuildComponentStyle(builder);
    BuildComponentParams(builder);
    BuildElementReference(builder);
    BuildComponentContent(builder);
    BuildComponentCss(builder);
    builder.CloseElement();
  }

  private void BuildComponentId(RenderTreeBuilder builder) => builder.AddAttribute(0, _componentId);

  private void BuildComponentStyle(RenderTreeBuilder builder) => builder.AddAttribute(0, "style", Style);

  private void BuildComponentParams(RenderTreeBuilder builder)
  {
    if (Params == null) return;

    foreach (var (key, value) in Params)
      builder.AddAttribute(0, key, value);
  }

  private void BuildElementReference(RenderTreeBuilder builder) => builder.AddElementReferenceCapture(7, value => ElementRef = value);
  
  private void BuildComponentContent(RenderTreeBuilder builder)
  {
    if (ChildContent == null) return;

    builder.AddContent(0, ChildContent);
  }

  private void BuildComponentCss(RenderTreeBuilder builder)
  {
    if (_css == null) return;

    builder.OpenElement(0, "style");
    builder.AddContent(0, _css);
    builder.CloseElement();
  }
}