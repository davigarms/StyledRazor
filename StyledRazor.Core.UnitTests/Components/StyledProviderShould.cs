using StyledRazor.Core.Components;
using StyledRazor.Core.Style.DesignTokens;

namespace StyledRazor.Core.UnitTests.Components;

public class StyledProviderShould
{
  private class TestComponent : StyledBase
  {
    public TestComponent(ITokens tokens) : base(tokens) {}

    protected override Styled BaseComponent => Create.Div(@"{
      Property: Value;
    }");

    public Styled GetBaseComponent() => BaseComponent;
  }

  [Test]
  public void GetStyled_FromAStyledComponent()
  {
    var tokens = new Tokens();
    var styledProvider = new StyledProvider(tokens);
    const string expectedCssString = "div[TestComponent]{Property:Value;}";
    
    var styled = styledProvider.Get(typeof(TestComponent));
    var baseComponent = ((TestComponent)styled).GetBaseComponent();

    Assert.Multiple(() =>
    {
      Assert.That(styled.GetType(), Is.EqualTo(typeof(TestComponent)));
      Assert.That(baseComponent.CssString, Is.EqualTo(ExpectedCssStringWithScopeFrom(baseComponent, expectedCssString)));
    });
  }
  
  private static string ExpectedCssStringWithScopeFrom(Styled styled, string expected) => 
    expected.Replace("TestComponent", styled.Id);
}