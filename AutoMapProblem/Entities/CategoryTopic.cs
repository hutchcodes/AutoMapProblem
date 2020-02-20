using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMapProblem.Entities
{
    public class CategoryTopic
    {
        public Guid ProjectId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid TopicId { get; set; }
        public int Order { get; set; }

        public virtual Category? Category { get; set; }
    }
}
