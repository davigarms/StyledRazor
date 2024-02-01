using StyledRazor.Core.Style.DesignTokens;
using System;

namespace StyledRazor.Core.Component.Styled;

public class StyledProvider
{
  private readonly ITokens _tokens;
  
  public StyledProvider(ITokens tokens)
  {
    _tokens = tokens;
  }

  public Styled Get(Type styleType) => Activator.CreateInstance(styleType, _tokens) as Styled;
}