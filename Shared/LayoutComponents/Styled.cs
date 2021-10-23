using System;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Shared.LayoutComponents
{
    public class Styled
    {
        public string ClassName { get; private set; }

        public Styled(string prefix = "wrapper")
        {
            GenerateUniqueClassName(prefix);
        }
        
        private string GenerateUniqueClassName(string prefix)
        {
            ClassName = $"{prefix}_{Guid.NewGuid().ToString()[..8]}";
            return ClassName;
        }

        public RenderFragment Render(string style) => builder =>
        {
            builder.OpenElement(0, "style");
            builder.AddContent(1, style.Replace(".wrapper", $".{ClassName}"));
            builder.CloseElement();
        };
    }
}