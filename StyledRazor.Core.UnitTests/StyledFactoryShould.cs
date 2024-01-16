namespace StyledRazor.Core.UnitTests;

[TestFixture]
public class StyledFactoryShould
{
  private StyledFactory _create = new(new TestComponent());

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
  }

  [SetUp]
  public void SetUp() => _create = new StyledFactory(new TestComponent());

  [TestCaseSource(nameof(CssCases))]
  public void CreateAStyledDiv_WithMinifiedAndScopedCss((string original, string minified) css)
  {
    var styled = _create.Div(css.original);

    Assert.That(styled.CssString, Is.EqualTo(MinifiedCssWithScopeFor("div", styled.Id, css.minified)));
  }

  [TestCaseSource(nameof(CssCases))]
  public void CreateAStyledHyperLink_WithMinifiedAndScopedCss((string original, string minified) css)
  {
    var styled = _create.A(css.original);

    Assert.That(styled.CssString, Is.EqualTo(MinifiedCssWithScopeFor("a", styled.Id, css.minified)));
  }

  [TestCaseSource(nameof(CssCases))]
  public void CreateAStyledH1_WithMinifiedAndScopedCss((string original, string minified) css)
  {
    var styled = _create.H1(css.original);

    Assert.That(styled.CssString, Is.EqualTo(MinifiedCssWithScopeFor("h1", styled.Id, css.minified)));
  }

  [TestCaseSource(nameof(CssCases))]
  public void CreateAStyledUl_WithMinifiedAndScopedCss((string original, string minified) css)
  {
    var styled = _create.Ul(css.original);

    Assert.That(styled.CssString, Is.EqualTo(MinifiedCssWithScopeFor("ul", styled.Id, css.minified)));
  }

  [TestCaseSource(nameof(CssCases))]
  public void CreateAStyledLi_WithMinifiedAndScopedCss((string original, string minified) css)
  {
    var styled = _create.Li(css.original);

    Assert.That(styled.CssString, Is.EqualTo(MinifiedCssWithScopeFor("li", styled.Id, css.minified)));
  }

  [TestCaseSource(nameof(CssCases))]
  public void NotCreateANewStyled_WhenAStyledAlreadyExists((string original, string minified) css)
  {
    var styled = _create.Div(css.original);
    var styledId = styled.Id;
    styled = _create.Div(css.original);

    Assert.That(styled.Id, Is.EqualTo(styledId));
  }

  private static string MinifiedCssWithScopeFor(string element, string scopeId, string cssMinified) =>
    cssMinified.Replace("ScopeId", scopeId).Replace("Element", element);
}