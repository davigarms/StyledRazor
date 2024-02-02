using StyledRazor.Core.Component.Styled;
using StyledRazor.Core.Style.Css;

namespace StyledRazor.Core.UnitTests.Style.Component;

public class ComponentStyleShould
{
  private StyledFactory _createFactory = new(new TestComponent());

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
  public void SetUp() => _createFactory = new StyledFactory(new TestComponent());

  [Test]
  public void UpdateStyled()
  {
    var anotherFactory = new StyledFactory(new TestComponent());
    var componentStyle1 = _createFactory.Div(string.Empty).ComponentStyle;
    var componentStyle2 = anotherFactory.Div("{Property: Value}").ComponentStyle;
    var expectedCss = "div[ScopeId]{Property:Value;}".Replace("ScopeId", componentStyle2.Id);

    componentStyle1.Update(componentStyle2);

    Assert.Multiple(() =>
    {
      Assert.That(componentStyle1.Id, Is.EqualTo(componentStyle2.Id));
      Assert.That(componentStyle1.CssString, Is.EqualTo(expectedCss));
    });
  }

  [Test]
  public void GetCssDeclarationDictionary()
  {
    var componentStyle = _createFactory.Div(Css).ComponentStyle;

    var styleDeclaration = componentStyle.Get("Child");

    Assert.That(styleDeclaration, Is.EqualTo(ExpectedDictionary));
  }
}