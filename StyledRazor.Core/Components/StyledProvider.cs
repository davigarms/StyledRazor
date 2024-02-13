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

  public StyledBase Get(Type styleType) => 
    Activator.CreateInstance(styleType, _tokens) as StyledBase;
}