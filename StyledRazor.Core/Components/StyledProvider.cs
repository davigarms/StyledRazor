using StyledRazor.Core.Style.DesignTokens;
using System;

namespace StyledRazor.Core.Components;

public class StyledProvider
{
  private readonly ITokens _tokens;
  
  public StyledProvider(ITokens tokens)
  {
    _tokens = tokens;
  }

  public StyledBase CreateInstance<T>() where T : StyledBase =>
    Activator.CreateInstance(typeof(T), _tokens) as StyledBase;
}