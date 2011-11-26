﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Html.Grid;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Grids;
using KnowYourTurf.Web.Models;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class AdminDashboardController:KYTController
    {
        private readonly IRepository _repository;
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<User> _grid;

        public AdminDashboardController(IRepository repository, IDynamicExpressionQuery dynamicExpressionQuery,
            IEntityListGrid<User> grid  )
        {
            _repository = repository;
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _grid = grid;
        }

        public ActionResult ViewAdmin(ViewModel input)
        {
            var admin = _repository.Find<User>(input.EntityId);
            var model = new UserViewModel
            {
                User = admin,
                AddEditUrl = UrlContext.GetUrlForAction<TaskController>(x => x.AddEdit(null)) + "?ParentId=" + input.EntityId+"&From=Admin",
               
            };
            return View("AdminDashboard", model);
        }
    }

    public class UserViewModel:ViewModel
    {
        public User User { get; set; }

        public bool DeleteImage { get; set; }

        public IEnumerable<TokenInputDto> AvailableItems { get; set; }

        public IEnumerable<TokenInputDto> SelectedItems{ get; set; }
    }
}