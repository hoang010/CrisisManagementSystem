using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.Models
{
    public class Crisis
    {
        public int Id { get; set; }

        public string CallerName { get; set; }
        public string CallerNumber { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public int LevelOfEmergencyId { get; set; }
        public virtual LevelOfEmergency LevelOfEmergency { get; set; }

        public int AssistanceRequiredId { get; set; }
        public virtual AssistanceRequired AssistanceRequired { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}