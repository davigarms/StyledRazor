using StyledRazor.Core;
using StyledRazor.Core.Component;
using System.Text.Json.Serialization;

namespace StyledRazor.Lib.Components.Layout;

public class Column : StyledBase
{
  
  
  public override Styled Base => Create.Div();
}