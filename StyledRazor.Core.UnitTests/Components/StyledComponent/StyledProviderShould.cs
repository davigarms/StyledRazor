using StyledRazor.Core.Components.StyledComponent;
using StyledRazor.Core.Style.Components;
using StyledRazor.Core.Style.DesignTokens;

namespace StyledRazor.Core.UnitTests.Components.StyledComponent;

public class StyledProviderShould
{
  private readonly Tokens _tokens = new();
  
  private StyledProvider StyledProvider => new(_tokens);

  private class TestComponent : Styled
  {
    public TestComponent(ITokens tokens) : base(tokens) {}

    protected override Styled BaseComponent => CreateStyled.Div(@"{
      Property: Value;
    }");
  }

  [Test]
  public void CreateAStyledComponentInstance()
  {
    var styled = StyledProvider.CreateInstance<TestComponent>();
    
    Assert.That(styled.GetType(), Is.EqualTo(typeof(TestComponent)));
  }

  [Test]
  public void GetComponentStyle_FromAStyledComponent()
  {
    const string expectedCssString = "div[TestComponent]{Property:Value;}";
    
    var componentStyle = StyledProvider.CreateInstance<TestComponent>().ComponentStyle;

    Assert.Multiple(() =>
    {
      Assert.That(componentStyle.Type, Is.EqualTo(typeof(TestComponent)));
      Assert.That(componentStyle.CssString, Is.EqualTo(ExpectedCssStringWithScopeFrom(componentStyle, expectedCssString)));
    });
  }
  
  private static string ExpectedCssStringWithScopeFrom(ComponentStyle componentStyle, string expected) => 
    expected.Replace("TestComponent", componentStyle.Id);
}