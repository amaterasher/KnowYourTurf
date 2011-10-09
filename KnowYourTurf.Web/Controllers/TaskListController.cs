﻿using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Grids;

namespace KnowYourTurf.Web.Controllers
{
    public class TaskListController:KYTController
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly ITaskListGrid _taskListGrid;

        public TaskListController(IDynamicExpressionQuery dynamicExpressionQuery,
            ITaskListGrid taskListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _taskListGrid = taskListGrid;
        }

        public ActionResult TaskList()
        {
            var url = UrlContext.GetUrlForAction<TaskListController>(x => x.Tasks(null));
            ListViewModel model = new ListViewModel()
            {
                AddEditUrl = UrlContext.GetUrlForAction<TaskController>(x => x.AddEdit(null)),
                ListDefinition = _taskListGrid.GetGridDefinition(url, WebLocalizationKeys.TASKS)
            };
            return View(model);
        }

        public JsonResult Tasks(GridItemsRequestModel input)
        {
               var items = _dynamicExpressionQuery.PerformQuery<Task>(input.filters);
            var gridItemsViewModel = _taskListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}