﻿namespace KnowYourTurf.Web.Areas.Reports.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using CC.Core.CoreViewModelAndDTOs;
    using CC.Core.DomainTools;
    using CC.Core.Services;

    using Castle.Components.Validator;

    using KnowYourTurf.Core.Domain;
    using KnowYourTurf.Web.Controllers;
    using KnowYourTurf.Web.Config;

    public class TasksByFieldController : AdminControllerBase
    {
        private readonly IRepository _repository;
        private readonly ISelectListItemService _selectListItemService;

        public TasksByFieldController(IRepository repository, ISelectListItemService selectListItemService)
        {
            this._repository = repository;
            this._selectListItemService = selectListItemService;
        }
        public ActionResult Display_Template(ViewModel input)
        {
            return this.View("Display", new TasksByFieldViewModel());
        }

        public CustomJsonResult Display(ViewModel input)
        {
            
            var model = new TasksByFieldViewModel
            {
//                StartDate = DateTime.Now,
//                EndDate = DateTime.Now,
                _FieldEntityIdList = this._selectListItemService.CreateList<Field>( x => x.Name, x => x.EntityId, true),
//                _Title = WebLocalizationKeys.TRAINER_METRIC.ToString(),
                ReportUrl = "/Areas/Reports/ReportViewer/TasksByField.aspx"
            };
            return new CustomJsonResult(model);
        }
    }

    public class TasksByFieldViewModel : ViewModel
    {
        public IEnumerable<SelectListItem> _FieldEntityIdList { get; set; }
        [ValidateNonEmpty]
        public int FieldEntityId { get; set; }
        public string ReportUrl { get; set; }
    }
}
