using StyledRazor.Core.Style;

namespace StyledRazor.Core.UnitTests;

public class CssShould
{
  private const string Selector = "> *";
  private readonly CssDefinition _cssDefinition = new()
  {
    { "padding", "10px" },
  };

  [Test]
  public void Get_CssDefinitionForASelector_WhenASelectorIsSet()
  {
    var css = new Css();
    css.Set(Selector, _cssDefinition);
    
    var cssDefinition = css.Get(Selector);
    
    Assert.That(cssDefinition, Is.EquivalentTo(_cssDefinition));
  }
  
  [Test]
  public void Get_AnEmptyCssDefinitionForASelector_WhenASelectorIsNotSet()
  {
    var css = new Css();
    
    var cssDefinition = css.Get(Selector);
    
    Assert.That(cssDefinition, Is.EquivalentTo(new CssDefinition()));
  }

  [Test]
  public void Set_CssDefinitionForASelector_FromString()
  {
    var css = new Css();

    css.Set(Selector,
    @"{
      padding: 10px;
    }");

    Assert.Multiple(() =>
    {
      Assert.That(css.ContainsKey(Selector));
      Assert.That(css.SingleOrDefault(x => x.Key == Selector).Value, Is.EquivalentTo(_cssDefinition));
    });
  }
  
  [Test]
  public void Set_CssDefinitionForASelector_FromCssDefinition()
  {
    var css = new Css();

    css.Set(Selector, _cssDefinition);

    Assert.Multiple(() =>
    {
      Assert.That(css.ContainsKey(Selector));
      Assert.That(css.SingleOrDefault(x => x.Key == Selector).Value, Is.EquivalentTo(_cssDefinition));
    });
  }
}