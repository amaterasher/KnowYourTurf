﻿using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Web.Models
{
    public class PurchaseOrderLineItemViewModel:ViewModel
    {
        public PurchaseOrderLineItem Item { get; set; }
    }
}