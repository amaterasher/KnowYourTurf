﻿using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class FieldListController : KYTController
    {
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<Field> _fieldListGrid;
        private readonly IRepository _repository;

        public FieldListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<Field> fieldListGrid,
            IRepository repository)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _fieldListGrid = fieldListGrid;
            _repository = repository;
        }

        public ActionResult ItemList(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<FieldListController>(x => x.Fields(null)) + "?RootId=" + input.RootId;
            ListViewModel model = new ListViewModel()
            {
                gridDef = _fieldListGrid.GetGridDefinition(url),
                _Title = WebLocalizationKeys.FIELDS.ToString()
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Fields(GridItemsRequestModel input)
        {
            var category = _repository.Find<Category>(input.RootId);
            var items = _dynamicExpressionQuery.PerformQuery(category.Fields, input.filters);
            var gridItemsViewModel = _fieldListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}