using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static StyledRazor.Core.Style.Css.CssHelper;

namespace StyledRazor.Core.Style.StyleSheet;

public static class StyleSheetService
{
  private static readonly List<ComponentStyle.ComponentStyle> StyledList = new();

  private static string Css => string.Join(string.Empty, StyledList.Select(styled => styled.CssString));

  public static Func<Task> OnUpdate { get; set; }

  public static void Add(ComponentStyle.ComponentStyle componentStyle)
  {
    if (componentStyle == null) return;

    StyledList.Add(componentStyle);
    OnUpdate?.Invoke();
  }

  public static void Update(string id, ComponentStyle.ComponentStyle componentStyle)
  {
    var toUpdate = StyledList.FirstOrDefault(s => s.Id == id);

    if (toUpdate == null || toUpdate.Id == componentStyle.Id) return;

    toUpdate.Update(componentStyle);
    OnUpdate?.Invoke();
  }

  public static RenderFragment CreateStyleSheet(string baseCss = "") => builder =>
  {
    builder.OpenElement(0, "style");
    builder.AddContent(1, baseCss.Minify());
    builder.AddContent(1, Css);
    builder.CloseElement();
  };

  public static void Clear() => StyledList.Clear();
}