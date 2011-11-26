using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Castle.Components.Validator;
using KnowYourTurf.Core.Domain.Tools.CustomAttributes;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;
using KnowYourTurf.Core.Services;
using Rhino.Security;

namespace KnowYourTurf.Core.Domain
{
    public class User : DomainEntity,IUser
    {
        [ValidateNonEmpty]
        public virtual string FirstName { get; set; }
        [ValidateNonEmpty]
        public virtual string LastName { get; set; }
        [ValidateNonEmpty]
        public virtual string Email { get; set; }
        [ValidateNonEmpty]
        public virtual string PhoneMobile { get; set; }
        public virtual string PhoneHome { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        [ValueOf(typeof(State))]
        public virtual string State { get; set; }
        public virtual string ZipCode { get; set; }
        [TextArea]
        public virtual string Notes { get; set; }
        public virtual DateTime? BirthDate { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual Company Company { get; set; }
        public virtual CultureInfo LanguageDefault { get; set; }
        public virtual UserLoginInfo UserLoginInfo { get; set; }
        public virtual string EmergencyContact { get; set; }
        public virtual string EmergencyContactPhone { get; set; }
        public virtual string EmployeeId { get; set; }
        
        public virtual string FullName
        {
            get { return "{0} {1}".ToFormat(FirstName, LastName); }
        }

        public virtual bool IsEmployeeAvailableForTask(Task task)
        {
            var startTime = DateTimeUtilities.StandardToMilitary(task.ScheduledStartTime.ToString());
            var endTime = DateTimeUtilities.StandardToMilitary(task.ScheduledEndTime.ToString());
            var conflictingTask = GetTasks().FirstOrDefault(x => x != task
                                                                 && (x.ScheduledDate == task.ScheduledDate
                                                                      && DateTimeUtilities.StandardToMilitary(x.ScheduledStartTime.ToString()) < endTime
                                                                      && DateTimeUtilities.StandardToMilitary(x.ScheduledEndTime.ToString()) > startTime));
            return conflictingTask == null;
        }

        #region Collections
        private readonly IList<EmailJob> _emailTemplates = new List<EmailJob>();
        public virtual IEnumerable<EmailJob> GetEmailTemplates() { return _emailTemplates; }
        public virtual void RemoveEmailTemplate(EmailJob emailJob) { _emailTemplates.Remove(emailJob); }
        public virtual void AddEmailTemplate(EmailJob emailJob)
        {
            if (!emailJob.IsNew() && _emailTemplates.Contains(emailJob)) return;
            _emailTemplates.Add(emailJob);
        }
        private readonly IList<Task> _tasks = new List<Task>();
        public virtual IEnumerable<Task> GetTasks() { return _tasks; }
        public virtual void RemoveTask(Task task) { _tasks.Remove(task); }
        public virtual void AddTask(Task task)
        {
            if (!task.IsNew() && _tasks.Contains(task)) return;
            _tasks.Add(task);
        }
        #endregion

        public virtual SecurityInfo SecurityInfo
        {
            get { return new SecurityInfo(FullName, EntityId); }
        }
    }

    public class UserLoginInfo : DomainEntity
    {
        public virtual string LoginName { get; set; }
        [ValidateNonEmpty]
        public virtual string Password { get; set; }
        [ValidateNonEmpty, ValueOf(typeof(Status))]
        public virtual string Status { get; set; }
        public virtual string UserRoles { get; set; }
        [ValueOf(typeof(UserRole))]
        public virtual string UserType { get; set; }
    }
}