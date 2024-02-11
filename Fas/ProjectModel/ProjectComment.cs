using FasDemo.Models;
using ProjectManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FasDemo.ProjectModel
{
    public class ProjectComment : Base
    {
        public int ProjectCommentId { get; set; }
        public string Comment { get; set; }
        public string CommentFromId { get; set; }
        public ApplicationUser CommentFrom { get; set; }
        public string CommentToId { get; set; }
        public ApplicationUser CommentTo { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public List<CommentImage> CommentImages  { get; set; } = new List<CommentImage>();
    }
}
