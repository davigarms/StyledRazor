using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorApp.Lib.Styled
{
    public class Styled : ComponentBase
    {
        private readonly string _element;
        private RenderFragment _child;
        private string _css;
        private Params _params;
        private string _style;

        public Styled(string element = "div")
        {
            _element = element;
            GenerateUniqueClassName();
        }

        private string ClassName { get; set; }

        public Styled As(string prefix)
        {
            GenerateUniqueClassName(prefix);
            return this;
        }

        public Styled WithContent(RenderFragment value)
        {
            _child = value;
            return this;
        }

        public Styled WithParams(Params value)
        {
            _params = value;
            return this;
        }

        public Styled WithStyle(string value)
        {
            _style = value;
            return this;
        }

        public Styled WithCss(string value)
        {
            _css = value;
            return this;
        }

        public RenderFragment Render()
        {
            return builder =>
            {
                builder.OpenElement(0, _element);
                AddClass(builder);
                AddStyle(builder);
                AddParams(builder);
                AddChild(builder);
                AddCss(builder);
                builder.CloseElement();
            };
        }
        
        private void AddCss(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "style");
            builder.AddContent(1, _css.Replace(_element, $".{ClassName}"));
            builder.CloseElement();
        }

        private void AddClass(RenderTreeBuilder builder)
        {
            builder.AddAttribute(0, "class", ClassName);
        }

        private void AddStyle(RenderTreeBuilder builder)
        {
            if (_style != null) builder.AddAttribute(0, "style", _style);
        }

        private void AddParams(RenderTreeBuilder builder)
        {
            if (_params == null) return;
            var i = 0;
            foreach (var (key, value) in _params)
            {
                builder.AddAttribute(i, key, value);
                i++;
            }
        }
        
        private void AddChild(RenderTreeBuilder builder)
        {
            if (_child != null) builder.AddContent(4, _child);
        }

        private string GenerateUniqueClassName(string prefix = "wrapper")
        {
            ClassName = $"{prefix}_{Guid.NewGuid().ToString()[..8]}";
            return ClassName;
        }
    }
}