using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace StyledRazor.Shared.Styled
{
    public class Styled
    {
        private string _componentId;
        private IReadOnlyDictionary<string,object> _params;
        private RenderFragment _childContent;
        private string _css;
        private readonly string _baseElement;

        public Styled(string baseElement)
        {
            _baseElement = baseElement;
            _componentId = new ComponentId().Value;
        }

        public Styled Component(object target)
        {
            _componentId = new ComponentId(target.GetType().Name).Value;
            return this;
        }

        public Styled Params(IReadOnlyDictionary<string,object> value)
        {
            _params = value;
            return this;
        }
        
        public Styled Content(RenderFragment value)
        {
            _childContent = value;
            return this;
        }

        public Styled Css(string value)
        {
            _css = value;
            return this;
        }

        public RenderFragment Render() =>  builder =>
        {
            builder.OpenElement(0, _baseElement);
            BuildComponentId(builder);
            BuildParams(builder);
            BuildContent(builder);
            BuildCss(builder);
            builder.CloseElement();
        };
        
        private void BuildComponentId(RenderTreeBuilder builder)
        {
            if (_componentId == null) return;
            builder.AddAttribute(0, _componentId);
        }

        private void BuildParams(RenderTreeBuilder builder)
        {
            if (_params == null) return;
            foreach (var (key, value) in _params)
                builder.AddAttribute(0, key, value);
        }

        private void BuildContent(RenderTreeBuilder builder)
        {
            if (_childContent == null) return; 
            builder.AddContent(0, _childContent);
        }

        private void BuildCss(RenderTreeBuilder builder)
        {
            if (_css == null) return;
            builder.OpenElement(0, "style");
            builder.AddContent(0, _css.Replace(":root", $"{_baseElement}[{_componentId}]"));
            builder.CloseElement();
        }
    }
}