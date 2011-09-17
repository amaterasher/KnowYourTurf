using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Web.Grids;
using KnowYourTurf.Web.Models;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class PurchaseOrderLineItemListController : AdminControllerBase
    {
        private readonly IRepository _repository;
        private readonly IPurchaseOrderLineItemGrid _purchaseOrderLineItemGrid;

        public PurchaseOrderLineItemListController(IRepository repository, IPurchaseOrderLineItemGrid purchaseOrderLineItemGrid)
        {
            _repository = repository;
            _purchaseOrderLineItemGrid = purchaseOrderLineItemGrid;
        }

        public JsonResult PurchaseOrderLineItems(GridItemsRequestModel input)
        {
            var purchaseOrder = _repository.Find<PurchaseOrder>(input.EntityId);
            if (purchaseOrder == null) return null;
            IQueryable<PurchaseOrderLineItem> items = purchaseOrder.GetLineItems().AsQueryable();
            if (input.PageSortFilter.SortColumn.IsEmpty()) items = items.OrderBy(x => x.Product.Name);
            var model = _purchaseOrderLineItemGrid.GetGridItemsViewModel(input.PageSortFilter, items, "poliGrid");
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}