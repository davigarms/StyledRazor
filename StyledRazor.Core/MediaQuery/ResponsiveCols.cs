using System.Collections.Generic;

namespace StyledRazor.Core.MediaQuery;

public class ResponsiveCols : Dictionary<BreakPoint, int> { }

public enum BreakPoint
{
  Xs = 0,
  Sm = 480,
  Md = 768,
  Lg = 960,
  Xl = 1280,
  Xxl = 1536,
}