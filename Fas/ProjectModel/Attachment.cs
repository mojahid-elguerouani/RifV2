namespace ProjectManagment.Models
{
    using FasDemo.Models;
    using System;
    using System.ComponentModel.DataAnnotations.Schema; 
    public partial class Attachment : Base
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime? DateAdded { get; set; }

        public string ProjectId { get; set; }

        public bool? IsApproved { get; set; } 
 
    }
}
