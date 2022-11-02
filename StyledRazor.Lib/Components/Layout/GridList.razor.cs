using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using StyledRazor.Core;
using StyledRazor.Core.Components;
using StyledRazor.Core.Services;
using StyledRazor.Lib.Styles.Base;

namespace StyledRazor.Lib.Components.Layout;

public class GridListBase : StyledBase, IDisposable
{
  [Inject]
  protected BrowserService Service { get; set; }

  [Parameter]
  public string Gutter { get; set; } = Tokens.SpacingS;

  [Parameter]
  public string Height { get; set; }

  [Parameter]
  public int Cols { get; set; } = 1;

  [Parameter]
  public double GridRatio { get; set; }

  protected string CalculatedHeight { get; private set; }

  protected override Styled Base => UL(
    @"
			{
				display: inline-flex;
        flex-wrap: wrap;
        margin: calc(-1 * var(--gutter)) 0 0 calc(-2 * var(--gutter));
        width: calc(100% + var(--gutter));
        position: relative;
			}

      > * {
        width: calc(var(--width) - var(--gutter)) !important;
        margin: var(--gutter) 0 0 var(--gutter);
        height: var(--height) !important;
        max-height: var(--height);
        overflow: hidden;
      }
		"
  );

  protected ElementReference ElementRef;

  protected override Task OnInitializedAsync() => InitComponent();

  private async Task InitComponent()
  {
    BrowserService.OnResize += WindowSizeHasChanged;
    await Task.Delay(1);
    await WindowSizeHasChanged();
  }

  private async Task WindowSizeHasChanged()
  {
    await SetCalculatedHeight();
    StateHasChanged();
  }

  private async Task SetCalculatedHeight()
  {
    var elementDimension = await Service.DimensionFrom(ElementRef);
    CalculatedHeight = !string.IsNullOrEmpty(Height) ? Height :
      GridRatio != 0 ? $"{((double)elementDimension.Width / Cols - Utils.RemToInt(Gutter)) / GridRatio}px" : "initial";
  }

  public void Dispose() => BrowserService.OnResize -= WindowSizeHasChanged;
}
