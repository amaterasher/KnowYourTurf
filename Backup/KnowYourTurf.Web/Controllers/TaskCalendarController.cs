﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.DomainTools;
using CC.Core.Html;
using CC.Core.Services;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using CC.Core;

namespace KnowYourTurf.Web.Controllers
{
    public class TaskCalendarController:KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;

        public TaskCalendarController(IRepository repository,ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult TaskCalendar(ViewModel input)
        {
            var model = new CalendarViewModel
                       {
                           CalendarDefinition = new CalendarDefinition
                                                   {
                                                       Url = UrlContext.GetUrlForAction<TaskCalendarController>(x => x.Events(null))+"?RootId="+input.RootId,
                                                       AddUpdateTemplateUrl = UrlContext.GetUrlForAction<TaskController>(x => x.AddUpdate_Template(null)),
                                                       AddUpdateUrl = UrlContext.GetUrlForAction<TaskController>(x => x.AddUpdate(null)),
                                                       AddUpdateRoute = "task",
                                                       DisplayTemplateUrl = UrlContext.GetUrlForAction<TaskController>(x => x.Display_Template(null)),
                                                       DisplayUrl = UrlContext.GetUrlForAction<TaskController>(x => x.Display(null)),
                                                       DisplayRoute = "taskdisplay",
                                                       DeleteUrl = UrlContext.GetUrlForAction<TaskController>(x => x.Delete(null)),
                                                       EventChangedUrl = UrlContext.GetUrlForAction<TaskCalendarController>(x => x.EventChanged(null)),
                                                       PopupTitle = WebLocalizationKeys.TASK_INFORMATION.ToString()

                                                   }
                       };
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public JsonResult EventChanged(TaskChangedViewModel input)
        {
            var task = _repository.Find<Task>(input.EntityId);
            task.ScheduledDate = input.ScheduledDate;
            task.EndTime = input.EndTime;
            task.StartTime = input.StartTime;
            var crudManager = _saveEntityService.ProcessSave(task);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Events(GetEventsViewModel input)
        {
            var events = new List<CalendarEvent>();
            var startDateTime = DateTimeUtilities.ConvertFromUnixTimestamp(input.start);
            var endDateTime = DateTimeUtilities.ConvertFromUnixTimestamp(input.end);
            var category = _repository.Find<Site>(input.RootId);
            var tasks = category.GetAllTasks().Where(x => x.ScheduledDate >= startDateTime && x.ScheduledDate <= endDateTime);
            tasks.ForEachItem(x =>
                       events.Add(new CalendarEvent
                                      {
                                          EntityId = x.EntityId,
                                          title = x.Field.Abbreviation + ": " + x.TaskType.Name,
                                          start = x.StartTime.ToString(),
                                          end = x.EndTime.ToString(),
                                          color = x.Field.FieldColor
                                      })
                );
            return Json(events, JsonRequestBehavior.AllowGet);
        }
    }

    public class TaskChangedViewModel:ViewModel
    {
        public DateTime? ScheduledDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}