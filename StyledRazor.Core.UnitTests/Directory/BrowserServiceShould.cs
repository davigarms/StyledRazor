using FakeItEasy;
using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Browser;

namespace StyledRazor.Core.UnitTests.Directory;

[TestFixture]
public class BrowserServiceShould
{
  private readonly int _expectedWidth = TestContext.CurrentContext.Random.Next();
  private readonly int _expectedHeight = TestContext.CurrentContext.Random.Next();

  [Test]
  public async Task ReturnWindowDimension()
  {
    var fakeBrowserRuntime = A.Fake<IBrowserConnector>();
    var browserService = new BrowserService(fakeBrowserRuntime);
    var windowDimension = new Dimension
    {
      Width = _expectedWidth,
      Height = _expectedHeight,
    };
    A.CallTo(() => fakeBrowserRuntime.InvokeAsync<Dimension>("WindowDimension"))
      .Returns(windowDimension);

    var result = await browserService.WindowDimension();
    
    Assert.Multiple(() =>
    {
      Assert.That(result.Width, Is.EqualTo(_expectedWidth));
      Assert.That(result.Height, Is.EqualTo(_expectedHeight));
    });
  }

  [Test]
  public async Task ReturnDimensionFromAnElement()
  {
    var fakeBrowserConnector = A.Fake<IBrowserConnector>();
    var browserService = new BrowserService(fakeBrowserConnector);
    var element = new ElementReference();
    var elementDimension = new Dimension
    {
      Width = _expectedWidth,
      Height = _expectedHeight,
    };
    A.CallTo(() => fakeBrowserConnector.InvokeAsync<Dimension>("DimensionFrom", element))
      .Returns(elementDimension);

    var result = await browserService.DimensionFrom(element);

    Assert.Multiple(() =>
    {
      Assert.That(result.Width, Is.EqualTo(_expectedWidth));
      Assert.That(result.Height, Is.EqualTo(_expectedHeight));
    });
  }
}