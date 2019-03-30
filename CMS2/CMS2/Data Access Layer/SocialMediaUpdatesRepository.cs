using CMS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.Data_Access_Layer
{
    public class SocialMediaUpdatesRepository
    {
        private CMS2Context db = new CMS2Context();

        public List<SocialMediaUpdates> getAllUpdates()
        {
            var allUpdates = db.SocialMediaUpdates.ToList();
            return allUpdates;
        }

        public SocialMediaUpdates getUpdateById(int? Id)
        {
            var update = db.SocialMediaUpdates.Find(Id);
            return update;
        }
        //to be done
    }
}