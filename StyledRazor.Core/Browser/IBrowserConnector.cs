using System.Threading.Tasks;

namespace StyledRazor.Core.Browser;

public interface IBrowserConnector
{
  ValueTask<TValue> InvokeAsync<TValue>(string identifier, params object[] args);
}