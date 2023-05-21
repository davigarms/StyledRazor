using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace StyledRazor.Core.Browser;

public class BrowserService
{
  private readonly IJSRuntime _js;

  public BrowserService(IJSRuntime js)
  {
    _js = js;
  }

  public async Task<Dimension> WindowDimension() => 
    await _js.InvokeAsync<Dimension>("windowDimension");

  public async Task<Dimension> DimensionFrom(ElementReference element) =>
    await _js.InvokeAsync<Dimension>("dimensionFrom", element);
  
  public static event Func<Task> OnResize;

  [JSInvokable]
  public static async Task OnBrowserResize() => await OnResize.Invoke();
}