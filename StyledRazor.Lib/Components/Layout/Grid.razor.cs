using static StyledRazor.Core.Style.Css.CssHelper;
using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Browser;
using StyledRazor.Core.Component;
using StyledRazor.Core.MediaQuery;
using StyledRazor.Core.Model;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace StyledRazor.Lib.Components.Layout;

public class Grid : StyledBase, IDisposable
{
  public override Styled Base => Create.Ul(@"{
    list-style: none;
	  display: flex;
    flex-wrap: wrap;
    padding: 0;
    margin: 0;
    margin-left: calc(-1 * var(--gutter));
    width: calc(100% + var(--gutter));
    position: relative;
  }

  > * {
    width: calc(var(--width) - 1 * var(--gutter)) !important;
    margin: var(--gutter) 0 0 var(--gutter) !important;
    height: var(--height) !important;
    max-height: var(--height);
    overflow: hidden;
    flex-grow: var(--flex-grow);
  }

  > [double] {
    flex-basis: calc(2 * (var(--width)) - 1 * var(--gutter)) !important
  }

  > [triple] {
    flex-basis: calc(3 * (var(--width)) - 1 * var(--gutter)) !important
  }

  > [half] {
    flex-basis: calc(50% - 1 * var(--gutter)) !important
  }

  > [third] {
    flex-basis: calc(33% - 1 * var(--gutter)) !important
  }

  > [full] {
    flex-basis: 100% !important
  }");

  protected override string Style => $@"
    --height: {CalculatedHeight};
    --width: {CalculatedWidth};
    --flex-grow: {FlexGrow};
    --gutter: {Gutter ?? Tokens.SpacingS};
  ";

  [Parameter] public string Gutter { get; set; }

  [Parameter] public bool Grow { get; set; } = true;

  [Parameter] public string Height { get; set; }

  [Parameter] public string BaseWidth { get; set; }

  [Parameter] public double Ratio { get; set; }

  [Parameter] public Columns Columns { get; set; } = new();

  [Inject] private BrowserService Browser { get; set; }

  private Dimension ElementDimension { get; set; } = new();

  private Dimension WindowDimension { get; set; } = new();
  
  private int ActualCols  => Columns.NumberOfColumnsFor(WindowDimension.Width) ?? 1;

  private string CalculatedHeight => ActualCols == 1 && string.IsNullOrEmpty(BaseWidth) ? "initial" :
                                     HasHeight ? Height :
                                     HasRatio ? HeightFrom(ElementDimension.Width) :
                                     "initial";

  private string CalculatedWidth => string.IsNullOrEmpty(BaseWidth) ? $"{100 / ActualCols}%" : $"{BaseWidth}";

  private string FlexGrow => HasBaseWidth ?
                               Grow ? "1" : "0"
                               : "0";

  private bool HasBaseWidth => !string.IsNullOrEmpty(BaseWidth);

  private bool HasHeight => !string.IsNullOrEmpty(Height);

  private bool HasRatio => Ratio != 0;

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender)
    {
      BrowserService.OnResize += WindowSizeHasChanged;
      await WindowSizeHasChanged();
    }
  }

  private async Task WindowSizeHasChanged()
  {
    WindowDimension = await Browser.WindowDimension();
    ElementDimension = await Browser.DimensionFrom(ElementRef);
    StateHasChanged();
  }
  
  private string HeightFrom(int elementWidth) => 
    $"{((double)elementWidth / ActualCols - (Gutter ?? Tokens.SpacingS).ToInt()) / Ratio}px";

  public void Dispose()
  {
    BrowserService.OnResize -= WindowSizeHasChanged;
    GC.SuppressFinalize(this);
  }
}
