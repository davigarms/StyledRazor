using StyledRazor.Core.Component;
using StyledRazor.Core.Style;
using StyledRazor.Core.Style.Css;

namespace StyledRazor.Core.UnitTests;

public class StyledShould
{
  private StyledFactory _create = new(new TestComponent());

  private class TestComponent : StyledBase {}
  
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
  public void SetUp() => _create = new StyledFactory(new TestComponent());

  [Test]
  public void UpdateStyled()
  {
    var createBase = new StyledFactory(new TestComponent());
    var styledBase = createBase.Div(string.Empty);
    var styledDiv = _create.Div("{Property: Value}");
    var expectedCss = "div[ScopeId]{Property:Value;}".Replace("ScopeId", styledDiv.Id);
      
    styledBase.Update(styledDiv);
    
    Assert.Multiple(() =>
    {
      Assert.That(styledBase.Id, Is.EqualTo(styledDiv.Id));
      Assert.That(styledBase.CssString, Is.EqualTo(expectedCss));
    });
  }

  [Test]
  public void GetCssDeclarationDictionaryFromADivSelector()
  {
    var styled = _create.Div(Css);

    var dictionary = styled.Get("Child");
    
    Assert.That(dictionary, Is.EquivalentTo(ExpectedDictionary));
  }

  [Test]
  public void GetCssDeclarationDictionaryFromAHyperlinkSelector()
  {
    var styled = _create.A(Css);

    var dictionary = styled.Get("Child");

    Assert.That(dictionary, Is.EquivalentTo(ExpectedDictionary));
  }
}