﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FasDemo.Services.App
{
    public static class GenderList
    {
        public static List<String> GetAll()
        {
            List<string> all = new List<string>();

            all.Add("ذكر");
            all.Add("أنثى");

            return all;
        }
    }
}
