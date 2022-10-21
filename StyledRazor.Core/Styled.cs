using System;
using System.Reflection;

namespace StyledRazor.Core;

public class Styled
{
  public string BaseElement { get; }
  public string BaseCss { get; }
  public string Name { get; }
  
  public Styled(string baseElement, string baseCss, string name = null)
  {
    BaseElement = baseElement;
    BaseCss = baseCss;
    Name = name;
  }
  
  public Styled(string baseElement, string baseCss, MemberInfo type) : this (baseElement, baseCss, type.Name) {}

  public static Styled Div(string baseCss) => new(MethodBase.GetCurrentMethod()?.Name, baseCss);
  public static Styled H1(string baseCss) => new(MethodBase.GetCurrentMethod()?.Name, baseCss);
}