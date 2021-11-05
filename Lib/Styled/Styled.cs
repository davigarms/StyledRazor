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
            Wrapper = GenerateUniqueWrapper();
        }

        public string Wrapper { get; private set; }

        public Styled As(string prefix)
        {
            Wrapper = GenerateUniqueWrapper(prefix);
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

        public RenderFragment Render() =>  builder =>
        {
            builder.OpenElement(0, _element);
            AddWrapper(builder);
            AddStyle(builder);
            AddParams(builder);
            AddChild(builder);
            AddCss(builder);
            builder.CloseElement();
        };
        
        public RenderFragment Css(string value = null)
        {
            if (value != null) _css = value;
            return AddCss;
        }
        
        private void AddCss(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "style");
            builder.AddContent(1, _css.Replace(_element, $"[as=\"{Wrapper}\"]"));
            builder.CloseElement();
        }

        private void AddWrapper(RenderTreeBuilder builder)
        {
            builder.AddAttribute(0, "as", Wrapper);
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

        private string GenerateUniqueWrapper(string prefix = "wrapper")
        {
            return $"{prefix}_{Guid.NewGuid().ToString()[..8]}";
        }
    }
}