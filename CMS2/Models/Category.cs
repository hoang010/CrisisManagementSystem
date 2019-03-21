using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Crisis> Crisis { get; set; }
    }
}