using CMS2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.Data_Access_Layer
{
    public class AssistanceRequiredRepository
    {
        CMS2Context db = new CMS2Context();

        public List<AssistanceRequired> getAssistanceRequired()
        {
            var all_assistance_required = db.AssistanceRequireds.ToList();
            return all_assistance_required;
        }

        public AssistanceRequired getAssistanceRequiredById(int? Id)
        {
            return db.AssistanceRequireds.Find(Id);
        }
    }
}