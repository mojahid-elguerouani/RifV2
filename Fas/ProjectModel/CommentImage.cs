namespace FasDemo.ProjectModel
{
    public class CommentImage
    {
        public int CommentImageId { get; set; } 
        
        public string CommentImageUrl { get; set; } 
        public int ProjectCommentId { get; set; }
        public virtual ProjectComment ProjectComment { get; set; }
        public string FileName { get; internal set; }
        public string ImageType { get; internal set; }
    }
}