using StyledRazor.Core.Component;
using StyledRazor.Core.Style.ComponentStyle;
using StyledRazor.Core.Style.DesignTokens;

namespace StyledRazor.Core.UnitTests.Component;

public class ComponentStyleProviderShould
{
  private class TestComponent : Styled
  {
    public TestComponent(ITokens tokens) : base(tokens) {}

    protected override Styled Component => CreateStyled.Div(@"{
      Property: Value;
    }");
  }

  [Test]
  public void GetStyled_FromAStyledComponent()
  {
    var tokens = new Tokens();
    var styledProvider = new ComponentStyleProvider(tokens);
    const string expectedCssString = "div[TestComponent]{Property:Value;}";
    
    var styled = styledProvider.Get(typeof(TestComponent));

    Assert.Multiple(() =>
    {
      Assert.That(styled.Type, Is.EqualTo(typeof(TestComponent)));
      Assert.That(styled.CssString, Is.EqualTo(ExpectedCssStringWithScopeFrom(styled, expectedCssString)));
    });
  }
  
  private static string ExpectedCssStringWithScopeFrom(ComponentStyle componentStyle, string expected) => 
    expected.Replace("TestComponent", componentStyle.Id);
}