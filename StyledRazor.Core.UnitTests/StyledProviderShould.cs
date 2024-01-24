using StyledRazor.Core.Style;

namespace StyledRazor.Core.UnitTests;

public class StyledProviderShould
{
  private class TestComponent : StyledBase
  {
    public TestComponent(ITokens tokens) : base(tokens) {}

    public override Styled Base => Create.Div(@"{
      Property: Value;
    }");
  }

  [Test]
  public void GetStyled_FromAStyledBaseComponent()
  {
    var tokens = new Tokens();
    var styledProvider = new StyledProvider(tokens);
    var styled = styledProvider.Get(typeof(TestComponent));
    var expected = new TestComponent(tokens).Base;

    Assert.Multiple(() =>
    {
      Assert.That(styled.Type, Is.EqualTo(expected.Type));
      Assert.That(styled.CssString, Is.EqualTo(ExpectedCssStringWithScopeFrom(styled, expected)));
    });
  }
  
  private static string ExpectedCssStringWithScopeFrom(Styled styled, Styled expectedStyled) => 
    expectedStyled.CssString.Replace(expectedStyled.Id, styled.Id);
}