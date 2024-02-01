using StyledRazor.Core.Component;
using StyledRazor.Core.Style.DesignTokens;
using System;

namespace StyledRazor.Core.Model;

public class ComponentStyleProvider
{
  private readonly ITokens _tokens;
  
  public ComponentStyleProvider(ITokens tokens)
  {
    _tokens = tokens;
  }

  public ComponentStyle Get(Type styleType) => 
    (Activator.CreateInstance(styleType, _tokens) as Styled)?.ComponentStyle;
}