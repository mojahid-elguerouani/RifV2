using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FasDemo.ViewModels
{
    public class TaskSummary
    {
        public string TaskLate { get; set; }
        public string TaskLatePercentage { get; set; }
        public string TaskCurrent { get; set; }
        public string TaskCurrentPercentage { get; set; }
        public string TaskFinished { get; set; }
        public string TaskFinishedPercentage { get; set; }
        public string TasksCustomer { get; set; }
        public string TasksCustomerPercentage { get; set; }
        public string TaskAccountant { get; internal set; }
        public string TaskAccountantPercentage { get; internal set; }
    }
}
