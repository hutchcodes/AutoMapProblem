using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMapProblem.DTO
{
    public class CategoryTopicList
    {
        public Guid CategoryId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid TopicId { get; set; }
        public int Order { get; set; }
    }
}
