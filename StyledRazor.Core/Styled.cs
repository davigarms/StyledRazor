using System;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace StyledRazor.Core;

public class Styled
{
  public string Name { get; }
  public string Css { get; }
  public string Prefix { get; }
  
  public Styled(string name, string css, string prefix = null)
  {
    Name = name;
    Css = css;
    Prefix = prefix;
  }
  
  public Styled(string name, string css, MemberInfo member) : this (name, css, member.Name) {}
  public Styled(string name, string css, ComponentBase componentBase) : this (name, css, componentBase.GetType().Name) {}
}