using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.Models
{
    public class LevelOfEmergency
    {
        public string Id { get; set; }

        public int Level { get; set; }

        public virtual ICollection<Crisis> ListOfCrisisByEmergency { get; set; }
    }
}