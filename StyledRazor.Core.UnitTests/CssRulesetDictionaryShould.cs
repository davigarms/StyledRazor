using StyledRazor.Core.Css;

namespace StyledRazor.Core.UnitTests;

[TestFixture]
public class CssRulesetDictionaryShould
{
  private const string Selector = "> *";
  
  private readonly CssDeclarationDictionary _declaration = new()
  {
    { "padding", "10px" },
  };

  [Test]
  public void Get_ReturnCssDeclarationDictionary_WhenGivenSelectorIsSet()
  {
    var css = new CssRulesetDictionary();
    css.Set(Selector, _declaration);
    
    var declaration = css.Get(Selector);
    
    Assert.That(declaration, Is.EquivalentTo(_declaration));
  }
  
  [Test]
  public void Get_ReturnEmptyCssDeclarationDictionary_WhenGivenSelectorIsNotSet()
  {
    var css = new CssRulesetDictionary();
    
    var declaration = css.Get(Selector);
    
    Assert.That(declaration, Is.EquivalentTo(new CssDeclarationDictionary()));
  }

  [Test]
  public void Set_CssDeclaration_FromString()
  {
    var css = new CssRulesetDictionary();

    css.Set(Selector,
    @"{
      padding: 10px;
    }");

    Assert.Multiple(() =>
    {
      Assert.That(css.ContainsKey(Selector));
      Assert.That(css.SingleOrDefault(x => x.Key == Selector).Value, Is.EquivalentTo(_declaration));
    });
  }
  
  [Test]
  public void Set_CssDeclaration_FromCssDeclaration()
  {
    var css = new CssRulesetDictionary();

    css.Set(Selector, _declaration);

    Assert.Multiple(() =>
    {
      Assert.That(css.ContainsKey(Selector));
      Assert.That(css.SingleOrDefault(x => x.Key == Selector).Value, Is.EquivalentTo(_declaration));
    });
  }
}