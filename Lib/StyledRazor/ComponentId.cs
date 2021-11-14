using System;

namespace BlazorApp.Lib.StyledRazor
{
    public class ComponentId
    {
      public string Value {get;}

      public ComponentId(string prefix)
      {
          Value = $"{prefix.ToLower()}_{Guid.NewGuid().ToString()[..8]}";
      }
    }
}