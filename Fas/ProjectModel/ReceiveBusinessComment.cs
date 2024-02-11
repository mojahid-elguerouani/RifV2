using FasDemo.Models;
using ProjectManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FasDemo.ProjectModel
{
    public class ReceiveBusinessComment : Base
    {
        public int ReceiveBusinessCommentId { get; set; }
        public string Comment { get; set; }
        public string CommentFromId { get; set; }
        public ApplicationUser CommentFrom { get; set; }
        public string CommentToId { get; set; }
        public ApplicationUser CommentTo { get; set; }
        public int PurchaseId { get; set; }
        public virtual ReceiveBusiness ReceiveBusiness { get; set; }
        public List<ReceiveBusinessCommentImage> ReceiveBusinessCommentImages { get; set; } = new List<ReceiveBusinessCommentImage>();
    }
}
