using StyledRazor.Core.Component;
using StyledRazor.Core.Model;
using StyledRazor.Core.Style.DesignTokens;

namespace StyledRazor.Core.UnitTests.Component;

public class StyledProviderShould
{
  private class TestComponent : StyledBase
  {
    public TestComponent(ITokens tokens) : base(tokens) {}

    protected override StyledBase Component => Create.Div(@"{
      Property: Value;
    }");
  }

  [Test]
  public void GetStyled_FromAStyledComponent()
  {
    var tokens = new Tokens();
    var styledProvider = new StyledProvider(tokens);
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