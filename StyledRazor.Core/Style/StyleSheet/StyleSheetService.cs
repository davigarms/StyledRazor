using Microsoft.AspNetCore.Components;
using StyledRazor.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static StyledRazor.Core.Style.Css.CssHelper;

namespace StyledRazor.Core.Style.StyleSheet;

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

  public static RenderFragment CreateStyleSheet(string baseCss = "") => builder =>
                                                                        {
                                                                          builder.OpenElement(0, "style");
                                                                          builder.AddContent(1, baseCss.Minify());
                                                                          builder.AddContent(1, Css);
                                                                          builder.CloseElement();
                                                                        };

  public static void Clear() => StyledList.Clear();
}