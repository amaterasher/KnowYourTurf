﻿using System;
using System.Collections;
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
    public class TaskListGrid : Grid<Task>, IEntityListGrid<Task>
    {

        public TaskListGrid(IGridBuilder<Task> gridBuilder,
            ISessionContext sessionContext,
            IRepository repository)
            : base(gridBuilder, sessionContext, repository)
        {
        }

        protected override Grid<Task> BuildGrid()
        {
            GridBuilder.ImageButtonColumn()
                .ForAction<TaskController>(x => x.Delete(null))
                .ToPerformAction(ColumnAction.Delete)
                .ImageName("delete.png")
                .ToolTip(WebLocalizationKeys.DELETE_ITEM);
            GridBuilder.ImageButtonColumn()
                .ForAction<TaskController>(x=>x.AddEdit(null))
                .ToPerformAction(ColumnAction.Edit)
                .ImageName("KYTedit.png")
                .ToolTip(WebLocalizationKeys.EDIT_ITEM);
            GridBuilder.LinkColumnFor(x=>x.TaskType.Name)
                .ForAction<TaskController>(x=>x.Display(null))
                .ToPerformAction(ColumnAction.Display)
                .ToolTip(WebLocalizationKeys.DISPLAY_ITEM);
            GridBuilder.DisplayFor(x=>x.ScheduledDate);
            GridBuilder.DisplayFor(x=>x.ScheduledStartTime);
            GridBuilder.DisplayFor(x => x.Complete);
            return this;
        }
    }
}