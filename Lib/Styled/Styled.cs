using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorApp.Lib.Styled
{
    public class Styled
    {
        private RenderFragment _child;
        private string _css;
        private readonly string _element;
        private IReadOnlyDictionary<string,object> _params;
        private string _style;
        
        public string WrapperName { get; private set; }

        public Styled(string element = "div")
        {
            _element = element;
            WrapperName = GenerateUniqueWrapper();
        }

        public Styled Prefix(string prefix)
        {
            WrapperName = GenerateUniqueWrapper(prefix);
            return this;
        }

        public Styled Content(RenderFragment value)
        {
            _child = value;
            return this;
        }

        public Styled Params(IReadOnlyDictionary<string,object> value)
        {
            _params = value;
            return this;
        }

        public Styled Style(string value)
        {
            if (value == "") return this;
            _style = value;
            return this;
        }

        public Styled Css(string value)
        {
            if (value == "") return this;
            _css = value;
            return this;
        }

        public RenderFragment Render() =>  builder =>
        {
            builder.OpenElement(0, _element);
            AddWrapper(builder);
            AddParams(builder);
            AddStyle(builder);
            AddContent(builder);
            AddCss(builder);
            builder.CloseElement();
        };
        
        public RenderFragment RenderCss(string value = null)
        {
            if (value != null) _css = value;
            return AddCss;
        }
        
        private string GenerateUniqueWrapper(string prefix = "wrapper")
        {
            return $"{prefix}_{Guid.NewGuid().ToString()[..8]}";
        }
        
        private void AddWrapper(RenderTreeBuilder builder)
        {
            builder.AddAttribute(0, "wrapper", WrapperName);
        }
        
        private void AddContent(RenderTreeBuilder builder)
        {
            if (_child != null) builder.AddContent(4, _child);
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

        private void AddStyle(RenderTreeBuilder builder)
        {
            if (_style != null) builder.AddAttribute(0, "style", _style);
        }

        private void AddCss(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "style");
            builder.AddContent(1, _css.Replace(_element, $"[wrapper=\"{WrapperName}\"]"));
            builder.CloseElement();
        }
    }
}