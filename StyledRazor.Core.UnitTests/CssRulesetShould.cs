using StyledRazor.Core.Css;

namespace StyledRazor.Core.UnitTests;

[TestFixture]
public class CssRulesetShould
{
  private const string Property = "Property";
  private const string InitialValue = "InitialValue";
  private const string NewValue = "NewValue";
  private const string Selector = "Selector";
  private const string Element = "Element";

  private static readonly CssStyleDeclaration InitialDeclaration = new()
  {
    [Property] = InitialValue,
  };
  
  private static readonly CssStyleDeclaration NewDeclaration = new()
  {
    [Property] = NewValue,
  };

  [Test]
  public void GetCssDeclarationDictionary_WhenSelectorExists()
  {
    var css = new CssRuleset
    {
      [Selector] = InitialDeclaration,
    };
    
    var declaration = css.Get(Selector);

    Assert.That(declaration, Is.EqualTo(InitialDeclaration));
  }

  [Test]
  public void GetAnEmptyCssDeclarationDictionary_WhenSelectorDoesNotExist()
  {
    var css = new CssRuleset();

    var declaration = css.Get(Selector);

    Assert.That(declaration, Is.EqualTo(new CssStyleDeclaration()));
  }

  [Test]
  public void SetCssDeclarationFromString_WhenSelectorDoesNotExist()
  {
    var css = new CssRuleset();

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
    var css = new CssRuleset
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
    var css = new CssRuleset();

    css.Set(Selector, string.Empty);

    Assert.That(css.ContainsKey(Selector), Is.EqualTo(false));
  }

  [Test]
  public void SetCssDeclarationFromCssDeclaration_WhenSelectorDoesNotExist()
  {
    var css = new CssRuleset();

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
    var css = new CssRuleset
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
    var css = new CssRuleset();

    css.Set(Selector, new CssStyleDeclaration());

    Assert.That(css.ContainsKey(Selector), Is.EqualTo(false));
  }

  [Test]
  public void NotDeserializeDeclaration_WhenCssRuleStringIsEmpty()
  {
    var cssRule = string.Empty;

    var declaration = CssRuleset.DeserializeCssDeclaration(cssRule);

    Assert.That(declaration, Is.Empty);
  }
  
  [Test]
  public void NotDeserializeCss_WhenCssStringIsEmpty()
  {
    var cssString = string.Empty;

    var cssRuleset = CssRuleset.Deserialize(cssString);

    Assert.That(cssRuleset, Is.Empty);
  }

  [Test]
  public void SerializeCssWithParentScope_WhenScopedIsTrue()
  {
    var css = new CssRuleset
    {
      [Element] = InitialDeclaration,
      [Selector] = InitialDeclaration,
    };
    const string expectedCss = "Element{Property:InitialValue;}Element Selector{Property:InitialValue;}"; 
    
    var serializedCss = css.Serialize(true);
    
    Assert.That(serializedCss, Is.EqualTo(expectedCss));
  }
}