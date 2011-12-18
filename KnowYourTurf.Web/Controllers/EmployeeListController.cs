﻿using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Controllers
{
    public class EmployeeListController : AdminControllerBase
    {
       private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
       private readonly IEntityListGrid<User> _employeeListGrid;

        public EmployeeListController(IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<User> employeeListGrid)
        {
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _employeeListGrid = employeeListGrid;
        }

        public ActionResult EmployeeList()
        {
            var url = UrlContext.GetUrlForAction<EmployeeListController>(x => x.Employees(null));
            var model = new ListViewModel()
            {
                AddUpdateUrl = UrlContext.GetUrlForAction<EmployeeController>(x => x.AddEdit(null)),
                GridDefinition = _employeeListGrid.GetGridDefinition(url)
            };
            return View(model);
        }

        public JsonResult Employees(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<User>(input.filters, x=>x.UserLoginInfo.UserType==UserType.Employee.ToString());
            var gridItemsViewModel = _employeeListGrid.GetGridItemsViewModel(input.PageSortFilter, items);
            return Json(gridItemsViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}