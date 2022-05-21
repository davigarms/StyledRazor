using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace StyledRazor.Styled
{
    public class Styled
    {
        private readonly string _componentId;
        private IReadOnlyDictionary<string,object> _params;
        private RenderFragment _childContent;
        private string _css;
        private string _baseElement = "div";

        public Styled() =>
            _componentId = GenerateId();

        public Styled(Type type) =>
            _componentId = GenerateId(type.Name);

        public Styled(ComponentBase componentBase = null) =>
            _componentId = GenerateId(componentBase.GetType().Name);

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
            _css = css;
            _baseElement = css.Substring(0, css.IndexOf("{")).Trim();
            return this;
        }

        public RenderFragment Render() => Component =>
        {
            Component.OpenElement(0, _baseElement);
            ComponentId(Component);
            ComponentParams(Component);
            ComponentContent(Component);
            ComponentCss(Component);
            Component.CloseElement();
        };

        private string GenerateId(string prefix = "wrapper") =>
            $"{prefix.ToLower()}_{Guid.NewGuid().ToString()[..8]}";

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