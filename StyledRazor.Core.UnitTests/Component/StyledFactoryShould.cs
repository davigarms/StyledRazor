using StyledRazor.Core.Component.Styled;
using System.Text.Json;

namespace StyledRazor.Core.UnitTests.Component;

[TestFixture]
public class StyledFactoryShould
{
  private const string UnexpectedSpace = " ";
  private const string MalformedSelectorWithColon = $":{UnexpectedSpace}Name";
  
  private StyledFactory _createStyled = new(new TestComponent());

  private class TestComponent : Styled {}

  private static IEnumerable<(string, string)> CssCases()
  {
    yield return (@"{
                    Property1: Value;
                  }", "Element[ScopeId]{Property1:Value;}");

    yield return (@"{
                    Property1 : Value;
                  }", "Element[ScopeId]{Property1:Value;}");

    yield return (@"{
                    Property1: Value ;
                  }", "Element[ScopeId]{Property1:Value;}");

    yield return (@"{
                    Property1: Value;
                    Property2: Value;
                  }", "Element[ScopeId]{Property1:Value;Property2:Value;}");

    yield return (@"{
                    Property1: Value;     Property2: Value;
                  }", "Element[ScopeId]{Property1:Value;Property2:Value;}");

    yield return (@"{Property1: Value; Property2: Value;}", "Element[ScopeId]{Property1:Value;Property2:Value;}");

    yield return (@"{
                      Property1: Value;
                      Property2: Value;
                    }
                    Selector {
                      Property1: Value;
                    }
                  ", "Element[ScopeId]{Property1:Value;Property2:Value;}Element[ScopeId]Selector{Property1:Value;}");

    yield return (@"{
                      Property1: Value;
                      Property2: Value;
                    }

                    Selector {
                      Property1: Value;
                    }", "Element[ScopeId]{Property1:Value;Property2:Value;}Element[ScopeId]Selector{Property1:Value;}");

    yield return (@"{
                      Property1: Value;
                      Property2: Value;
                    }


                    Selector {
                      Property1: Value;
                    }

                    ", "Element[ScopeId]{Property1:Value;Property2:Value;}Element[ScopeId]Selector{Property1:Value;}");

    yield return (@"{
                      Property1: Value;
                      Property2: Value;
                    }
                    :Selector {
                      Property1: Value;
                    }
                  ", "Element[ScopeId]{Property1:Value;Property2:Value;}Element[ScopeId]:Selector{Property1:Value;}");

    yield return (@"{
                      Property1: Value;
                      Property2: Value;
                    }
                    ::Selector {
                      Property1: Value;
                    }
                  ", "Element[ScopeId]{Property1:Value;Property2:Value;}Element[ScopeId]::Selector{Property1:Value;}");

    yield return (@"{
                      Property1: Value;
                      Property2: Value;
                    }
                    > :Selector {
                      Property1: Value;
                    }
                  ", "Element[ScopeId]{Property1:Value;Property2:Value;}Element[ScopeId]> :Selector{Property1:Value;}");

    yield return (@"{
                      Property1: Value;
                      Property2: Value;
                    }
                    >:Selector {
                      Property1: Value;
                    }
                  ", "Element[ScopeId]{Property1:Value;Property2:Value;}Element[ScopeId]>:Selector{Property1:Value;}");

    yield return (@"{
                      Property1: Value;
                      Property2: Value;
                    }
                    >  :Selector {
                      Property1: Value;
                    }
                  ", "Element[ScopeId]{Property1:Value;Property2:Value;}Element[ScopeId]>:Selector{Property1:Value;}");
  }

  [SetUp]
  public void SetUp() => _createStyled = new StyledFactory(new TestComponent());

  [TestCaseSource(nameof(CssCases))]
  public void CreateAStyledDiv_WithMinifiedAndScopedCss((string original, string minified) css)
  {
    var styled = _createStyled.Div(css.original);
    var componentStyle = styled.ComponentStyle;

    Assert.That(componentStyle.CssString, Is.EqualTo(MinifiedCssWithScopeFor("div", componentStyle.Id, css.minified)));
  }

  [TestCaseSource(nameof(CssCases))]
  public void CreateAStyledHyperLink_WithMinifiedAndScopedCss((string original, string minified) css)
  {
    var styled = _createStyled.A(css.original);
    var componentStyle = styled.ComponentStyle;

    Assert.That(componentStyle.CssString, Is.EqualTo(MinifiedCssWithScopeFor("a", componentStyle.Id, css.minified)));
  }

  [TestCaseSource(nameof(CssCases))]
  public void CreateAStyledH1_WithMinifiedAndScopedCss((string original, string minified) css)
  {
    var styled = _createStyled.H1(css.original);
    var componentStyle = styled.ComponentStyle;

    Assert.That(componentStyle.CssString, Is.EqualTo(MinifiedCssWithScopeFor("h1", componentStyle.Id, css.minified)));
  }

  [TestCaseSource(nameof(CssCases))]
  public void CreateAStyledUl_WithMinifiedAndScopedCss((string original, string minified) css)
  {
    var styled = _createStyled.Ul(css.original);
    var componentStyle = styled.ComponentStyle;

    Assert.That(componentStyle.CssString, Is.EqualTo(MinifiedCssWithScopeFor("ul", componentStyle.Id, css.minified)));
  }

  [TestCaseSource(nameof(CssCases))]
  public void CreateAStyledLi_WithMinifiedAndScopedCss((string original, string minified) css)
  {
    var styled = _createStyled.Li(css.original);
    var componentStyle = styled.ComponentStyle;

    Assert.That(componentStyle.CssString, Is.EqualTo(MinifiedCssWithScopeFor("li", componentStyle.Id, css.minified)));
  }

  [TestCaseSource(nameof(CssCases))]
  public void ReturnTheExistingStyledComponent_WhenStyledAlreadyExistsInFactory((string original, string minified) css)
  {
    var styled1 = _createStyled.Div(css.original);
    var componentStyle1 = styled1.ComponentStyle;
    var styled2 = _createStyled.Div(css.original);
    var componentStyle2 = styled2.ComponentStyle;

    Assert.That(componentStyle2.Id, Is.EqualTo(componentStyle1.Id));
  }

  [Test]
  public void ThrowJsonException_WhenCssIsMalformed()
  {
    const string css = @$"
      {MalformedSelectorWithColon} {{
        Property: Value
      }}";

    Assert.Throws<JsonException>(() => _createStyled.Div(css));
  }

  private static string MinifiedCssWithScopeFor(string element, string scopeId, string cssMinified) =>
    cssMinified.Replace("ScopeId", scopeId).Replace("Element", element);
}