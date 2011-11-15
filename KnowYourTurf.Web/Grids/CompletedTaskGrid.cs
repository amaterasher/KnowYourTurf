﻿using System;
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
    public interface ICompletedTaskGrid
    {
        void AddColumnModifications(Action<IGridColumn, Task> modifications);
        GridDefinition GetGridDefinition(string url, StringToken title = null);
        GridItemsViewModel GetGridItemsViewModel(PageSortFilter pageSortFilter, IQueryable<Task> items, string gridName = "");
    }

    public class CompletedTaskGrid : Grid<Task>, ICompletedTaskGrid
    {

        public CompletedTaskGrid(IGridBuilder<Task> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, repository, sessionContext)
        {
        }

        protected override Grid<Task> BuildGrid()
        {
            GridBuilder.LinkColumnFor(x => x.TaskType.Name)
                .ForAction<TaskController>(x => x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x => x.ScheduledDate);
            GridBuilder.DisplayFor(x => x.ScheduledStartTime);
            return this;
        }
    }
}