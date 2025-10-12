using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRememberer.Objects.Entities
{
    public class ImageTag
    {
        public Guid ImageId { get; set; }
        public Image Image { get; set; } = null!;
        public Guid TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}
