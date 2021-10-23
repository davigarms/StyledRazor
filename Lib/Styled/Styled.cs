using System;
using Microsoft.AspNetCore.Components;

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
                builder.OpenElement(5, _element);
                builder.AddAttribute(1, "class", ClassName);
                builder.AddAttribute(2, "style", _style);
                if (_params != null)
                    foreach (var (key, value) in _params)
                        builder.AddAttribute(3, key, value);
                builder.AddContent(4, _child);
                builder.OpenElement(5, "style");
                builder.AddContent(6, _css.Replace(_element, $".{ClassName}"));
                builder.CloseElement();
                builder.CloseElement();
            };
        }

        private string GenerateUniqueClassName(string prefix = "wrapper")
        {
            ClassName = $"{prefix}_{Guid.NewGuid().ToString()[..8]}";
            return ClassName;
        }
    }
}