using StyledRazor.Core.MediaQuery;

namespace StyledRazor.Lib.Styles;

public static class Tokens
{
    public const string Zero = "0";
    public const string Auto = "auto";
    public const string SpacingXs = ".25rem";
    public const string SpacingS = ".5rem";
    public const string SpacingM = "1rem";
    public const string SpacingL = "2rem";
    public const string SpacingXl = "4rem";
    public const string SpacingXxl = "8rem";
    public const string Initial = "initial";
    public const string SizeViewPortWidth = "100vw";
    public const string SizeViewportHeight = "100vh";
    public const string SizeTotal = "100%";
    public const string AlignLeft = "left";
    public const string AlignCenter = "center";
    public const string AlignRight = "right";
    public const string AlignJustify = "justify";
    public const string AlignSpaceBetween = "space-between";
    public const string AlignTop = "top";
    public const string AlignEnd = "end";
    public const string AlignFlexStart = "flex-start";
    public const string AlignFlexEnd = "flex-end";
    public const string DisplayInlineFlex = "inline-flex";
    public const string DisplayFlex = "flex";
    public const string DisplayBlock = "block";
    public const string DirectionColumn = "column";
    public const string DirectionRow = "row";
    public const string FlexWrap = "wrap";
    public const string FlexWrapReverse = "wrap-reverse";
    public const string FlexNoWrap = "nowrap";
    public static string ScreenXs => $"{(int)BreakPoint.Xs}px";
    public static string ScreenS => $"{(int)BreakPoint.Sm}px";
    public static string ScreenM => $"{(int)BreakPoint.Md}px";
    public static string ScreenLg => $"{(int)BreakPoint.Lg}px";
    public static string ScreenXlg => $"{(int)BreakPoint.Xl}px";
    public static string ScreenXxg => $"{(int)BreakPoint.Xxl}px";
}