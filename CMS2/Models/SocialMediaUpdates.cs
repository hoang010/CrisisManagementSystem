using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.Models
{
    public class SocialMediaUpdates
    {
        public int Id { get; set; }


        public string Description { get; set; }

        public DateTime TimeStamp { get; set; }

        public int SocialMediaTypeId { get; set; }
        public virtual SocialMediaType SocialMediaType { get; set; }

    }
}