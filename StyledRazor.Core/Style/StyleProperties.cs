using System.Collections.Generic;

namespace StyledRazor.Core.Style;

public class StyleProperties : Dictionary<string, string>
{
  public void SetProperty(string property, string value)
  {
    if (ContainsKey(property) && this[property] == value) return;
    
    this[property] = value;
  }
}