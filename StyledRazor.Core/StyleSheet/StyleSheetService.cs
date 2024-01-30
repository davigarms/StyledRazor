using static StyledRazor.Core.Style.Css.CssHelper;
using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Component;
using StyledRazor.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StyledRazor.Core.StyleSheet;

public static class StyleSheetService
{
  private static readonly List<Styled> StyledList = new();

  private static string Css => string.Join(string.Empty, StyledList.Select(styled => styled.CssString));

  public static Func<Task> OnUpdate { get; set; }

  public static void Add(Styled styled)
  {
    if (styled == null) return;

    StyledList.Add(styled);
    OnUpdate?.Invoke();
  }

  public static void Update(string id, Styled styled)
  {
    var toUpdate = StyledList.FirstOrDefault(s => s.Id == id);

    if (toUpdate == null || toUpdate.Id == styled.Id) return;

    toUpdate.Update(styled);
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