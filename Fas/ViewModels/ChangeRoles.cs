using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FasDemo.ViewModels
{
    //view model for changeroles screen
    public class ChangeRoles
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool IsTodoRegistered { get; set; }
        public bool IsMembershipRegistered { get; set; }
        public bool IsRoleRegistered { get; set; }
        public bool IsEmployeeRegistered { get; set; }
        public bool IsTicketRegistered { get; set; }       
        public bool IsProjectRegistered { get; set; }

        public bool IsHomeRegistered { get; set; }
        public bool IsSettingsRegistered { get; set; }
        public bool IsContractorsRegistered { get; set; }
        public bool IsReceiveBusinessRegistered { get; set; }
        public bool IsReceiveBusinessTaskRegistered { get; set; }
        
    }
}
