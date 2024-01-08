using System;
using System.Collections.Generic;

namespace StyledRazor.Core.Style;

public class CssDeclarationDictionary : Dictionary<string, string>
{
  public event Action OnChange;

  public void Set(string property, string value)
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