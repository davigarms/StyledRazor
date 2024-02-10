using StyledRazor.Core.Style.DesignTokens;

namespace StyledRazor.Core.MediaQuery;

public class BreakPoints
{
  private readonly ITokens _tokens;

  public BreakPoints(ITokens tokens)
  {
    _tokens = tokens;
  }
  
  public int[] Values => new[]
                         {
                           _tokens.BreakPointXs,
                           _tokens.BreakPointSm,
                           _tokens.BreakPointMd,
                           _tokens.BreakPointLg,
                           _tokens.BreakPointXl,
                           _tokens.BreakPointXxl,
                         };
}