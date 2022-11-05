using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using StyledRazor.Core.Model;

namespace StyledRazor.Core.Components;

public class StyledBase : StyledData
{
  [Parameter] public Styled Styled { get; set; }

  [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> Params { get; set; }

  [Parameter] public RenderFragment ChildContent { get; set; }
  
  protected override void BuildRenderTree(RenderTreeBuilder builder)
  {
    builder.OpenElement(0, Element);
    BuildComponentId(builder);
    BuildComponentStyle(builder);
    BuildComponentParams(builder);
    BuildComponentContent(builder);
    BuildComponentCss(builder);
    builder.CloseElement();
  }

  protected override void OnParametersSet()
  {
    if (Styled == null) return;

    ComponentStyleFrom(Styled);
  }

  private void BuildComponentId(RenderTreeBuilder builder) => builder.AddAttribute(0, ComponentId);

  private void BuildComponentStyle(RenderTreeBuilder builder) => builder.AddAttribute(0, "style", Style);

  private void BuildComponentParams(RenderTreeBuilder builder)
  {
    if (Params == null) return;

    foreach (var (key, value) in Params)
      builder.AddAttribute(0, key, value);
  }

  private void BuildComponentContent(RenderTreeBuilder builder)
  {
    if (ChildContent == null) return;

    builder.AddContent(0, ChildContent);
  }

  private void BuildComponentCss(RenderTreeBuilder builder)
  {
    if (Css == null) return;

    builder.OpenElement(0, "style");
    builder.AddContent(0, Css);
    builder.CloseElement();
  }
}