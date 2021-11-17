using System;

namespace StyledRazor.Styled
{
    public class ComponentId
    {
      public string Value {get;}

      public ComponentId(string prefix = "wrapper")
      {
          Value = $"{prefix.ToLower()}_{Guid.NewGuid().ToString()[..8]}";
      }
    }
}