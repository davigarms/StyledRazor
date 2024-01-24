using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using StyledRazor.Core.Style;
using StyledRazor.Core.StyleSheet;
using System.Collections.Generic;

namespace StyledRazor.Core;

public abstract class StyledBase : ComponentBase
{
  [Inject] 
  protected ITokens Tokens { get; set; }
  
  [Parameter] 
  public Styled Styled { get; set; }
  
  [Parameter(CaptureUnmatchedValues = true)] 
  public IDictionary<string, object> Params { get; set; }
  
  [Parameter] 
  public RenderFragment ChildContent { get; set; }

  protected readonly StyledFactory Create;
  
  protected ElementReference ElementRef { get; private set; }
  
  protected virtual bool UseElementRef => false;
  
  protected virtual string Style => string.Empty;

  public virtual Styled Base => Create.Div();

  protected StyledBase()
  {
    Create = new StyledFactory(this);
  }

  protected StyledBase(ITokens tokens)
  {
    Tokens ??= tokens;
    Create = new StyledFactory(this);
  } 
  
  protected override void OnInitialized() => StyleSheetService.Add(Base);

  protected override void OnParametersSet()
  {
    if (Styled == null) return;
    UpdateStyle(Styled);
  }
  
  private void UpdateStyle(Styled styled)
  {
    StyleSheetService.Update(Base.Id, styled);
    Base.Update(styled);
  }

  protected override void BuildRenderTree(RenderTreeBuilder builder)
  {
    builder.OpenElement(0, Base.Element);
    BuildComponentId(builder);
    BuildComponentStyle(builder);
    BuildComponentParams(builder);
    BuildElementReference(builder);
    BuildComponentContent(builder);
    builder.CloseElement();
  }

  private void BuildComponentId(RenderTreeBuilder builder) => builder.AddAttribute(0, Base.Id);

  private void BuildComponentStyle(RenderTreeBuilder builder)
  {
    if (string.IsNullOrEmpty(Style)) return; 
    builder.AddAttribute(0, "style", Style);
  }

  private void BuildComponentParams(RenderTreeBuilder builder)
  {
    if (Params == null) return;
    foreach (var (key, value) in Params)
      builder.AddAttribute(0, key, value);
  }

  private void BuildElementReference(RenderTreeBuilder builder)
  {
    if (!UseElementRef) return;
    builder.AddElementReferenceCapture(0, value => ElementRef = value);
  }

  private void BuildComponentContent(RenderTreeBuilder builder)
  {
    if (ChildContent == null) return;
    builder.AddContent(0, ChildContent);
  }
}