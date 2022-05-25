using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace StyledRazor.Lib
{
    public class Styled
    {
        private readonly string _componentId;
        private IReadOnlyDictionary<string, object> _params;
        private RenderFragment _childContent;
        private string _css;
        private string _baseElement = "div";
        public string Type { get; }


        public Styled(string css = null, string type = null)
        {
            Type = type;
            _componentId = GenerateId(type);
            SetCss(css);
        }

        public Styled(string css, Type type)
            : this(css, type.Name) => SetCss(css);

        public Styled(string css, ComponentBase component) :
            this(css, component.GetType().Name) => SetCss(css);

        private static string GenerateId(string prefix = null) =>
            $"{(prefix == null ? "w" : prefix.ToLower() + "-w")}{Guid.NewGuid().ToString()[..8]}";

        private static string BaseElement(string css) => css.Substring(0, css.IndexOf("{")).Trim();

        private void SetCss(string css)
        {
            if (css == null) return;
            _baseElement = BaseElement(css);
            _css = css
                .Insert(0, "\n")
                .Replace("}", "}\n")
                .Replace("  ", "")
                .Replace("\r", "\n")
                .Replace("\n ", "\n")
                .Replace("\t", "")
                .Replace("\n" + _baseElement, $"{_baseElement}[{_componentId}]")
                .Replace("\n", "");
        }

        public Styled Params(IReadOnlyDictionary<string, object> @params)
        {
            _params = @params;
            return this;
        }

        public Styled Content(RenderFragment childContent)
        {
            _childContent = childContent;
            return this;
        }

        public RenderFragment Render() => component =>
        {
            component.OpenElement(0, _baseElement);
            BuildComponentId(component);
            BuildComponentParams(component);
            BuildComponentContent(component);
            BuildComponentCss(component);
            component.CloseElement();
        };

        private void BuildComponentId(RenderTreeBuilder builder) => builder.AddAttribute(0, _componentId);

        private void BuildComponentParams(RenderTreeBuilder builder)
        {
            if (_params == null) return;
            foreach (var (key, value) in _params)
                builder.AddAttribute(0, key, value);
        }

        private void BuildComponentContent(RenderTreeBuilder builder)
        {
            if (_childContent == null) return;
            builder.AddContent(0, _childContent);
        }

        private void BuildComponentCss(RenderTreeBuilder builder)
        {
            if (_css == null) return;
            builder.OpenElement(0, "style");
            builder.AddContent(0, _css);
            builder.CloseElement();
        }
    }
}