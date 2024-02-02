using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using StyledRazor.Core.Style.Components;
using StyledRazor.Core.Style.DesignTokens;
using StyledRazor.Core.Style.StyleSheet;
using System.Collections.Generic;

namespace StyledRazor.Core.Components.StyledComponent;

public class Styled : ComponentBase
{
  [Inject] protected ITokens Tokens { get; set; }

  [Parameter] public Styled Base { get; set; }

  [Parameter(CaptureUnmatchedValues = true)]
  public IDictionary<string, object> Params { get; set; }

  [Parameter] public RenderFragment ChildContent { get; set; }

  protected readonly StyledFactory CreateStyled;

  protected ElementReference ElementRef { get; private set; }

  protected virtual string InlineStyle => string.Empty;

  private readonly ComponentStyle _componentStyle;

  public ComponentStyle ComponentStyle
  {
    get => _componentStyle ?? ComponentBase.ComponentStyle;
    private init => _componentStyle = value;
  }

  protected virtual Styled ComponentBase => CreateStyled.Div();

  public Styled()
  {
    CreateStyled = new StyledFactory(this);
  }

  public Styled(ITokens tokens) : this()
  {
    Tokens ??= tokens;
  }

  public Styled(ComponentStyle componentStyle) : this()
  {
    ComponentStyle = componentStyle;
  }

  protected override void OnInitialized() => StyleSheetService.Add(ComponentStyle);

  protected override void OnParametersSet()
  {
    if (Base == null) return;
    UpdateStyle(Base.ComponentStyle);
  }

  private void UpdateStyle(ComponentStyle componentStyle)
  {
    StyleSheetService.Update(ComponentStyle.Id, componentStyle);
    ComponentStyle.Update(componentStyle);
  }

  protected override void BuildRenderTree(RenderTreeBuilder builder)
  {
    builder.OpenElement(0, ComponentStyle.Element);
    builder.AddAttribute(0, ComponentStyle.Id);
    if (!string.IsNullOrEmpty(InlineStyle)) builder.AddAttribute(0, "style", InlineStyle);
    builder.AddMultipleAttributes(0, Params);
    builder.AddElementReferenceCapture(0, value => ElementRef = value);
    builder.AddContent(0, ChildContent);
    builder.CloseElement();
  }
}