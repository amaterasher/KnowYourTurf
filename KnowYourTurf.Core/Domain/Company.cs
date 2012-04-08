using System.Collections.Generic;

namespace KnowYourTurf.Core.Domain
{
    public class Company : DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual double TaxRate { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual int NumberOfCategories { get; set; }
        #region Collections
        private IList<Category> _categories = new List<Category>();
        public virtual IEnumerable<Category> Categories { get { return _categories; } }
        public virtual void ClearCategory() { _categories = new List<Category>(); }
        public virtual void RemoveCategory(Category category) { _categories.Remove(category); }
        public virtual void AddCategory(Category category)
        {
            if (!category.IsNew() && _categories.Contains(category)) return;
            _categories.Add(category);
        }
        #endregion
    }

    public class Category:DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}