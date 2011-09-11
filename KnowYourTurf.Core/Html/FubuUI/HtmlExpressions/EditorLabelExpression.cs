using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Castle.Components.Validator;
using FubuMVC.Core.Util;
using FubuMVC.UI.Tags;
using HtmlTags;

namespace KnowYourTurf.Core.Html.FubuUI.HtmlExpressions
{
    public class EditorLabelExpression<VIEWMODEL> where VIEWMODEL : class
    {
        private readonly ITagGenerator<VIEWMODEL> _generator;
        private readonly Expression<Func<VIEWMODEL, object>> _expression;
        private HtmlTag _htmlRoot;
        private bool _noColon;
        private bool _LeadingColon;
        private string _labelRootClass;
        private string _labelClass;
        private bool _hide;
        private string _elementId;
        private string _customDisplay;

        public EditorLabelExpression(ITagGenerator<VIEWMODEL> generator, Expression<Func<VIEWMODEL, object>> expression)
        {
            _generator = generator;
            _expression = expression;
        }

        public HtmlTag ToHtmlTag()
        {
            _htmlRoot = new HtmlTag("div").AddClass("KYT_editor_label");
            if (_labelRootClass.IsNotEmpty()) _htmlRoot.AddClass(_labelRootClass);
            if (_hide) _htmlRoot.Hide();
            HtmlTag label = _generator.LabelFor(_expression);
            if (_labelClass.IsNotEmpty()) label.AddClass(_labelClass);
            if (_elementId.IsNotEmpty()) label.Id(_elementId);
            if (_customDisplay.IsNotEmpty()) label.Text(_customDisplay);
            
            if(!_noColon && !_LeadingColon)
            {
                label.Text(label.Text() + ":");
            }
            if (!_noColon && _LeadingColon)
            {
                label.Text(":" + label.Text());
            }
            if (ReflectionHelper.GetProperty(_expression).HasAttribute<ValidateNonEmptyAttribute>())
            {
                label.Text(label.Text()+"*");
            }
            _htmlRoot.Child(label);
            return _htmlRoot;
        }

        public EditorLabelExpression<VIEWMODEL> NoColon()
        {
            _noColon = true;
            return this;
        }

        public EditorLabelExpression<VIEWMODEL>LeadingColon()
        {
            _LeadingColon = true;
            return this;
        }

        public EditorLabelExpression<VIEWMODEL> AddClassToLabelRoot(string cssClass)
        {
            _labelRootClass = cssClass;
            return this;
        }

        public EditorLabelExpression<VIEWMODEL> AddClassToLabel(string cssClass)
        {
            _labelClass = cssClass;
            return this;
        }

        public EditorLabelExpression<VIEWMODEL> Hide()
        {
            _hide = true;
            return this;
        }

        public EditorLabelExpression<VIEWMODEL> ElementId(string id)
        {
            _elementId = id;
            return this;
        }

        public EditorLabelExpression<VIEWMODEL> CustomLabel(string display)
        {
            _customDisplay = display;
            return this;
        }
    }
}