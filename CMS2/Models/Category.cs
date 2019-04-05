using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CMS2.Models
{
    public class Category
    {
        public int Id { get; set; }

        [DisplayName("Category")]
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Crisis> Crisis { get; set; }
    }
}