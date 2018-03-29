﻿using SlowNewsBlog.Models.Tables;
using System.Collections.Generic;
using System.Linq;

namespace SlowNewsBlog.Data.Repos
{
    public class InMemoryCatagoryRepo
    {
        public static List<Catagory> catagories = new List<Catagory>()
        {
            new Catagory ()
            {
                CatagoryId = 1,
                CatagoryName = "metapost"
            },
            new Catagory (){CatagoryId= 2,CatagoryName= "horseandbuggy"},
            new Catagory (){CatagoryId=3, CatagoryName="moon"},
            new Catagory (){CatagoryId= 4, CatagoryName= "y2k" }
        };

        public void AddCatagory(Catagory catagory)
        {
            catagories.Add(catagory);
        }

        public void EditCatagory(Catagory catagory)
        {
            catagories.Remove(catagories.Where(cat => cat.CatagoryId == catagory.CatagoryId).FirstOrDefault());
            catagories.Add(catagory);
        }

        public List<Catagory> GetAllCatagories()
        {
            return catagories;
        }

        public Catagory GetCatagory(int id)
        {
            var catagories = GetAllCatagories();
            return catagories.Where(h => h.CatagoryId == id).SingleOrDefault();

        }

        public void RemoveCatagory(int id)
        {
            var allCatagories = GetAllCatagories();
            var toRemove = GetCatagory(id);

            allCatagories.Remove(toRemove);
        }
    }
}
