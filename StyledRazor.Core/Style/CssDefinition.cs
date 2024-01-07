using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StyledRazor.Core.Style;

public class CssDefinition : Dictionary<string, string>
{
  public event Action OnChange;
  
  public void SetProperty(string property, string value)
  {
    if (ContainsKey(property))
    {
      if (this[property] == value) return;
      this[property] = value;  
    }
    else
    {
      Add(property, value);
    }

    OnChange?.Invoke();
  }
}