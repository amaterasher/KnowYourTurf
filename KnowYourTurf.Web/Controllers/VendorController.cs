﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using KnowYourTurf.Web.Models.Vendor;

namespace KnowYourTurf.Web.Controllers
{
    public class VendorController:KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;

        public VendorController(IRepository repository,
            ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult AddEdit(ViewModel input)
        {
            var vendor = input.EntityId > 0 ? _repository.Find<Vendor>(input.EntityId) : new Vendor();
            var availableChemicals = _repository.FindAll<Chemical>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var selectedChemicals = vendor.GetAllProductsOfType("Chemical").Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var availableFertilizers = _repository.FindAll<Fertilizer>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var selectedFertilizers = vendor.GetAllProductsOfType("Fertilizer").Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var availableMaterials = _repository.FindAll<Material>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var selectedMaterials = vendor.GetAllProductsOfType("Material").Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var availableSeeds = _repository.FindAll<Seed>().Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            var selectedSeeds = vendor.GetAllProductsOfType("Seed").Select(x => new TokenInputDto { id = x.EntityId.ToString(), name = x.Name });
            
            var model = new VendorViewModel
            {
                Vendor = vendor,
                AvailableChemicals = availableChemicals,
                SelectedChemicals = selectedChemicals,
                AvailableFertilizers = availableFertilizers,
                SelectedFertilizers = selectedFertilizers,
                AvailableMaterials = availableMaterials,
                SelectedMaterials = selectedMaterials,
                AvailableSeeds = availableSeeds,
                SelectedSeeds = selectedSeeds
            };
            return PartialView("VendorAddUpdate", model);
        }
      
        public ActionResult Display(ViewModel input)
        {
            var vendor = _repository.Find<Vendor>(input.EntityId);
            var contactNames = new List<string>();
            vendor.GetContacts().Each(x => contactNames.Add(x.FullName)); 
            var model = new VendorViewModel
                            {
                                Vendor = vendor,
                                VendorContactNames = contactNames,
                                AddEditUrl = UrlContext.GetUrlForAction<VendorController>(x => x.AddEdit(null)) + "/" + vendor.EntityId
                            };
            return PartialView("VendorView", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var vendor = _repository.Find<Vendor>(input.EntityId);
            _repository.HardDelete(vendor);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(VendorViewModel input)
        {
            var vendor = input.Vendor.EntityId > 0 ? _repository.Find<Vendor>(input.Vendor.EntityId) : new Vendor();
            var newTask = mapToDomain(input, vendor);

            var crudManager = _saveEntityService.ProcessSave(newTask);
            var notification = crudManager.Finish();
            return Json(notification, JsonRequestBehavior.AllowGet);
        }

        private Vendor mapToDomain(VendorViewModel input, Vendor vendor)
        {
            var vendorModel = input.Vendor;
            vendor.Company = vendorModel.Company;
            vendor.Fax = vendorModel.Fax;
            vendor.Phone = vendorModel.Phone;
            vendor.Address1 = vendorModel.Address1;
            vendor.Address2 = vendorModel.Address2;
            vendor.City = vendorModel.City;
            vendor.State = vendorModel.State;
            vendor.ZipCode = vendorModel.ZipCode;
            vendor.Website = vendorModel.Website;
            vendor.Status = vendorModel.Status;
            vendor.Notes = vendorModel.Notes;
            vendor.ClearProducts();
            if(input.ChemicalInput.IsNotEmpty())
                input.ChemicalInput.Split(',').Each(x => vendor.AddProduct(_repository.Find<Chemical>(Int32.Parse(x))));
            if (input.FertilizerInput.IsNotEmpty())
                input.FertilizerInput.Split(',').Each(x => vendor.AddProduct(_repository.Find<Fertilizer>(Int32.Parse(x))));
            if (input.MaterialInput.IsNotEmpty())
                input.MaterialInput.Split(',').Each(x => vendor.AddProduct(_repository.Find<Material>(Int32.Parse(x))));
            if (input.SeedInput.IsNotEmpty())
                input.SeedInput.Split(',').Each(x => vendor.AddProduct(_repository.Find<Seed>(Int32.Parse(x))));
            return vendor;
        }
    }
}