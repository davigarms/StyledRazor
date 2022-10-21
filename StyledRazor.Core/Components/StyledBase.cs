using System;
using Microsoft.AspNetCore.Components.Rendering;

namespace StyledRazor.Core.Components;

public class StyledBase : ParametersBase
{
  private string _componentId;
  private string _baseElement;
  private string _baseCss;

  public string Name { get; set; }

  protected virtual Styled Styled { get; set; }

  protected virtual string Styles { get; set;  }

  protected StyledBase()
  {
    Name = GetType().Name;
    if (Styled != null)
      SetCss(Styled.BaseElement, Styled.BaseCss, Name);
  }

  private static string ComponentId(string prefix = null) =>
    $"{(prefix == null ? "w" : prefix.ToLower() + "_")}{Id}";

  private static string Id => Guid.NewGuid().ToString().Replace("-", "")[..10];

  private string BaseElementId => $"{_baseElement}[{_componentId}]";

  private static string BaseElement(string css) =>
    css[..css.IndexOf("{", StringComparison.Ordinal)]
      .Trim();

  private void SetCss(string baseElement, string baseCss, bool useNamePrefix = false)
  {
    _baseElement = baseElement;
    _componentId = useNamePrefix ? ComponentId(Name) : ComponentId();
    _baseCss = CompressedCss(baseCss);
  }

  protected void SetCss(string baseElement, string baseCss, string name)
  {
    Name = name;
  }

  private string CompressedCss(string css)
  {
    css = css
      .Insert(0, $"{BaseElementId}")
      .Insert(0, "\n")
      .Replace("  ", "")
      .Replace("\r", "\n")
      .Replace(" \n", "\n")
      .Replace("\t", "")
      .Replace(": ", ":")
      .Replace(" {", "{")
      .Replace(" }", "}")
      .Replace(" > ", ">")
      .Replace("}", $"}}{BaseElementId}")
      .Replace("\n", "");

    return css
      .Insert(css.Length, "\n")
      .Replace($"{BaseElementId}\n", "");
  }

  private void BuildComponentId(RenderTreeBuilder builder) => builder.AddAttribute(0, _componentId);

  private void BuildComponentStyle(RenderTreeBuilder builder) => builder.AddAttribute(0, "style", Styles);

  private void BuildComponentParams(RenderTreeBuilder builder)
  {
    if (Params == null) return;

    foreach (var (key, value) in Params) //.ToArray()[1..])
      builder.AddAttribute(0, key, value);
  }

  private void BuildComponentContent(RenderTreeBuilder builder)
  {
    if (ChildContent == null) return;

    builder.AddContent(0, ChildContent);
  }

  private void BuildComponentCss(RenderTreeBuilder builder)
  {
    if (_baseCss == null) return;

    builder.OpenElement(0, "style");
    builder.AddContent(0, _baseCss);
    builder.CloseElement();
  }

  protected override void BuildRenderTree(RenderTreeBuilder builder)
  {
    builder.OpenElement(0, _baseElement);
    BuildComponentId(builder);
    BuildComponentStyle(builder);
    BuildComponentParams(builder);
    BuildComponentContent(builder);
    BuildComponentCss(builder);
    builder.CloseElement();
  }
}