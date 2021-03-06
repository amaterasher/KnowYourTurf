using System;
using System.Collections.Generic;
using Castle.Components.Validator;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Core.Domain
{
    public class EmailJob:DomainEntity
    {
        [ValidateNonEmpty]
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Sender { get; set; }
        public virtual string Subject { get; set; }
        [ValidateNonEmpty]
        [ValueOf(typeof(EmailFrequency))]
        public virtual string Frequency { get; set; }
        [ValueOf(typeof(Status))]
        public virtual string Status { get; set; }
        [ValidateNonEmpty]
        public virtual EmailTemplate EmailTemplate { get; set; }

        #region Collections
        private readonly IList<User> _subscribers = new List<User>();
        public virtual IEnumerable<User> GetSubscribers() { return _subscribers; }
        public virtual void RemoveSubscriber(User subscriber) { _subscribers.Remove(subscriber); }
        public virtual void AddSubscriber(User subscriber)
        {
            if (!subscriber.IsNew() && _subscribers.Contains(subscriber)) return;
            _subscribers.Add(subscriber);
        }
        #endregion
    }
}