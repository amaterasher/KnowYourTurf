using System.Collections.Generic;
using KnowYourTurf.Core;

namespace KnowYourTurf.Web.Controllers
{
    public class BulkActionViewModel : ViewModel
    {
        public IEnumerable<int> EntityIds { get; set; }
    }
}