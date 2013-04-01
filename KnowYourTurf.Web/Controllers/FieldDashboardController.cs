﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using StructureMap;
using CC.Core;

namespace KnowYourTurf.Web.Controllers
{
    using KnowYourTurf.Web.Config;

    public class FieldDashboardController : KYTController
    {
        private readonly IRepository _repository;
        private readonly IDynamicExpressionQuery _dynamicExpressionQuery;
        private readonly IEntityListGrid<Task> _pendingTaskGrid;
        private readonly IEntityListGrid<Task> _completedTaskGrid;
        private readonly IEntityListGrid<Photo> _photoListGrid;
        private readonly IEntityListGrid<Document> _documentListGrid;

        public FieldDashboardController(IRepository repository,
                                        IDynamicExpressionQuery dynamicExpressionQuery,
                                        IEntityListGrid<Photo> photoListGrid,
                                        IEntityListGrid<Document> documentListGrid)
        {
            _repository = repository;
            _dynamicExpressionQuery = dynamicExpressionQuery;
            _pendingTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<Task>>("PendingTasks");
            _completedTaskGrid = ObjectFactory.Container.GetInstance<IEntityListGrid<Task>>("CompletedTasks");
            _photoListGrid = photoListGrid;
            _documentListGrid = documentListGrid;
        }

        public ActionResult ViewField_Template(ViewModel input)
        {
            return View("FieldDashboard", new FieldViewModel());
        }

        public ActionResult ViewField(ViewModel input)
        {
            var field = _repository.Find<Field>(input.EntityId);
            var url = UrlContext.GetUrlForAction<FieldDashboardController>(x => x.PendingTasksGrid(null)) + "?ParentId=" + input.EntityId;
            var completeUrl = UrlContext.GetUrlForAction<FieldDashboardController>(x => x.CompletedTasksGrid(null)) +"?ParentId=" + input.EntityId;
            var photoUrl = UrlContext.GetUrlForAction<FieldDashboardController>(x => x.PhotoGrid(null)) + "?ParentId=" + input.EntityId;
            var docuemntUrl = UrlContext.GetUrlForAction<FieldDashboardController>(x => x.DocumentGrid(null)) + "?ParentId=" + input.EntityId;
            var model = Mapper.Map<Field, FieldViewModel>(field);
            model._pendingGridUrl = url;
            model._completedGridUrl = completeUrl;
            model._documentGridUrl = docuemntUrl;
            model._photoGridUrl = photoUrl;
            model._saveUrl = UrlContext.GetUrlForAction<FieldController>(x => x.Save(null));
            model._Title = WebLocalizationKeys.FIELD_INFORMATION.ToString();
            model._Photos = field.Photos.Select(x => new PhotoDto {FileUrl = x.FileUrl});

            return new CustomJsonResult(model);
        }

        public ActionResult CompletedTasksGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<FieldDashboardController>(x => x.CompletedTasks(null)) + "?ParentId=" + input.ParentId;
            ListViewModel model = new ListViewModel()
            {
                gridDef = _completedTaskGrid.GetGridDefinition(url, input.User),
                ParentId = input.ParentId
            };
            return new CustomJsonResult(model);
        }
        public JsonResult CompletedTasks(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Task>(input.filters, x => x.Field.EntityId == input.ParentId && x.Complete);
            var gridItemsViewModel = _completedTaskGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }

        public ActionResult PendingTasksGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<FieldDashboardController>(x => x.PendingTasks(null)) + "?ParentId=" + input.ParentId;
            ListViewModel model = new ListViewModel()
            {
                gridDef = _pendingTaskGrid.GetGridDefinition(url, input.User),
                ParentId = input.ParentId
            };
            return new CustomJsonResult(model);
        }
        public JsonResult PendingTasks(GridItemsRequestModel input)
        {
            var items = _dynamicExpressionQuery.PerformQuery<Task>(input.filters,
                                                                   x =>
                                                                   x.Field.EntityId == input.ParentId && !x.Complete);
            var gridItemsViewModel = _pendingTaskGrid.GetGridItemsViewModel(input.PageSortFilter, items, input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }

        public ActionResult PhotoGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<FieldDashboardController>(x => x.Photos(null)) + "?ParentId=" + input.ParentId;
            var model = new ListViewModel()
            {
                gridDef = _photoListGrid.GetGridDefinition(url, input.User),
                ParentId = input.ParentId,
                deleteMultipleUrl = UrlContext.GetUrlForAction<PhotoController>(x => x.DeleteMultiple(null))
            };
            model.headerButtons.Add("new");
            model.headerButtons.Add("delete");
            return new CustomJsonResult(model);
        }
        public JsonResult Photos(GridItemsRequestModel input)
        {
            var field = _repository.Find<Field>(input.ParentId);
            Expression<Func<Photo, bool>> photoWhereClause =
                _dynamicExpressionQuery.PrepareExpression<Photo>(input.filters);
            IEnumerable<Photo> items;
            if(photoWhereClause==null)
           {
               items = field.Photos;
           }
           else
           {
               items = field.Photos.Where(photoWhereClause.Compile());
           }
            var gridItemsViewModel = _photoListGrid.GetGridItemsViewModel(input.PageSortFilter, items.AsQueryable(), input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }

        public ActionResult DocumentGrid(ViewModel input)
        {
            var url = UrlContext.GetUrlForAction<FieldDashboardController>(x => x.Documents(null)) + "?ParentId=" + input.ParentId;
            var model = new ListViewModel()
            {
                gridDef = _documentListGrid.GetGridDefinition(url, input.User),
                ParentId = input.ParentId,
                deleteMultipleUrl = UrlContext.GetUrlForAction<DocumentController>(x => x.DeleteMultiple(null))
            };
            model.headerButtons.Add("new");
            model.headerButtons.Add("delete");
            return new CustomJsonResult(model);
        }
        public JsonResult Documents(GridItemsRequestModel input)
        {
            var field = _repository.Find<Field>(input.ParentId);
            var documentWhereClause = _dynamicExpressionQuery.PrepareExpression<Document>(input.filters);
            IEnumerable<Document> items;
            if (documentWhereClause == null)
            {
                items = field.Documents;
            }else
            {
                items = field.Documents.Where(documentWhereClause.Compile());
            }
            var gridItemsViewModel = _documentListGrid.GetGridItemsViewModel(input.PageSortFilter, items.AsQueryable(), input.User);
            return new CustomJsonResult(gridItemsViewModel);
        }
    }

    
}