using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static StyledRazor.Core.Utils.Css;

namespace StyledRazor.Core.StyleSheet;

public sealed class StyleSheetService
{
  private static StyleSheetService _instance;
  
  private static readonly object Lock = new();
  
  private readonly List<Styled> _styledList;

  private string Css => string.Join("", _styledList.Select(styled => styled.Css));

  public Func<Task> OnUpdate { get; set; }
  

  public StyleSheetService()
  {
    _styledList = new List<Styled>();
  }

  public void Add(Styled styled)
  {
    if (styled == null) return;
    
    _styledList.Add(styled);
    OnUpdate?.Invoke();
  }

  public void Update(string id, Styled styled)
  {
    var toUpdate = _styledList.FirstOrDefault(s => s.Id == id);

    if (toUpdate == null || toUpdate.Id == styled.Id) return;
    
    toUpdate.UpdateStyle(styled);
    OnUpdate?.Invoke();
  }

  public RenderFragment CreateStyleSheet() => CreateStyleSheet("");

  public RenderFragment CreateStyleSheet(string baseCss) => builder =>
  {
    builder.OpenElement(0, "style");
    builder.AddContent(1, Minify(baseCss));
    builder.AddContent(1, Css);
    builder.CloseElement();
  };

  public void Clear() => _styledList.Clear();

  public static StyleSheetService GetInstance()
  {
    if (_instance == null)
    {
      lock (Lock)
      {
        if (_instance == null)
        {
          _instance = new StyleSheetService();
        }
      }
    }
    return _instance;
  }
}