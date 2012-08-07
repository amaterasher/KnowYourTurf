﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using KnowYourTurf.Security.Interfaces;
using FluentNHibernate.Utils;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using KnowYourTurf.Web.Services;
using System.Linq;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class EmployeeController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;
        private readonly ISessionContext _sessionContext;
        private readonly IFileHandlerService _fileHandlerService;
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IUpdateCollectionService _updateCollectionService;

        public EmployeeController(IRepository repository,
            ISaveEntityService saveEntityService,
            ISessionContext sessionContext,
            IFileHandlerService fileHandlerService,
            IAuthorizationRepository authorizationRepository,
            IUpdateCollectionService updateCollectionService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
            _sessionContext = sessionContext;
            _fileHandlerService = fileHandlerService;
            _authorizationRepository = authorizationRepository;
            _updateCollectionService = updateCollectionService;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
//            var employee = input.EntityId > 0 ? _repository.Find<User>(input.EntityId) : new User();
//            var availableUserRoles = Enumeration.GetAll<UserType>(true).Select(x => new TokenInputDto { id = x.Key, name = x.Key});
//            IEnumerable<TokenInputDto> selectedUserRoles;
//            if (input.EntityId > 0 && employee.UserRoles != null)
//                selectedUserRoles =
//                    employee.UserRoles.Select(x => new TokenInputDto {id = x.EntityId.ToString(), name = x.Name});
//            else selectedUserRoles = null;
//
//            var model = new UserViewModel
//            {
//                Item = employee,
//                AvailableItems = availableUserRoles,
//                SelectedItems = selectedUserRoles,
//                _Title = WebLocalizationKeys.EMPLOYEE_INFORMATION.ToString()
//            };
//            return PartialView("EmployeeAddUpdate", model);
            return null;
        }
      
        public ActionResult Display(ViewModel input)
        {
//            var employee = _repository.Find<User>(input.EntityId);
//            var model = new UserViewModel
//                            {
//                                Item = employee,
//                                AddUpdateUrl = UrlContext.GetUrlForAction<EmployeeController>(x => x.AddUpdate(null)) + "/" + employee.EntityId,
//                                _Title = WebLocalizationKeys.EMPLOYEE_INFORMATION.ToString()
//                            };
//            return PartialView("EmployeeView", model);
            return null;
        }

        public ActionResult Delete(ViewModel input)
        {
            var employee = _repository.Find<User>(input.EntityId);
            var rulesEngineBase = ObjectFactory.Container.GetInstance<RulesEngineBase>("DeleteEmployeeRules");
            var rulesResult = rulesEngineBase.ExecuteRules(employee);
            if(!rulesResult.Success)
            {
                Notification notification = new Notification(rulesResult);
                return Json(notification);
            }
            _repository.SoftDelete(employee);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(UserViewModel input)
        {
            User employee;
            if (input.EntityId > 0)
            {
                employee = _repository.Find<User>(input.EntityId);
            }
            else
            {
                employee = new User();
                var companyId = _sessionContext.GetCompanyId();
                var company = _repository.Find<Company>(companyId);
                employee.Company = company;
            }
            employee = mapToDomain(input, employee);
            mapRolesToGroups(employee);
            if (input.DeleteImage)
            {
                _fileHandlerService.DeleteFile(employee.ImageUrl);
                employee.ImageUrl = string.Empty;
            }

            if (_fileHandlerService.RequsetHasFile())
            {
                employee.ImageUrl = _fileHandlerService.SaveAndReturnUrlForFile("CustomerPhotos/Employees");
                employee.ImageFriendlyName = employee.FirstName + "_" + employee.LastName;
            }
            var crudManager = _saveEntityService.ProcessSave(employee);

            var notification = crudManager.Finish();
            return Json(notification,"text/plain");
        }

        private void mapRolesToGroups(User employee)
        {
            foreach (var x in employee.UserRoles)
            {
                if (x.Name == UserType.Administrator.Key)
                {
                    _authorizationRepository.AssociateUserWith(employee, UserType.Administrator.Key);
                }else if(!employee.UserRoles.Any(r=>r.Name==x.Name))
                {
                    _authorizationRepository.DetachUserFromGroup(employee, UserType.Administrator.Key);
                }
                if (x.Name== UserType.Employee.Key)
                {
                    _authorizationRepository.AssociateUserWith(employee, UserType.Employee.Key);
                }
                else if (!employee.UserRoles.Any(r => r.Name == x.Name))
                {
                    _authorizationRepository.DetachUserFromGroup(employee, UserType.Employee.Key);
                }
                if (x.Name == UserType.Facilities.Key)
                {
                    _authorizationRepository.AssociateUserWith(employee, UserType.Facilities.Key);
                }
                else if (!employee.UserRoles.Any(r => r.Name == x.Name))
                {
                    _authorizationRepository.DetachUserFromGroup(employee, UserType.Facilities.Key);
                }
                if (x.Name == UserType.KYTAdmin.Key)
                {
                    _authorizationRepository.AssociateUserWith(employee, UserType.KYTAdmin.Key);
                }
                else if(!employee.UserRoles.Any(r=>r.Name==x.Name))
                {
                    _authorizationRepository.DetachUserFromGroup(employee, UserType.KYTAdmin.Key);
                }
                if (x.Name == UserType.MultiTenant.Key)
                {
                    _authorizationRepository.AssociateUserWith(employee, UserType.MultiTenant.Key);
                }
                else if (!employee.UserRoles.Any(r => r.Name == x.Name))
                {
                    _authorizationRepository.DetachUserFromGroup(employee, UserType.MultiTenant.Key);
                }
            }
        }

        private User mapToDomain(UserViewModel model, User employee)
        {
            employee.EmployeeId = model.EmployeeId;
            employee.Address1 = model.Address1;
            employee.Address2 = model.Address2;
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.EmergencyContact = model.EmergencyContact;
            employee.EmergencyContactPhone = model.EmergencyContactPhone;
            employee.Email = model.Email;
            employee.PhoneMobile = model.PhoneMobile;
            employee.City = model.City;
            employee.State = model.State;
            employee.ZipCode = model.ZipCode;
            employee.Notes = model.Notes;
            if(employee.UserLoginInfo == null)
            {
                employee.UserLoginInfo = new UserLoginInfo();
            }
            employee.UserLoginInfo.Password = model.UserLoginInfoPassword;
            employee.UserLoginInfo.LoginName = model.Email;
            employee.UserLoginInfo.Status = model.UserLoginInfoStatus;
            _updateCollectionService.Update(employee.UserRoles, model.UserRoles, employee.AddUserRole, employee.RemoveUserRole);
            if (!employee.UserRoles.Any())
            {
                var emp = _repository.Query<UserRole>(x => x.Name == UserType.Employee.ToString()).FirstOrDefault();
                employee.AddUserRole(emp);
            }
            return employee;
        }

    }
}