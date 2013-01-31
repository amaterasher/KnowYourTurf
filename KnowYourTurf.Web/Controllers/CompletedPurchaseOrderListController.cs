﻿using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

    public class CompletedPurchaseOrderListController : AdminControllerBase
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
       private readonly IEntityListGrid<PurchaseOrder> _purchaseOrderListGrid;

        public CompletedPurchaseOrderListController(IDynamicExpressionQuery dynamicExpressionQuery)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _purchaseOrderListGrid = ObjectFactory.Container.GetInstance< IEntityListGrid<PurchaseOrder>>("Completed");
        }

        public ActionResult ItemList(ListViewModel input)
        {
            var url = UrlContext.GetUrlForAction<CompletedPurchaseOrderListController>(x => x.PurchaseOrdersCompleted(null));
            ListViewModel model = new ListViewModel()
            {
                gridDef = _purchaseOrderListGrid.GetGridDefinition(url, input.User),
                _Title = WebLocalizationKeys.COMPLETED_PURCHASE_ORDERS.ToString()
            };
            return new CustomJsonResult(model);
        }

        public JsonResult PurchaseOrdersCompleted(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<PurchaseOrder>(input.filters, x => x.Completed);
            var gridItemsViewModel = _purchaseOrderListGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }
    }
}