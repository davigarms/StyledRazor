using System.Collections.Generic;

namespace StyledRazor.Core;

public class StyleProperties : Dictionary<string, string>
{
  public event UpdateHandler OnUpdate;

  public delegate void UpdateHandler();

  public void SetProperty(string property, string value)
  {
    if (value == this[property]) return;
    
    this[property] = value;
    OnUpdate?.Invoke();
  }
}