using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace StyledRazor.Core.Browser;

public class BrowserService : IAsyncDisposable
{
  private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

  public BrowserService(IJSRuntime js)
  {
    _moduleTask = new(() => js.InvokeAsync<IJSObjectReference>(
      "import", "./_content/StyledRazor.Core/Browser/BrowserService.js").AsTask());
  }

  public async Task<Dimension> WindowDimension()
  {
    var module = await _moduleTask.Value;
    return await module.InvokeAsync<Dimension>("WindowDimension");
  }

  public async Task<Dimension> DimensionFrom(ElementReference element)
  {
    var module = await _moduleTask.Value;
    return await module.InvokeAsync<Dimension>("DimensionFrom", element);
  }

  public static event Func<Task> OnResize;

  [JSInvokable]
  public static async Task OnBrowserResize() => await OnResize.Invoke();
  
  public async ValueTask DisposeAsync()
  {
    if (_moduleTask.IsValueCreated)
    {
      var module = await _moduleTask.Value;
      await module.DisposeAsync();
    }
  }
}