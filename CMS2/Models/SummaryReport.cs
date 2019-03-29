using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.Models
{
    public class SummaryReport
    {
        public int Id { get; set; }

        public string ReportDetails { get; set; }

        public DateTime TimeStamp { get; set; }

        public ICollection<Crisis> all_Crisis { get; set; }
    }
}