using StyledRazor.Core.Components;
using System.Text.Json;

namespace StyledRazor.Core.UnitTests.Components;

[TestFixture]
public class StyledFactoryShould
{
  private const string UnexpectedSpace = " ";
  private const string MalformedSelectorWithColon = $":{UnexpectedSpace}Name";

  private StyledFactory CreateStyled { get; set; } = new(new TestComponent());

  private class TestComponent : StyledBase {}

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
  public void SetUp() => CreateStyled = new StyledFactory(new TestComponent());

  [TestCaseSource(nameof(CssCases))]
  public void CreateAStyledDiv_WithMinifiedAndScopedCss((string original, string minified) css)
  {
    var styled = CreateStyled.Div(css.original);

    Assert.That(styled.CssString, Is.EqualTo(MinifiedCssWithScopeFor("div", styled.Id, css.minified)));
  }

  [TestCaseSource(nameof(CssCases))]
  public void CreateAStyledHyperLink_WithMinifiedAndScopedCss((string original, string minified) css)
  {
    var styled = CreateStyled.A(css.original);

    Assert.That(styled.CssString, Is.EqualTo(MinifiedCssWithScopeFor("a", styled.Id, css.minified)));
  }

  [TestCaseSource(nameof(CssCases))]
  public void CreateAStyledH1_WithMinifiedAndScopedCss((string original, string minified) css)
  {
    var styled = CreateStyled.H1(css.original);

    Assert.That(styled.CssString, Is.EqualTo(MinifiedCssWithScopeFor("h1", styled.Id, css.minified)));
  }

  [TestCaseSource(nameof(CssCases))]
  public void CreateAStyledUl_WithMinifiedAndScopedCss((string original, string minified) css)
  {
    var styled = CreateStyled.Ul(css.original);

    Assert.That(styled.CssString, Is.EqualTo(MinifiedCssWithScopeFor("ul", styled.Id, css.minified)));
  }

  [TestCaseSource(nameof(CssCases))]
  public void CreateAStyledLi_WithMinifiedAndScopedCss((string original, string minified) css)
  {
    var styled = CreateStyled.Li(css.original);

    Assert.That(styled.CssString, Is.EqualTo(MinifiedCssWithScopeFor("li", styled.Id, css.minified)));
  }

  [TestCaseSource(nameof(CssCases))]
  public void ReturnTheExistingStyledComponentForAGivenFactory((string original, string minified) css)
  {
    var styled1 = CreateStyled.Div(css.original);
    var styled2 = CreateStyled.Div(css.original);

    Assert.That(styled2.Id, Is.EqualTo(styled1.Id));
  }

  [Test]
  public void ThrowJsonException_WhenCssIsMalformed()
  {
    const string css = @$"
      {MalformedSelectorWithColon} {{
        Property: Value
      }}";

    Assert.Throws<JsonException>(() => CreateStyled.Div(css));
  }

  private static string MinifiedCssWithScopeFor(string element, string scopeId, string cssMinified) =>
    cssMinified.Replace("ScopeId", scopeId).Replace("Element", element);
}