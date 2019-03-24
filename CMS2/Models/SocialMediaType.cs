using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.Models
{
    public class SocialMediaType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<SocialMediaUpdates> SocialMediaUpdates { get; set; }
    }
}