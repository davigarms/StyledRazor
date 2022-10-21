using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
    
namespace StyledRazor.Core.Components;

public class ParametersBase : ComponentBase
{
  [Parameter(CaptureUnmatchedValues = true)] 
  public IDictionary<string, object> Params { get; set; }
  [Parameter] public RenderFragment ChildContent { get; set; }
}