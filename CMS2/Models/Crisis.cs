using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS2.Models
{
    public class Crisis
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="This field is required")]
        public string CallerName { get; set; }

        [Required]
        [StringLength(8)]
        [DisplayName("Caller Number")]
        public string CallerNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Description")]
        public string Description { get; set; } = "-";

        public DateTime TimeStamp { get; set; }

        public int EmergencyId { get; set; }
        public virtual Emergency Emergency { get; set; }

        public int AssistanceRequiredId { get; set; }
        public virtual AssistanceRequired AssistanceRequired { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}