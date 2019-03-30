using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.Models
{
    public class AssistanceRequired
    {
        public int Id { get; set; }

        public string Assistance { get; set; }

        [JsonIgnore]
        public virtual ICollection<Crisis> Crisis { get; set; }
    }
}