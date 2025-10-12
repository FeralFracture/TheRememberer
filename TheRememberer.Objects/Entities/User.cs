using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRememberer.Objects.Entities
{
    public class User : EntityBase
    {
        public ulong DiscordId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;

        public ICollection<Image> UploadedImages { get; set; } = new List<Image>();
    }
}
