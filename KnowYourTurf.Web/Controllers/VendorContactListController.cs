﻿using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Grids;
using KnowYourTurf.Web.Models;

namespace KnowYourTurf.Web.Controllers
{
    public class VendorContactListController:KYTController
    {
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IVendorContactListGrid _vendorContactListGrid;

        public VendorContactListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IVendorContactListGrid vendorContactListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _vendorContactListGrid = vendorContactListGrid;
        }

        public ActionResult VendorContactList(ListViewModel input)
        {
            var url = UrlContext.GetUrlForAction<VendorContactListController>(x => x.VendorContacts(null)) + "?ParentId=" + input.ParentId;
            ListViewModel model = new ListViewModel()
            {
                AddEditUrl = UrlContext.GetUrlForAction<VendorContactController>(x => x.AddEdit(null)) + "?ParentId=" + input.ParentId,
                ListDefinition = _vendorContactListGrid.GetGridDefinition(url, WebLocalizationKeys.VENDOR_CONTACTS)
            };
            return View(model);
        }

        public JsonResult VendorContacts(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<VendorContact>(input.filters, x => x.Vendor.EntityId == input.ParentId);
            var gridItemsViewModel = _vendorContactListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}