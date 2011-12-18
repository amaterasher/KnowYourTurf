using FluentNHibernate.Mapping;

namespace KnowYourTurf.Core.Domain.Persistence
{
    public class EmailJobMap : DomainEntityMap<EmailJob>
    {
        public EmailJobMap()
        {
            Map(x => x.Name);
            Map(x => x.Subject);
            Map(x => x.Sender);
            Map(x => x.Description);
            Map(x => x.Frequency);
            References(x => x.EmailTemplate);
//            HasManyToMany(x => x.GetSubscribers()).Access.CamelCaseField(Prefix.Underscore).LazyLoad().Cascade.SaveUpdate();
        }
    }
}