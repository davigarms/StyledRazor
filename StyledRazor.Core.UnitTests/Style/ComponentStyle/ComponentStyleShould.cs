using StyledRazor.Core.Component;
using StyledRazor.Core.Style.Css;

namespace StyledRazor.Core.UnitTests.Style.ComponentStyle;

public class ComponentStyleShould
{
  private StyledFactory _createFactory = new(new TestComponent());

  private class TestComponent : Styled {}

  public enum ValidElements
  {
    Div,
    A,
    H1,
    Li,
    Ul,
  }
  
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
    var createFactory = new StyledFactory(new TestComponent());
    var componentStyle1 = _createFactory.Div(string.Empty).ComponentStyle;
    var componentStyle2 = createFactory.Div("{Property: Value}").ComponentStyle;
    var expectedCss = "div[ScopeId]{Property:Value;}".Replace("ScopeId", componentStyle2.Id);
      
    componentStyle1.Update(componentStyle2);
    
    Assert.Multiple(() =>
    {
      Assert.That(componentStyle1.Id, Is.EqualTo(componentStyle2.Id));
      Assert.That(componentStyle1.CssString, Is.EqualTo(expectedCss));
    });
  }

  [Test]
  public void GetCssDeclarationDictionary_FromADivSelector()
  {
    var componentStyle = _createFactory.Div(Css).ComponentStyle;

    var styleDeclaration = componentStyle.Get("Child");
    
    Assert.That(styleDeclaration, Is.EqualTo(ExpectedDictionary));
  }

  [Test]
  public void GetCssDeclarationDictionary_FromAHyperlinkSelector()
  {
    var componentStyle = _createFactory.A(Css).ComponentStyle;

    var styleDeclaration = componentStyle.Get("Child");

    Assert.That(styleDeclaration, Is.EqualTo(ExpectedDictionary));
  }

  [Test]
  public void GetCssDeclarationDictionary_FromAnyValidSelector([Values] ValidElements elementName)
  {
    var componentStyle = (_createFactory.GetType()
                    .GetMethod(elementName.ToString())?
                    .Invoke(_createFactory, new object?[] { Css }) as Styled)?.ComponentStyle;
    
    var styleDeclaration = componentStyle?.Get("Child");

    Assert.That(styleDeclaration, Is.EqualTo(ExpectedDictionary));
  }
}