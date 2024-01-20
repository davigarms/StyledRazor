using static StyledRazor.Core.Css.CssHelper;

namespace StyledRazor.Core.UnitTests;

public class CssHelperShould
{
  [TestCase("1px", 1)]
  [TestCase("1rem", 16)]
  [TestCase("1em", 16)]
  [TestCase("10px",10)]
  [TestCase("2em",32)]
  [TestCase("2.5rem",40)]
  public void ConvertCssUnitToUnit(string value, int expected)
  {
    var result = value.ToInt();
    
    Assert.That(result, Is.EqualTo(expected));
  }

  [TestCase("100%")]
  [TestCase("10cm")]
  [TestCase("")]
  public void ThrowsInvalidOperationException_WhenValueIsInvalid(string invalidValue)
  {
      var message = Assert.Throws<InvalidOperationException>(() => invalidValue.ToInt())?.Message;
      Assert.That(message, Is.EqualTo("Css value must be in one of the following units:\n- rem\n- em\n- px"));
  }
}