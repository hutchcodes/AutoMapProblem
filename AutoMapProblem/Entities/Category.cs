using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMapProblem.Entities
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public Guid ProjectId { get; set; }
        public string? Name { get; set; }
        public int Order { get; set; }
        public Guid? ParentCategoryId { get; set; }

        public Category? ParentCategory { get; set; }
        public virtual ICollection<CategoryTopic> CategoryTopics { get; set; } = new HashSet<CategoryTopic>();
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();
    }
}
