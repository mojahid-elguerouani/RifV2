using System.ComponentModel.DataAnnotations;

namespace FasDemo.ProjectModel
{
    public class ReceiveBusinessCommentImage
    {
        [Key]
        public int ReceiveBusinessCommentImageId { get; set; }

        public string CommentImageUrl { get; set; }
        public int PurchaseCommentId { get; set; }
        public virtual ReceiveBusinessComment ReceiveBusinessComment { get; set; }
        public string FileName { get; internal set; }
        public string ImageType { get; internal set; }
    }
}