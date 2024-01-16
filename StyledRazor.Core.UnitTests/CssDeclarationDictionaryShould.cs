using StyledRazor.Core.Css;

namespace StyledRazor.Core.UnitTests;

[TestFixture]
public class CssDeclarationDictionaryShould
{
  private const string Property = "Property";
  private const string InitialValue = "InitialValue";
  private const string NewValue = "NewValue";
  private static readonly string Selector = TestContext.CurrentContext.Random.GetString(4);

  private static readonly CssDeclarationDictionary InitialDeclaration = new()
  {
    [Property] = InitialValue,
  };
  
  private static readonly CssDeclarationDictionary NewDeclaration = new()
  {
    [Property] = NewValue,
  };
  
  private static CssRulesetDictionary _css = new();

  [SetUp]
  public void SetUp() => _css = new CssRulesetDictionary();
  
  [Test]
  public void SetNewProperty_WhenPropertyIsNotDefined()
  {
    _css.Get(Selector).Set(Property, NewValue);

    Assert.Multiple(() =>
    {
      Assert.That(_css.Keys, Does.Contain(Selector));
      Assert.That(_css[Selector], Is.EqualTo(NewDeclaration));
    });
  }

  [Test]
  public void SetProperty_WhenPropertyIsAlreadyDefined()
  {
    _css[Selector] = InitialDeclaration;

    _css.Get(Selector).Set(Property, NewValue);

    Assert.Multiple(() =>
    {
      Assert.That(_css.Keys, Does.Contain(Selector));
      Assert.That(_css[Selector], Is.EqualTo(NewDeclaration));
    });
  }

  [Test]
  public void NotSetProperty_WhenValueIsAlreadySetForProperty()
  {
    _css[Selector] = InitialDeclaration;
      
    _css.Get(Selector).Set(Property, InitialValue);

    Assert.Multiple(() =>
    {
      Assert.That(_css.Keys, Does.Contain(Selector));
      Assert.That(_css[Selector], Is.EqualTo(InitialDeclaration));
    });
  }
}