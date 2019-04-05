﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS2.Models
{
    public class SummaryReport
    {
        public int Id { get; set; }

        [DataType(DataType.MultilineText)]
        public string ReportDetails { get; set; }

        public DateTime TimeStamp { get; set; }

        public bool Approved { get; set; } = false;

        public ICollection<Crisis> all_Crisis { get; set; }
    }
}