using StyledRazor.Core.Style.DesignTokens;
using System;

namespace StyledRazor.Core.Components.StyledComponent;

public class StyledProvider
{
  private readonly ITokens _tokens;
  
  public StyledProvider(ITokens tokens)
  {
    _tokens = tokens;
  }

  public Styled CreateInstance<T>() where T: Styled => 
    Activator.CreateInstance(typeof(T), _tokens) as Styled;
}