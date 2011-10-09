using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using HtmlTags;
using KnowYourTurf.Core.Domain;
using Rhino.Security.Interfaces;
using System.Linq;

namespace KnowYourTurf.Core.Html.Grid
{
    public class ImageButtonColumn<ENTITY> : ImageColumn<ENTITY> where ENTITY : IGridEnabledClass
    {
        private string _actionUrl;
        public string ActionUrl
        {
            get { return _actionUrl; }
            set { _actionUrl = value; }
        }
        private string _action;
        private string _jsonData;

        public ImageButtonColumn<ENTITY> ForAction<CONTROLLER>(Expression<Func<CONTROLLER, object>> expression) where CONTROLLER : Controller
        {
            var actionUrl = UrlContext.GetUrlForAction(expression);
            _actionUrl = actionUrl;
            return this;
        }

        public ImageButtonColumn<ENTITY> ToPerformAction(ColumnAction action)
        {
            _action = action.ToString();
            return this;
        }

        public override string BuildColumn(object item, User user, IAuthorizationService _authorizationService)
        {
            var _item = (ENTITY)item;
            var value = FormatValue(_item, user, _authorizationService);
            if (value.IsEmpty()) return null;
            var divTag = BuildDiv();
            divTag.AddClasses(new[] { "imageButtonColumn", _action });
            var anchor = buildAnchor(_item);
            var image = BuildImage();
             divTag.Children.Add(image);
            anchor.Children.Add(divTag);
            return anchor.ToString();
        }

        private HtmlTag buildAnchor(ENTITY item)
        {
            var anchor = new HtmlTag("a");
            string data = string.Empty;
            if(_jsonData.IsNotEmpty())
            {
                data = ","+_jsonData;
            }
            anchor.Attr("onclick",
                        "$.publish('/grid/" + _action + "',['" + _actionUrl + "/" + item.EntityId + "'"+data+"]);");
            return anchor;
        }

        public void AddDataToEvent(string jsonObject)
        {
            _jsonData = jsonObject;
        }
    }
}