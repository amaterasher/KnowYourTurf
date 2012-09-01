using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;

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
                .ToPerformAction(ColumnAction.AddUpdateItem)
                .IsSortable(false)
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.DisplayFor(x => x.PhoneMobile);
            GridBuilder.DisplayFor(x => x.Email);
            return this;
        }
    }

}