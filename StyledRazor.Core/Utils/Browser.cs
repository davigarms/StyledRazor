namespace StyledRazor.Core.Utils;

public static class Browser
{
  public static double RemToInt(string rem) => 
    double.Parse(rem.Replace(" ", "").Replace("rem", "")) * 16;
}