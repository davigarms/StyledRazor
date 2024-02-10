using StyledRazor.Core.MediaQuery;

namespace StyledRazor.Core.Style.DesignTokens;

public class Tokens : ITokens
{
  public int BreakPointXs => 0;
  public int BreakPointSm => 480;
  public int BreakPointMd => 768;
  public int BreakPointLg => 960;
  public int BreakPointXl => 1280;
  public int BreakPointXxl => 1536;
  public string Zero => "0";
  public string Auto => "auto";
  public string SpacingXs => ".25rem";
  public string SpacingS => ".5rem";
  public string SpacingM => "1rem";
  public string SpacingL => "2rem";
  public string SpacingXl => "4rem";
  public string SpacingXxl => "8rem";
  public string Initial => "initial";
  public string SizeViewPortWidth => "100vw";
  public string SizeViewportHeight => "100vh";
  public string SizeTotal => "100%";
  public string AlignLeft => "left";
  public string AlignCenter => "center";
  public string AlignRight => "right";
  public string AlignJustify => "justify";
  public string AlignSpaceBetween => "space-between";
  public string AlignTop => "top";
  public string AlignEnd => "end";
  public string AlignFlexStart => "flex-start";
  public string AlignFlexEnd => "flex-end";
  public string DisplayInlineFlex => "inline-flex";
  public string DisplayFlex => "flex";
  public string DisplayBlock => "block";
  public string DirectionColumn => "column";
  public string DirectionRow => "row";
  public string FlexWrap => "wrap";
  public string FlexWrapReverse => "wrap-reverse";
  public string FlexNoWrap => "nowrap";
  public string ScreenXs => $"{BreakPointXs}px";
  public string ScreenS => $"{BreakPointSm}px";
  public string ScreenM => $"{BreakPointMd}px";
  public string ScreenLg => $"{BreakPointLg}px";
  public string ScreenXlg => $"{BreakPointXl}px";
  public string ScreenXxg => $"{BreakPointXxl}px";
}