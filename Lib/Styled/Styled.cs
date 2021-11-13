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
        
        private string WrapperName { get; set; }

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

        public Styled Params(IReadOnlyDictionary<string,object> value)
        {
            _params = value;
            return this;
        }
        
        public Styled Content(RenderFragment value)
        {
            _child = value;
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
            AddWrapperName(builder);
            AddParams(builder);
            AddContent(builder);
            AddCss(builder);
            builder.CloseElement();
        };
        
        private string GenerateUniqueWrapper(string prefix = "wrapper")
        {
            return $"{prefix}_{Guid.NewGuid().ToString()[..8]}";
        }
        
        private void AddWrapperName(RenderTreeBuilder builder)
        {
            builder.AddAttribute(0, WrapperName);
        }

        private void AddParams(RenderTreeBuilder builder)
        {
            if (_params == null) return;
            foreach (var (key, value) in _params)
                builder.AddAttribute(0, key, value);
        }

        private void AddContent(RenderTreeBuilder builder)
        {
            if (_child != null) builder.AddContent(0, _child);
        }

        private void AddCss(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "style");
            builder.AddContent(0, _css.Replace(":root", $"{_element}[{WrapperName}]"));
            builder.CloseElement();
        }
    }
}