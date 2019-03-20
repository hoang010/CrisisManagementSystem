using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CMS2.Models
{
    public class CMS2Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public CMS2Context() : base("name=CMS2Context")
        {
        }

        public System.Data.Entity.DbSet<CMS2.Models.Crisis> Crises { get; set; }

        public System.Data.Entity.DbSet<CMS2.Models.AssistanceRequired> AssistanceRequireds { get; set; }

        public System.Data.Entity.DbSet<CMS2.Models.LevelOfEmergency> LevelOfEmergencies { get; set; }
    }
}
