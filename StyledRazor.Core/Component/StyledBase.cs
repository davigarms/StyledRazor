using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using StyledRazor.Core.Model;
using StyledRazor.Core.Style.DesignTokens;
using StyledRazor.Core.StyleSheet;
using System.Collections.Generic;

namespace StyledRazor.Core.Component;

public class StyledBase : ComponentBase
{
  [Inject] protected ITokens Tokens { get; set; }
  
  [Parameter] public StyledBase Styled { get; set; }
  
  [Parameter(CaptureUnmatchedValues = true)] 
  public IDictionary<string, object> Params { get; set; }
  
  [Parameter] public RenderFragment ChildContent { get; set; }

  protected readonly StyledFactory Create;
  
  protected ElementReference ElementRef { get; set; }
  
  protected virtual string Style => string.Empty;

  private readonly Styled _componentStyle;

  public Styled ComponentStyle
  {
    get => _componentStyle ?? Component.ComponentStyle;
    private init => _componentStyle = value;
  }

  protected virtual StyledBase Component => Create.Div();
  
  public StyledBase()
  {
    Create = new StyledFactory(this);
  }

  public StyledBase(ITokens tokens) : this()
  {
    Tokens ??= tokens;
  }

  public StyledBase(Styled styled) : this()
  {
    ComponentStyle = styled;
  }
  
  protected override void OnInitialized() => StyleSheetService.Add(ComponentStyle);

  protected override void OnParametersSet()
  {
    if (Styled == null) return;
    UpdateStyle(Styled.ComponentStyle);
  }
  
  private void UpdateStyle(Styled styled)
  {
    StyleSheetService.Update(ComponentStyle.Id, styled);
    ComponentStyle.Update(styled);
  }

  protected override void BuildRenderTree(RenderTreeBuilder builder)
  {
    builder.OpenElement(0, ComponentStyle.Element);
    builder.AddAttribute(0, ComponentStyle.Id);
    if (!string.IsNullOrEmpty(Style)) builder.AddAttribute(0, "style", Style);
    builder.AddMultipleAttributes(0, Params);
    builder.AddElementReferenceCapture(0, value => ElementRef = value);
    builder.AddContent(0, ChildContent);
    builder.CloseElement();
  }
}