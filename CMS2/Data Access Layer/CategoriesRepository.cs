using CMS2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.Data_Access_Layer
{
    public class CategoriesRepository
    {
        CMS2Context db = new CMS2Context();

        public List<Category> getCategories()
        {
            var all_categories = db.Categories.ToList();
            return all_categories;
        }

        public Category getCategoryById(int? Id)
        {
            return db.Categories.Find(Id);
        }
    }
}