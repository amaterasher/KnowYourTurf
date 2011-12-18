using System;
using System.Collections.Generic;
using System.Linq;
using HtmlTags;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public class VendorContactListGrid : Grid<VendorContact>, IEntityListGrid<VendorContact>
    {

     public VendorContactListGrid(IGridBuilder<VendorContact> gridBuilder,
         ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

     protected override Grid<VendorContact> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
               .ForAction<VendorContactController>(x => x.Delete(null))
               .ToPerformAction(ColumnAction.Delete)
               .ImageName("delete.png")
               .ToolTip(WebLocalizationKeys.DELETE_ITEM);
            GridBuilder.ImageButtonColumn()
                .ForAction<VendorContactController>(x => x.AddEdit(null))
                .ToPerformAction(ColumnAction.Edit)
                .ImageName("KYTedit.png")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x => x.FullName)
                .ForAction<VendorContactController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .IsSortable(false)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.Email).FormatValue(GridColumnFormatter.EMail);
            GridBuilder.DisplayFor(x => x.Phone);
            GridBuilder.DisplayFor(x => x.Address1).DisplayHeader(WebLocalizationKeys.ADDRESS);
            GridBuilder.DisplayFor(x => x.City);
            GridBuilder.DisplayFor(x => x.State);
            GridBuilder.DisplayFor(x => x.Fax);
            return this;
        }
    }
}
