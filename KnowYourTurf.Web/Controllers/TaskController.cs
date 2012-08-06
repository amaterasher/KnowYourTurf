﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FluentNHibernate.Utils;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using NHibernate.Linq;

namespace KnowYourTurf.Web.Controllers
{
    public class TaskController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly ISelectListItemService _selectListItemService;
        private readonly IInventoryService _inventoryService;
        private readonly IUpdateCollectionService _updateCollectionService;

        public TaskController(IRepository repository,
            ISaveEntityService saveEntityService,
            ISelectListItemService selectListItemService,
            IInventoryService inventoryService,
            IUpdateCollectionService updateCollectionService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _selectListItemService = selectListItemService;
            _inventoryService = inventoryService;
            _updateCollectionService = updateCollectionService;
        }
        public ActionResult AddUpdate_Template(AddUpdateTaskViewModel input)
        {
            return View("TaskAddUpdate", new TaskViewModel());
        }   

        public ActionResult AddUpdate(TaskViewModel input)
        {
            var task = input.EntityId > 0 ? _repository.Find<Task>(input.EntityId) : new Task();
            task.ScheduledDate = input.ScheduledDate.HasValue ? input.ScheduledDate.Value : task.ScheduledDate;
            task.ScheduledStartTime= input.ScheduledStartTime.HasValue ? input.ScheduledStartTime.Value: task.ScheduledStartTime;
            var taskTypes = _selectListItemService.CreateList<TaskType>(x => x.Name, x => x.EntityId, true);
            var fields = _selectListItemService.CreateFieldsSelectListItems(input.RootId,input.ParentId);
            var products = createProductSelectListItems();
            var availableEmployees = _repository.Query<User>(x => x.UserLoginInfo.Status == Status.Active.ToString() && x.UserRoles.Any(y=>y.Name==UserType.Employee.ToString()))
                .Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.FirstName + " " + x.LastName }).OrderBy(x=>x.name).ToList();
            var selectedEmployees = task.Employees.Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.FullName }).OrderBy(x => x.name);
            var availableEquipment = _repository.FindAll<Equipment>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name }).OrderBy(x => x.name);
            var selectedEquipment = task.Equipment.Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name }).OrderBy(x => x.name);
            var model = Mapper.Map<Task, TaskViewModel>(task);
            model.Employees = new TokenInputViewModel { _availableItems = availableEmployees, selectedItems = selectedEmployees };
            model.Equipment = new TokenInputViewModel { _availableItems = availableEquipment, selectedItems = selectedEquipment};
            model._FieldEntityIdList = fields;
            model._InventoryProductProductEntityIdList = products;
            model._TaskTypeEntityIdList = taskTypes;
            model._Title = WebLocalizationKeys.TASK_INFORMATION.ToString();
            model.Popup = input.Popup;
            model.RootId = input.RootId;

            model._saveUrl = UrlContext.GetUrlForAction<TaskController>(x => x.Save(null));

            if (input.Copy)
            {
                // entityid is changed on view cuz the entity is in the NH graph here on server
                model.Complete = false;
            }
                
            return Json(model,JsonRequestBehavior.AllowGet);
        }



        private GroupedSelectViewModel createProductSelectListItems()
        {
            IEnumerable<InventoryProduct> inventory = _repository.FindAll<InventoryProduct>();
            var chemicals =
                _selectListItemService.CreateListWithConcatinatedText(
                    inventory.Where(i => i.Product.InstantiatingType == "Chemical"),
                    x => x.Product.Name,
                    x => x.UnitType,
                    "-->",
                    y => y.EntityId,
                    false);
            var fertilizer =
                _selectListItemService.CreateListWithConcatinatedText(
                    inventory.Where(i => i.Product.InstantiatingType == "Fertilizer"),
                    x => x.Product.Name,
                    x => x.UnitType,
                    "-->",
                    x => x.EntityId,
                    false);
            var materials =
                _selectListItemService.CreateListWithConcatinatedText(
                    inventory.Where(i => i.Product.InstantiatingType == "Material"),
                    x => x.Product.Name,
                    x => x.UnitType,
                    "-->",
                    x => x.EntityId,
                    false);
            var groups = new GroupedSelectViewModel();
            groups.groups.Add(new SelectGroup {label = "Chemicals", children = chemicals});
            groups.groups.Add(new SelectGroup {label = "Ferilizers", children = fertilizer});
            groups.groups.Add(new SelectGroup {label = "Materials", children = materials});

            return groups;
        }

        public ActionResult Display(ViewModel input)
        {
//            var task = _repository.Find<Task>(input.EntityId);
////            var productName = task.GetProductName();
//            var model = new TaskViewModel
//                            {
//                                Popup = input.Popup,
//                                Item = task,
////                                Product = productName,
//                                _EmployeeNames = task.Employees.Select(x =>  x.FullName ),
//                                _EquipmentNames = task.Equipment.Select(x => x.Name ),
//                                AddUpdateUrl = UrlContext.GetUrlForAction<TaskController>(x=>x.AddUpdate(null))+"/"+task.EntityId,
//                                _Title = WebLocalizationKeys.TASK_INFORMATION.ToString()
//
//            };
//            return PartialView( model);
            return null;
        }

        public ActionResult Delete(ViewModel input)
        {
            var task = _repository.Find<Task>(input.EntityId);
            if (!task.Complete)
            {
                _repository.HardDelete(task);
                _repository.UnitOfWork.Commit();
            }
            return null;
        }

        public ActionResult DeleteMultiple(BulkActionViewModel input)
        {
            input.EntityIds.Each(x =>
            {
                var item = _repository.Find<Task>(x);
                if(!item.Complete) _repository.HardDelete(item);
            });
            _repository.Commit();
            return Json(new Notification { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(TaskViewModel input)
        {
            var task = input.EntityId > 0 ? _repository.Find<Task>(input.EntityId) : new Task();

            mapItem(task, input);
            mapChildren(task, input);
            ICrudManager crudManager = null;
            if (task.Complete && !task.InventoryDecremented)
            {
                crudManager = _inventoryService.DecrementTaskProduct(task);
                task.InventoryDecremented = true;
            }
            crudManager = _saveEntityService.ProcessSave(task, crudManager);
            var notification = crudManager.Finish();
            return Json(notification);
        }

        private void mapItem(Task item, TaskViewModel input)
        {
            item.ScheduledDate = input.ScheduledDate;
            item.ScheduledStartTime = null;
            if (input.ScheduledStartTime.HasValue)
                item.ScheduledStartTime =
                    DateTime.Parse(input.ScheduledDate.Value.ToShortDateString() + " " +
                                   input.ScheduledStartTime.Value.ToShortTimeString());
            item.ScheduledEndTime = null;
            if (input.ScheduledEndTime.HasValue)
            {
                item.ScheduledEndTime = DateTime.Parse(input.ScheduledDate.Value.ToShortDateString() + " " + input.ScheduledEndTime.Value.ToShortTimeString());
            }
            item.ActualTimeSpent = input.ActualTimeSpent;
            item.QuantityNeeded = input.QuantityNeeded;
            item.QuantityUsed = input.QuantityUsed;
            item.Notes = input.Notes;
            item.Complete = input.Complete;
            
        }

        private void mapChildren(Task task,TaskViewModel model)
        {
            task.ClearEmployees();
            task.ClearEquipment();
            var newEmployees = new List<User>();
            var newEquipment = new List<Equipment>();
            if (model.Employees !=null && model.Employees.selectedItems != null)
            {model.Employees.selectedItems.Each(x => newEmployees.Add(_repository.Find<User>(Int32.Parse(x.id))));}
            _updateCollectionService.Update(task.Employees, newEmployees, task.AddEmployee, task.RemoveEmployee);

            if (model.Equipment!=null && model.Equipment.selectedItems != null)
            {model.Equipment.selectedItems.Each(x => newEquipment.Add(_repository.Find<Equipment>(Int32.Parse(x.id))));}
            _updateCollectionService.Update(task.Equipment, newEquipment, task.AddEquipment, task.RemoveEquipment);

            task.TaskType = _repository.Find<TaskType>(model.TaskTypeEntityId);
            task.Field = _repository.Find<Field>(model.FieldEntityId);
            task.InventoryProduct = _repository.Find<InventoryProduct>(model.InventoryProductProductEntityId);
        }
    }

    
}