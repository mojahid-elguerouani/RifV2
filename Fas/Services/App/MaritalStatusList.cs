using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FasDemo.Services.App
{
    public static class MaritalStatusList
    {
        public static List<String> GetAll()
        {
            List<string> all = new List<string>();

            all.Add("أعزب");
            all.Add("متزوج");
            //all.Add("Remarried");
            //all.Add("Separated");
            all.Add("مطلق");
            //all.Add("Widowed");

            return all;
        }
    }
}
