﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.CoreViewModels;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Models
{
    public class EmailJobViewModel:ViewModel
    {
        public TokenInputViewModel Subscribers { get; set; }
        public IEnumerable<SelectListItem> _EmailTemplateEntityIdList { get; set; }
        public IEnumerable<SelectListItem> _EmailJobTypeEntityIdList { get; set; }
        public IEnumerable<SelectListItem> _FrequencyList { get; set; }
        public IEnumerable<SelectListItem> _StatusList { get; set; }

        [ValidateNonEmpty]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        [ValidateNonEmpty]
        [ValueOf(typeof(EmailFrequency))]
        public string Frequency { get; set; }
        [ValueOf(typeof(Status))]
        public string Status { get; set; }

        public int EmailTemplateEntityId { get; set; }
        public int EmailJobTypeEntityId { get; set; }

        public string _saveUrl { get; set; }
    }
}