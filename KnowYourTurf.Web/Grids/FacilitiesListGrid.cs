using System;
using System.Linq;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Grids
{
    public class FacilitiesListGrid : Grid<User>, IEntityListGrid<User>
    {
        public FacilitiesListGrid(IGridBuilder<User> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<User> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.FullName)
                .ForAction<FacilitiesController>(x => x.AddUpdate(null))
                .ToPerformAction(ColumnAction.AddUpdateItem).WithId("facilitieslist")
                .IsSortable(false)
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.PhoneMobile);
            GridBuilder.DisplayFor(x => x.Email);
            return this;
        }
    }

}