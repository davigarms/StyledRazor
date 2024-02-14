using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using StyledRazor.Core.Style.DesignTokens;
using System.Collections.Generic;

namespace StyledRazor.Core.Components;

public abstract class StyledBase : ComponentBase
{
  [Inject] protected ITokens Tokens { get; set; }
  
  [Parameter] public StyledBase Base { get; set; }
  
  [Parameter(CaptureUnmatchedValues = true)] 
  public IDictionary<string, object> Params { get; set; }
  
  [Parameter] public RenderFragment ChildContent { get; set; }

  protected readonly StyledFactory Create;
  
  protected ElementReference ElementRef { get; private set; }
  
  protected virtual string InlineStyle => string.Empty;

  protected virtual Styled BaseComponent => Create.Div();

  protected StyledBase()
  {
    Create = new StyledFactory(this);
  }

  protected StyledBase(ITokens tokens) : this()
  {
    Tokens ??= tokens;
  } 

  protected override void OnParametersSet()
  {
    if (Base is null) return;
    BaseComponent.Update(Base.BaseComponent);
  }

  protected override void BuildRenderTree(RenderTreeBuilder builder)
  {
    builder.OpenElement(0, BaseComponent.Element);
    builder.AddAttribute(0, BaseComponent.Id);
    if (!string.IsNullOrEmpty(InlineStyle)) builder.AddAttribute(0, "style", InlineStyle);
    builder.AddMultipleAttributes(0, Params);
    builder.AddElementReferenceCapture(0, value => ElementRef = value);
    builder.AddContent(0, ChildContent);
    builder.CloseElement();
  }
}