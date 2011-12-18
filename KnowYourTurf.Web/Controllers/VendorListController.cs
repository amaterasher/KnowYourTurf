﻿using System;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class VendorListController:KYTController
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
       private readonly IEntityListGrid<Vendor> _vendorListGrid;

        public VendorListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<Vendor> vendorListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _vendorListGrid = vendorListGrid;
        }

        public ActionResult VendorList(ListViewModel input)
        {
            var url = UrlContext.GetUrlForAction<VendorListController>(x => x.Vendors(null));
            ListViewModel model = new ListViewModel()
            {
                AddUpdateUrl = UrlContext.GetUrlForAction<VendorController>(x => x.AddUpdate(null)),
                GridDefinition = _vendorListGrid.GetGridDefinition(url)
            };
            return View(model);
        }
        
        public JsonResult Vendors(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Vendor>();
            Action<IGridColumn, Vendor> mod = (c, v) =>
                                          {
                                              if (c.GetType() == typeof(ImageButtonColumn<Vendor>) && c.ColumnIndex == 10)
                                              {
                                                  var col = (ImageButtonColumn<Vendor>)c;
                                                  col.AddDataToEvent("{ 'ParentId' : " + v.EntityId + "}");
                                              }
                                          };

            _vendorListGrid.AddColumnModifications(mod);
            var gridItemsViewModel = _vendorListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}