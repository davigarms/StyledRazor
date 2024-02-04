using System.Collections.Generic;

namespace StyledRazor.Core.MediaQuery;

public class MediaQuery : Dictionary<BreakPoint, int> { }

public enum BreakPoint
{
  Xs = 0,
  Sm = 480,
  Md = 768,
  Lg = 960,
  Xl = 1280,
  Xxl = 1536,
}