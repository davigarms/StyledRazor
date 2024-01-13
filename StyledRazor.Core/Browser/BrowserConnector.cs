using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace StyledRazor.Core.Browser;

public class BrowserConnector : IAsyncDisposable, IBrowserConnector
{
  private readonly Lazy<Task<IJSObjectReference>> _jsObjectReferenceTask;

  public BrowserConnector(IJSRuntime js)
  {
    _jsObjectReferenceTask = new Lazy<Task<IJSObjectReference>>(() => js.InvokeAsync<IJSObjectReference>(
      "import", "./_content/StyledRazor.Core/Browser/BrowserService.js")
      .AsTask());
  }
  
  public async ValueTask<TValue> InvokeAsync<TValue>(string identifier, params object[] args)
  {
    var jsObjectReference = await _jsObjectReferenceTask.Value;
    return await jsObjectReference.InvokeAsync<TValue>(identifier, args);
  }

  public async ValueTask DisposeAsync()
  {
    if (_jsObjectReferenceTask.IsValueCreated)
    {
      var jsObjectReference = await _jsObjectReferenceTask.Value;
      await jsObjectReference.DisposeAsync();
      GC.SuppressFinalize(this);
    }
  }
}