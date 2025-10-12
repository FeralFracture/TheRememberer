using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRememberer.Objects.Entities
{
    public class Tag : EntityBase
    {
        public string Value { get; set; } = string.Empty;
        public Guid CreatorId { get; set; }
        public string? Color { get; set; }


        public ICollection<ImageTag> ImageTags { get; set; } = new List<ImageTag>();
    }
}
