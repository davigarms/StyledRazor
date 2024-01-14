using StyledRazor.Core.Css;

namespace StyledRazor.Core.UnitTests;

[TestFixture]
public class CssDeclarationDictionaryShould
{
  private const string Selector = "> *";
  private readonly CssDeclarationDictionary _expectedDeclaration = new()
  {
    { "margin", "10px" },
  };
  
  private CssRulesetDictionary _css;

  [SetUp]
  public void SetUp()
  {
    _css = new CssRulesetDictionary();
  }

  [TestCase("0")]
  [TestCase("10px")]
  public void SetProperty_WhenPropertyIsAlreadyDefined(string marginValue)
  {
    _css[Selector] = new CssDeclarationDictionary
    {
      { "margin", marginValue },
    };

    _css.Get(Selector).Set("margin", "10px");

    Assert.Multiple(() =>
    {
      Assert.That(_css.Keys, Does.Contain(Selector));
      Assert.That(_css[Selector], Is.EquivalentTo(_expectedDeclaration));
    });
  }

  [Test]
  public void SetNewProperty_WhenPropertyIsNotDefined()
  {
    _css.Get(Selector).Set("margin", "10px");

    Assert.Multiple(() =>
    {
      Assert.That(_css.Keys, Does.Contain(Selector));
      Assert.That(_css[Selector], Is.EquivalentTo(_expectedDeclaration));
    });
  }
}