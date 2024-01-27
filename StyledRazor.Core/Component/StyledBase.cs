using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using StyledRazor.Core.Style.DesignTokens;
using StyledRazor.Core.StyleSheet;
using System.Collections.Generic;

namespace StyledRazor.Core.Component;

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
  
  protected ElementReference ElementRef { get; set; }
  
  protected virtual string Style => string.Empty;

  public virtual Styled Base => Create.Div();

  protected StyledBase()
  {
    Create = new StyledFactory(this);
  }

  protected StyledBase(ITokens tokens) : this()
  {
    Tokens ??= tokens;
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
    builder.AddAttribute(0, Base.Id);
    if (!string.IsNullOrEmpty(Style)) builder.AddAttribute(0, "style", Style);
    builder.AddMultipleAttributes(0, Params);
    builder.AddElementReferenceCapture(0, value => ElementRef = value);
    builder.AddContent(0, ChildContent);
    builder.CloseElement();
  }
}