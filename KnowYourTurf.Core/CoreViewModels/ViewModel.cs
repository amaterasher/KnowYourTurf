using System;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html.Grid;

namespace KnowYourTurf.Core
{
    public class ViewModel
    {
        public long EntityId { get; set; }
        public long ParentId { get; set; }
        public long RootId { get; set; }
        public string AddUpdateUrl { get; set; }
        public string From { get; set; }
        public string Title { get; set; }
        public bool Popup { get; set; }
    }

    public class ListViewModel : ViewModel
    {
        public GridDefinition GridDefinition { get; set; }
        public bool NotPopup { get; set; }
        public string DeleteMultipleUrl { get; set; }
    }
}