using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace StyledRazor.Core.Browser;

public class BrowserService 
{
  private readonly IBrowserConnector _browserConnector;
  public static event Func<Task> OnResize;
  
  public BrowserService(IBrowserConnector browserConnector)
  {
    _browserConnector = browserConnector;
  }
  
  public async Task<Dimension> WindowDimension() => 
    await _browserConnector.InvokeAsync<Dimension>("WindowDimension");

  public async Task<Dimension> DimensionFrom(ElementReference element) => 
    await _browserConnector.InvokeAsync<Dimension>("DimensionFrom", element);
  
  [JSInvokable]
  public static async Task OnBrowserResize()
  {
    if (OnResize is not null)
      await OnResize.Invoke();
  }
}