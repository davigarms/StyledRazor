using StyledRazor.Core.Components;
using StyledRazor.Core.Style.DesignTokens;

namespace StyledRazor.Core.UnitTests.Components;

public class StyledProviderShould
{
  private readonly Tokens _tokens = new();

  private StyledProvider StyledProvider => new(_tokens);
  
  private class TestComponent : StyledBase
  {
    public TestComponent(ITokens tokens) : base(tokens) {}
    
    protected override Styled BaseComponent => CreateStyled.Div(@"{
      Property: Value;
    }");

    public Styled GetBaseComponent() => BaseComponent;
  }

  [Test]
  public void CreateAStyledComponentInstance()
  {
    var styled = StyledProvider.CreateInstance<TestComponent>();

    Assert.That(styled.GetType(), Is.EqualTo(typeof(TestComponent)));
  }

  [Test]
  public void GetStyled_FromAStyledComponent()
  {
    const string expectedCssString = "div[TestComponent]{Property:Value;}";
    
    var styledBase = StyledProvider.CreateInstance<TestComponent>();
    var baseComponent = ((TestComponent)styledBase).GetBaseComponent();

    Assert.Multiple(() =>
    {
      Assert.That(styledBase.GetType(), Is.EqualTo(typeof(TestComponent)));
      Assert.That(baseComponent.CssString, Is.EqualTo(ExpectedCssStringWithScopeFrom(baseComponent, expectedCssString)));
    });
  }
  
  private static string ExpectedCssStringWithScopeFrom(Styled styled, string expected) => 
    expected.Replace("TestComponent", styled.Id);
}