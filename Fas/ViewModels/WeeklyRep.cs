using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FasDemo.ViewModels
{
    public class WeeklyRep
    {
        public string Fullname { get; set; }
        public string ProjectName { get; set; }
        public string TaskName { get; set; }
        public DateTime? StartDateActual { get; set; }
        public DateTime? EndDateActual { get; set; }
        public int? Compleation { get; set; }
    }
}
