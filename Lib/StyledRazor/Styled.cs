using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorApp.Lib.StyledRazor
{
    public class Styled
    {
        private string _componentId;
        private IReadOnlyDictionary<string,object> _params;
        private RenderFragment _childContent;
        private string _css;
        private readonly string _baseElement;

        public Styled(string baseElement = "div")
        {
            _baseElement = baseElement;
            _componentId = new ComponentId("wrapper").Value;
        }

        public Styled Component(Object type)
        {
            _componentId = new ComponentId(type.GetType().Name).Value;
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
            if (value == "") return this;
            _css = value;
            return this;
        }

        public RenderFragment Render() =>  builder =>
        {
            builder.OpenElement(0, _baseElement);
            AddComponentId(builder);
            AddParams(builder);
            AddContent(builder);
            AddCss(builder);
            builder.CloseElement();
        };
        
        private void AddComponentId(RenderTreeBuilder builder)
        {
            builder.AddAttribute(0, _componentId);
        }

        private void AddParams(RenderTreeBuilder builder)
        {
            if (_params == null) return;
            foreach (var (key, value) in _params)
                builder.AddAttribute(0, key, value);
        }

        private void AddContent(RenderTreeBuilder builder)
        {
            if (_childContent != null) builder.AddContent(0, _childContent);
        }

        private void AddCss(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "style");
            builder.AddContent(0, _css.Replace(":root", $"{_baseElement}[{_componentId}]"));
            builder.CloseElement();
        }
    }
}