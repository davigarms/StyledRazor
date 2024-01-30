using StyledRazor.Core.Component;
using StyledRazor.Core.Model;

namespace StyledRazor.Lib.Components.Layout;

public class Layout : StyledBase
{
  public override Styled Base => Create.Div();
}