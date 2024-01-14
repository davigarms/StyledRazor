using StyledRazor.Core.Css;

namespace StyledRazor.Core.UnitTests;

public class StyledShould
{
  private StyledFactory _create;

  private class TestComponent : StyledBase {}

  [SetUp]
  public void SetUp() => _create = new StyledFactory(new TestComponent());


  [Test]
  public void UpdateStyled()
  {
    var createBase = new StyledFactory(new TestComponent());
    var styledDiv = _create.Div("{margin: 10px}");
    var styledBase = createBase.Div(string.Empty);
    var minifiedCss = "div[component]{margin:10px;}";

    styledBase.Update(styledDiv);
    Assert.Multiple(() =>
    {
      Assert.That(styledBase.Id, Is.EqualTo(styledDiv.Id));
      Assert.That(styledBase.CssString, Is.EqualTo(minifiedCss.Replace("component", styledDiv.Id)));
    });
  }

  [Test]
  public void GetCssDeclarationDictionaryFromASelector()
  {
    var styled = _create.Div("{margin: 10px}");
    var expectedDictionary = new CssDeclarationDictionary
    {
      { "margin", "10px" },
    };
    
    Assert.That(styled.Get($"div[{styled.Id}]"), Is.EquivalentTo(expectedDictionary));
  }
}