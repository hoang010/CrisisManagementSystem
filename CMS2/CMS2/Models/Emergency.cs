using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.Models
{
    public class Emergency
    {
        public int Id { get; set; }

        public string Level { get; set; }

        [JsonIgnore]
        public virtual List<Crisis> CrisisByLevel { get; set; }
    }
}