using StyledRazor.Core.Css;

namespace StyledRazor.Core.UnitTests;

[TestFixture]
public class CssRulesetDictionaryShould
{
  private static readonly string Selector = TestContext.CurrentContext.Random.GetString(4);

  private static readonly CssDeclarationDictionary Declaration = new()
  {
    { "padding", "10px" },
  };

  [Test]
  public void GetCssDeclarationDictionary_WhenSelectorExists()
  {
    var css = new CssRulesetDictionary();
    css.Set(Selector, Declaration);

    var declaration = css.Get(Selector);

    Assert.That(declaration, Is.EquivalentTo(Declaration));
  }

  [Test]
  public void GetAnEmptyCssDeclarationDictionary_WhenSelectorDoesNotExist()
  {
    var css = new CssRulesetDictionary();

    var declaration = css.Get(Selector);

    Assert.That(declaration, Is.EquivalentTo(new CssDeclarationDictionary()));
  }

  [Test]
  public void SetCssDeclarationFromString_WhenSelectorDoesNotExist()
  {
    var css = new CssRulesetDictionary();

    css.Set(Selector,
    @"{
      padding: 10px;
    }");

    Assert.Multiple(() =>
    {
      Assert.That(css.ContainsKey(Selector));
      Assert.That(css.SingleOrDefault(x => x.Key == Selector).Value, Is.EquivalentTo(Declaration));
    });
  }

  [Test]
  public void SetCssDeclarationFromString_WhenSelectorExists()
  {
    var css = new CssRulesetDictionary
    {
      [Selector] = new()
      {
        { "padding", "0" },
      },
    };

    css.Set(Selector,
    @"{
      padding: 10px;
    }");

    Assert.Multiple(() =>
    {
      Assert.That(css.ContainsKey(Selector));
      Assert.That(css.SingleOrDefault(x => x.Key == Selector).Value, Is.EquivalentTo(Declaration));
    });
  }

  [Test]
  public void NotSetCssDeclarationFromString_WhenStringIsEmpty()
  {
    var css = new CssRulesetDictionary();

    css.Set(Selector, string.Empty);

    Assert.That(css.ContainsKey(Selector), Is.EqualTo(false));
  }

  [Test]
  public void SetCssDeclarationFromCssDeclaration_WhenSelectorDoesNotExist()
  {
    var css = new CssRulesetDictionary();

    css.Set(Selector, Declaration);

    Assert.Multiple(() =>
    {
      Assert.That(css.ContainsKey(Selector));
      Assert.That(css.SingleOrDefault(x => x.Key == Selector).Value, Is.EquivalentTo(Declaration));
    });
  }

  [Test]
  public void SetCssDeclarationFromCssDeclaration_WhenSelectorExists()
  {
    var css = new CssRulesetDictionary
    {
      [Selector] = new()
      {
        { "padding", "0" },
      },
    };

    css.Set(Selector, Declaration);

    Assert.Multiple(() =>
    {
      Assert.That(css.ContainsKey(Selector));
      Assert.That(css.SingleOrDefault(x => x.Key == Selector).Value, Is.EquivalentTo(Declaration));
    });
  }

  [Test]
  public void NotSetCssDeclarationFromCssDeclaration_WhenDeclarationCountIsZero()
  {
    var css = new CssRulesetDictionary();

    css.Set(Selector, new CssDeclarationDictionary());

    Assert.That(css.ContainsKey(Selector), Is.EqualTo(false));
  }

  [Test]
  public void NotDeserializeDeclaration_WhenCssRuleStringIsEmpty()
  {
    var cssRule = string.Empty;

    var declaration = CssRulesetDictionary.DeserializeCssDeclaration(cssRule);

    Assert.That(declaration, Is.Empty);
  }
  
  [Test]
  public void NotDeserializeCss_WhenCssStringIsEmpty()
  {
    var cssString = string.Empty;

    var cssRuleset = CssRulesetDictionary.Deserialize(cssString);

    Assert.That(cssRuleset, Is.Empty);
  }

  [Test]
  public void SerializeCssWithScope_WhenScopedIsTrue()
  {
    var css = new CssRulesetDictionary
    {
      ["div"] = new()
      {
        { "padding", "10px" },
      },
      ["> *"] = new()
      {
        { "padding", "10px" },
      },
    };
    const string expectedCss = "div{padding:10px;}div > *{padding:10px;}"; 
    
    var serializedCss = css.Serialize();
    
    Assert.That(serializedCss, Is.EqualTo(expectedCss));
  }
}