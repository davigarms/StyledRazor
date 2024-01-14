namespace StyledRazor.Core.UnitTests;

[TestFixture]
public class StyledFactoryShould
{
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

  [TestCaseSource(nameof(CssCases))]
  public void CreateAStyledDiv_WithMinifiedAndScopedCss((string original, string minified) css)
  {
    var create = new StyledFactory(new TestComponent());

    var styled = create.Div(css.original);

    css.minified = css.minified.Replace("@scope", styled.Id).Replace("@element", "div");
    Assert.That(styled.CssString, Is.EqualTo(css.minified));
  }
}