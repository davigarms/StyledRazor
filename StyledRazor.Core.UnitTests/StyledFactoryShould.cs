namespace StyledRazor.Core.UnitTests;

[TestFixture]
public class StyledFactoryShould
{
  private StyledFactory _create;

  private class TestComponent : StyledBase {}

  private static IEnumerable<(string, string)> CssCases()
  {
    yield return (@"{
                    margin: 10px;
                  }", "@element[@scope]{margin:10px;}");

    yield return (@"{
                    margin : 10px;
                  }", "@element[@scope]{margin:10px;}");

    yield return (@"{
                    margin: 10px ;
                  }", "@element[@scope]{margin:10px;}");
    
    yield return (@"{
                    margin: 10px;
                    padding: 10px;
                  }", "@element[@scope]{margin:10px;padding:10px;}");

    yield return (@"{
                    margin: 10px;     padding: 10px;
                  }", "@element[@scope]{margin:10px;padding:10px;}");

    yield return (@"{margin: 10px; padding: 10px;}", "@element[@scope]{margin:10px;padding:10px;}");

    yield return (@"{
                      margin: 10px;
                      padding: 10px;
                    }
                    > * {
                      margin: 10px;
                    }
                  ", "@element[@scope]{margin:10px;padding:10px;}@element[@scope]> *{margin:10px;}");

    yield return (@"{
                      margin: 10px;
                      padding: 10px;
                    }

                    > * {
                      margin: 10px;
                    }", "@element[@scope]{margin:10px;padding:10px;}@element[@scope]> *{margin:10px;}");

    yield return (@"{
                      margin: 10px;
                      padding: 10px;
                    }


                    > * {
                      margin: 10px;
                    }

                    ", "@element[@scope]{margin:10px;padding:10px;}@element[@scope]> *{margin:10px;}");
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
  public void CreateAStyledUL_WithMinifiedAndScopedCss((string original, string minified) css)
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

  private static string MinifiedCssWithScopeFor(string element, string scope, string cssMinified) =>
    cssMinified.Replace("@scope", scope).Replace("@element", element);
}