using KnowYourTurf.Core.Config;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class ListTypeMap<LISTTYPE> : DomainEntityMap<LISTTYPE>
        where LISTTYPE : ListType
    {
        public ListTypeMap()
        {
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Status);
            ApplyFilter<StatusConditionFilter>("(Status = :condition)");

        }
    }

    public class DocumentCategoryMap : ListTypeMap<DocumentCategory>
    {
        public DocumentCategoryMap()
        {
        }
    }

    public class PhotoCategoryMap : ListTypeMap<PhotoCategory>
    {
        public PhotoCategoryMap()
        {
        }
    }

    public class TaskTypeMap : ListTypeMap<TaskType>
    {
        public TaskTypeMap()
        {
        }
    }

    public class EquipmentTaskTypeMap : ListTypeMap<EquipmentTaskType>
    {
        public EquipmentTaskTypeMap()
        {
        }
    }

    public class EventTypeMap : ListTypeMap<EventType>
    {
        public EventTypeMap()
        {
            Map(x => x.EventColor);
        }
    }

}