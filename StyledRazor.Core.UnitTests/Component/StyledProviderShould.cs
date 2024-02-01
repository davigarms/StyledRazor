using StyledRazor.Core.Component.Styled;
using StyledRazor.Core.Style.ComponentStyle;
using StyledRazor.Core.Style.DesignTokens;

namespace StyledRazor.Core.UnitTests.Component;

public class ComponentStyleProviderShould
{
  private readonly Type _styleType = typeof(TestComponent);
  
  private readonly Tokens _tokens = new();
  
  private StyledProvider StyledProvider => new(_tokens);

  private class TestComponent : Styled
  {
    public TestComponent(ITokens tokens) : base(tokens) {}

    protected override Styled Component => CreateStyled.Div(@"{
      Property: Value;
    }");
  }

  [Test]
  public void GetComponent_FromAStyledComponent()
  {
    var styled = StyledProvider.Get(_styleType);
    
    Assert.That(styled.GetType(), Is.EqualTo(_styleType));
  }

  [Test]
  public void GetComponentStyle_FromAStyledComponent()
  {
    const string expectedCssString = "div[TestComponent]{Property:Value;}";
    
    var componentStyle = StyledProvider.Get(_styleType).ComponentStyle;

    Assert.Multiple(() =>
    {
      Assert.That(componentStyle.Type, Is.EqualTo(_styleType));
      Assert.That(componentStyle.CssString, Is.EqualTo(ExpectedCssStringWithScopeFrom(componentStyle, expectedCssString)));
    });
  }
  
  private static string ExpectedCssStringWithScopeFrom(ComponentStyle componentStyle, string expected) => 
    expected.Replace("TestComponent", componentStyle.Id);
}