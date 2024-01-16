using StyledRazor.Core.Css;

namespace StyledRazor.Core.UnitTests;

[TestFixture]
public class CssRulesetDictionaryShould
{
  private const string Property = "Property";
  private const string InitialValue = "InitialValue";
  private const string NewValue = "NewValue";
  private const string Selector = "Selector";
  private const string Element = "Element";

  private static readonly CssDeclarationDictionary InitialDeclaration = new()
  {
    [Property] = InitialValue,
  };
  
  private static readonly CssDeclarationDictionary NewDeclaration = new()
  {
    [Property] = NewValue,
  };

  [Test]
  public void GetCssDeclarationDictionary_WhenSelectorExists()
  {
    var css = new CssRulesetDictionary
    {
      [Selector] = InitialDeclaration,
    };
    
    var declaration = css.Get(Selector);

    Assert.That(declaration, Is.EqualTo(InitialDeclaration));
  }

  [Test]
  public void GetAnEmptyCssDeclarationDictionary_WhenSelectorDoesNotExist()
  {
    var css = new CssRulesetDictionary();

    var declaration = css.Get(Selector);

    Assert.That(declaration, Is.EqualTo(new CssDeclarationDictionary()));
  }

  [Test]
  public void SetCssDeclarationFromString_WhenSelectorDoesNotExist()
  {
    var css = new CssRulesetDictionary();

    css.Set(Selector,
    @"{
      Property: NewValue;
    }");

    Assert.Multiple(() =>
    {
      Assert.That(css.ContainsKey(Selector));
      Assert.That(css.SingleOrDefault(x => x.Key == Selector).Value, Is.EqualTo(NewDeclaration));
    });
  }

  [Test]
  public void SetCssDeclarationFromString_WhenSelectorExists()
  {
    var css = new CssRulesetDictionary
    {
      [Selector] = InitialDeclaration,
    };

    css.Set(Selector,
    @"{
      Property: NewValue;
    }");

    Assert.Multiple(() =>
    {
      Assert.That(css.ContainsKey(Selector));
      Assert.That(css.SingleOrDefault(x => x.Key == Selector).Value, Is.EqualTo(NewDeclaration));
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

    css.Set(Selector, NewDeclaration);

    Assert.Multiple(() =>
    {
      Assert.That(css.ContainsKey(Selector));
      Assert.That(css.SingleOrDefault(x => x.Key == Selector).Value, Is.EqualTo(NewDeclaration));
    });
  }

  [Test]
  public void SetCssDeclarationFromCssDeclaration_WhenSelectorExists()
  {
    var css = new CssRulesetDictionary
    {
      [Selector] = InitialDeclaration,
    };

    css.Set(Selector, NewDeclaration);

    Assert.Multiple(() =>
    {
      Assert.That(css.ContainsKey(Selector));
      Assert.That(css.SingleOrDefault(x => x.Key == Selector).Value, Is.EqualTo(NewDeclaration));
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
      [Element] = InitialDeclaration,
      [Selector] = InitialDeclaration,
    };
    const string expectedCss = "Element{Property:InitialValue;}Element Selector{Property:InitialValue;}"; 
    
    var serializedCss = css.Serialize();
    
    Assert.That(serializedCss, Is.EqualTo(expectedCss));
  }
}