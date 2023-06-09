using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;
using System;

namespace StyledRazor.Core.StyleSheet;

public class StyleSheet : ComponentBase, IDisposable
{
  private RenderFragment StyleElement { get; set; }

  private static StyleSheetService StyleSheetService => StyleSheetService.GetInstance();

  
  protected override void OnInitialized() => StyleSheetService.OnUpdate += UpdateStyleSheet;

  private async Task UpdateStyleSheet()
  {
    StyleElement = StyleSheetService.CreateStyleSheet();
    await InvokeAsync(StateHasChanged);
  }

  protected override void BuildRenderTree(RenderTreeBuilder builder)
  {
    builder.AddContent(0, StyleElement);
  }

  public void Dispose()
  {
    StyleSheetService.Clear();
    StyleSheetService.OnUpdate -= UpdateStyleSheet;
    GC.SuppressFinalize(this);
  }
}