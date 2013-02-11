﻿using System.Collections.Generic;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.CustomAttributes;
using CC.Core.Enumerations;
using CC.Core.Localization;
using Castle.Components.Validator;
using Status = KnowYourTurf.Core.Enums.Status;

namespace KnowYourTurf.Web.Models.EquipmentVendor
{
    public class EquipmentVendorViewModel : ViewModel
    {
        public IEnumerable<string> VendorContactNames{ get; set; }

        [ValidateNonEmpty]
        public string Company { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        [ValueOf(typeof(State))]
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        [TextArea]
        public string Notes { get; set; }
        [ValueOf(typeof(Status))]
        public string Status { get; set; }

        public string _saveUrl { get; set; }

        public IEnumerable<SelectListItem> _StateList { get; set; }

        public IEnumerable<SelectListItem> _StatusList { get; set; }
    }
}