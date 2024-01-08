using StyledRazor.Core.Style;

namespace StyledRazor.Core.UnitTests;

[TestFixture]
public class CssDeclarationDictionaryShould
{
  private const string Selector = "> *";

  private readonly CssDeclarationDictionary _declaration = new()
  {
    { "margin", "10px" },
  };

  [Test]
  public void Set_Property_WhenAPropertyIsNotDefined()
  {
    var css = new CssRulesetDictionary();

    css.Get(Selector).Set("margin", "10px");

    Assert.Multiple(() =>
    {
      Assert.That(css.Keys, Does.Contain(Selector));
      Assert.That(css[Selector], Is.EquivalentTo(_declaration));
    });
  }
  
  [Test]
  public void Set_Property_WhenAPropertyIsAlreadyDefined()
  {
    const string initialDeclaration = "{margin: 0}";
    var css = new CssRulesetDictionary();
    css.Set(Selector, initialDeclaration);

    css.Get(Selector).Set("margin", "10px");

    Assert.Multiple(() =>
    {
      Assert.That(css.Keys, Does.Contain(Selector));
      Assert.That(css[Selector], Is.EquivalentTo(_declaration));
    });
  }
}