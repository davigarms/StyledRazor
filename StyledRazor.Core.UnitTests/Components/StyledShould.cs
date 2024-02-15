using StyledRazor.Core.Components;
using StyledRazor.Core.Style.Css;

namespace StyledRazor.Core.UnitTests.Components;

public class StyledShould
{
  private StyledFactory CreateStyled { get; set; } = new(new TestComponent());

  private class TestComponent : Styled {}

  private const string Css = @"{
      Property: Value
    }

    Child {
      Property1: Value;
      Property2: Value;
    }";

  private static readonly CssStyleDeclaration ExpectedDictionary = new()
                                                                   {
                                                                     ["Property1"] = "Value",
                                                                     ["Property2"] = "Value",
                                                                   };


  [SetUp]
  public void SetUp() => CreateStyled = new StyledFactory(new TestComponent());

  [Test]
  public void UpdateStyled()
  {
    var createAnother = new StyledFactory(new TestComponent());
    var styled1 = CreateStyled.Div(string.Empty);
    var styled2 = createAnother.Div("{Property: Value}");
    const string expectedCss = "div[TestComponent]{Property:Value;}";

    styled1.Update(styled2);

    Assert.Multiple(() =>
                    {
                      Assert.That(styled1.Id, Is.EqualTo(styled2.Id));
                      Assert.That(styled1.CssString, Is.EqualTo(ExpectedCssStringWithScopeFrom(styled2, expectedCss)));
                    });
  }

  [Test]
  public void GetCssDeclarationDictionary()
  {
    var styled = CreateStyled.A(Css);

    var styleDeclaration = styled.Get("Child");

    Assert.That(styleDeclaration, Is.EqualTo(ExpectedDictionary));
  }

  private static string ExpectedCssStringWithScopeFrom(Styled styled, string expected) =>
    expected.Replace("TestComponent", styled.Id);
}