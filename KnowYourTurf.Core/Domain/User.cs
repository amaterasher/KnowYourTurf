using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CC.Core.Domain;
using CC.Core.Enumerations;
using CC.Core.Localization;
using CC.Security;
using Castle.Components.Validator;
using KnowYourTurf.Core.Services;
using Status = KnowYourTurf.Core.Enums.Status;

namespace KnowYourTurf.Core.Domain
{
    using CC.Core.Html.Grid;

    public class User : DomainEntity,IUser, IPersistableObject
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
        public virtual string FileUrl { get; set; }
        public virtual string ImageFriendlyName { get; set; }
        public virtual Client Client { get; set; }
        public virtual CultureInfo LanguageDefault { get; set; }
        public virtual UserLoginInfo UserLoginInfo { get; set; }
        public virtual string EmergencyContact { get; set; }
        public virtual string EmergencyContactPhone { get; set; }
        public virtual string EmployeeId { get; set; }
        public virtual bool SystemSupport { get; set; }

        public virtual string FullNameLNF
        {
            get { return LastName + ", " + FirstName; }
        }
        public virtual string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public virtual bool IsEmployeeAvailableForTask(Task task)
        {
            var startTime = DateTimeUtilities.StandardToMilitary(task.StartTime.ToString());
            var endTime = DateTimeUtilities.StandardToMilitary(task.EndTime.ToString());
            var conflictingTask = Tasks.FirstOrDefault(x => x != task
                                                                 && (x.ScheduledDate == task.ScheduledDate
                                                                      && DateTimeUtilities.StandardToMilitary(x.StartTime.ToString()) < endTime
                                                                      && DateTimeUtilities.StandardToMilitary(x.EndTime.ToString()) > startTime));
            return conflictingTask == null;
        }

        #region Collections
        private readonly IList<EmailJob> _emailTemplates = new List<EmailJob>();
        public virtual IEnumerable<EmailJob> EmailTemplates { get { return _emailTemplates; } }
        public virtual void RemoveEmailTemplate(EmailJob emailJob) { _emailTemplates.Remove(emailJob); }
        public virtual void AddEmailTemplate(EmailJob emailJob)
        {
            if (!emailJob.IsNew() && _emailTemplates.Contains(emailJob)) return;
            _emailTemplates.Add(emailJob);
        }
        private readonly IList<Task> _tasks = new List<Task>();
        public virtual IEnumerable<Task> Tasks { get { return _tasks; } }
        public virtual void RemoveTask(Task task) { _tasks.Remove(task); }
        public virtual void AddTask(Task task)
        {
            if (!task.IsNew() && _tasks.Contains(task)) return;
            _tasks.Add(task);
        }

        private IList<UserRole> _userRoles = new List<UserRole>();
        public virtual void EmptyUserRoles() { _userRoles.Clear(); }
        public virtual IEnumerable<UserRole> UserRoles { get { return _userRoles; } }
        public virtual void RemoveUserRole(UserRole userRole)
        {
            _userRoles.Remove(userRole);
        }
        public virtual void AddUserRole(UserRole userRole)
        {
            if (_userRoles.Contains(userRole)) return;
            _userRoles.Add(userRole);
        }
        #endregion

        public virtual bool IsEmployee()
        {
            return _userRoles.Any(x => x.Name == "Employee");
        }

        public virtual SecurityInfo SecurityInfo
        {
            get { return new SecurityInfo(FullName, EntityId); }
        }

    }

    public class UserLoginInfo : DomainEntity
    {
        public virtual User User { get; set; }
        public virtual string LoginName { get; set; }
        [ValidateNonEmpty]
        public virtual string Password { get; set; }
        [ValidateNonEmpty]
        public virtual string Salt { get; set; }
        public virtual Guid ByPassToken { get; set; }

    }
}