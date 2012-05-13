using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using KnowYourTurf.Security.Interfaces;
using FubuMVC.Core.Util;
using HtmlTags;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Core.Html.Grid
{
    public class LinkColumn<ENTITY> : ColumnBase<ENTITY> where ENTITY : IGridEnabledClass
    {
        protected List<string> _divCssClasses;
        private string _actionUrl;
        public string ActionUrl
        {
            get { return _actionUrl; }
            set { _actionUrl = value; }
        }

        private string _action;
        private string _gridName;

        public LinkColumn(Expression<Func<ENTITY, object>> expression,string gridName = "")
        {
            _gridName = gridName;
            _divCssClasses = new List<string>();
            propertyAccessor = ReflectionHelper.GetAccessor(expression); 
            var name = LocalizationManager.GetLocalString(expression);
            if (propertyAccessor is PropertyChain)
            {
                name = ((PropertyChain)(propertyAccessor)).Names.Aggregate((current, next) => current + "." + next);
            }
            Properties[GridColumnProperties.name.ToString()] = name;
            
            var headerText = LocalizationManager.GetHeader(expression).HeaderText;
            if(headerText == "Name")
            {
                headerText = typeof (ENTITY).Name.ToSeperateWordsFromPascalCase() + " " + headerText;
            }
            Properties[GridColumnProperties.header.ToString()] = headerText;
        }
        //used for getting controller from a field value like "InstantiatingType"
        public LinkColumn<ENTITY> ForAction(Expression<Func<ENTITY, object>> expression, string actionName)
        {
            var controllerName = ReflectionHelper.GetAccessor(expression).FieldName + "Controller";
            var urlForAction = UrlContext.GetUrlForAction(controllerName, actionName);
            _actionUrl = urlForAction;
            return this;
        }

        public LinkColumn<ENTITY> ForAction<CONTROLLER>(Expression<Func<CONTROLLER, object>> expression) where CONTROLLER : Controller
        {
            var urlForAction = UrlContext.GetUrlForAction(expression);
            _actionUrl = urlForAction;
            return this;
        }

        public LinkColumn<ENTITY> ForAction(string controllerName, string actionName)
        {
            var urlForAction = UrlContext.GetUrlForAction(controllerName, actionName);
            _actionUrl = urlForAction;
            return this;
        }

        public LinkColumn<ENTITY> ToPerformAction(ColumnAction action)
        {
            _action = action.ToString();
            return this;
        }

        public LinkColumn<ENTITY> FormatValue(GridColumnFormatter formatter)
        {
            Properties[GridColumnProperties.formatter.ToString()] = formatter.ToString().ToLowerInvariant();
            return this;
        }

        public LinkColumn<ENTITY> FormatOptions(GridColumnFormatterOptions option)
        {
            Properties[GridColumnProperties.formatoptions.ToString()] = option.ToString().ToLowerInvariant();
            return this;
        }

        public override string BuildColumn(object item, User user, IAuthorizationService _authorizationService, string gridName = "")
        {
            // if a name is given in the controller it overrides the name given in the grid declaration
            if (gridName.IsNotEmpty()) _gridName = gridName;
            var _item = (ENTITY)item;
            var value = FormatValue(_item, user, _authorizationService);
            if (value.IsEmpty()) return null;
            var span = new HtmlTag("span").Text(value);
            addToolTipAndClasses(span);
            var anchor = buildAnchor(_item);
            anchor.AddClasses(new[] { "linkColumn", _action });

            var div = BuildDiv();
            div.Children.Add(span);
            anchor.Children.Add(div);
            return anchor.ToString();
        }


        protected DivTag BuildDiv()
        {
            var divTag = new DivTag("imageDiv");
            divTag.Attr("title", _toolTip);
            divTag.AddClasses(_divCssClasses);
            return divTag;
        }

        private void addToolTipAndClasses(HtmlTag span)
        {
            span.Attr("title", _toolTip);
            span.AddClasses(_divCssClasses);
        }

        private HtmlTag buildAnchor(ENTITY item)
        {
            var anchor = new HtmlTag("a");
            anchor.Attr("onclick", "KYT.vent.trigger('" + _action + "'," + item.EntityId + ")");

            return anchor;
        }
     
        public ColumnBase<ENTITY> AddClassToSpan(string cssClass)
        {
            _divCssClasses.Add(cssClass);
            return this;
        }


    }
}