﻿using System.Collections.Generic;
using System.Web.Mvc;
using CC.Core.CoreViewModelAndDTOs;
using CC.Core.Localization;
using KnowYourTurf.Core.Enums;

namespace KnowYourTurf.Web.Models
{
    public class ListTypeViewModel : ViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [ValueOf(typeof(Status))]
        public string Status { get; set; }
        public string _saveUrl { get; set; }
        public IEnumerable<SelectListItem> _StatusList { get; set; }
    }

    public class EventTypeViewModel : ListTypeViewModel
    {
        public string EventColor { get; set; }

    }

}