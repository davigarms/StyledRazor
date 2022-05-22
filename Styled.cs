using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Lib
{
    public class Styled 
    {
        private readonly string _componentId;
        private IReadOnlyDictionary<string,object> _params;
        private RenderFragment _childContent;
        private string _css;
        private string _baseElement = "div";

        public Styled() => _componentId = GenerateId();
        
        public Styled(string css) : this () => SetCss(css);

        public Styled(Type type) => _componentId = GenerateId(type.Name);
        
        public Styled(string css, Type type) : this (type) => SetCss(css);

        public Styled(ComponentBase componentBase) => _componentId = GenerateId(componentBase.GetType().Name);
        
        public Styled(string css, ComponentBase componentBase) : this (componentBase) => SetCss(css);

        private string GenerateId(string prefix = null) =>
            $"{(prefix == null ? "w" : prefix.ToLower() + "-w")}{Guid.NewGuid().ToString()[..8]}";
        
        private static string BaseElement(string css) => css.Substring(0, css.IndexOf("{")).Trim();
        
        private void SetCss(string css)
        {
            _css = css;
            _baseElement = BaseElement(css);
        }

        public Styled Params(IReadOnlyDictionary<string,object> @params)
        {
            _params = @params;
            return this;
        }
        
        public Styled Content(RenderFragment childContent)
        {
            _childContent = childContent;
            return this;
        }

        public Styled Css(string css)
        {
            SetCss(css);
            return this;
        }
       
        public RenderFragment Render() => component =>
        {
            component.OpenElement(0, _baseElement);
            ComponentId(component);
            ComponentParams(component);
            ComponentContent(component);
            ComponentCss(component);
            component.CloseElement();
        };

        private void ComponentId(RenderTreeBuilder builder) => builder.AddAttribute(0, _componentId);

        private void ComponentParams(RenderTreeBuilder builder)
        {
            if (_params == null) return;
            foreach (var (key, value) in _params)
                builder.AddAttribute(0, key, value);
        }

        private void ComponentContent(RenderTreeBuilder builder)
        {
            if (_childContent == null) return; 
            builder.AddContent(0, _childContent);
        }

        private void ComponentCss(RenderTreeBuilder builder)
        {
            if (_css == null) return;
            builder.OpenElement(0, "style");
            builder.AddContent(0, _css.Replace(_baseElement, $"{_baseElement}[{_componentId}]"));
            builder.CloseElement();
        }
    }
}