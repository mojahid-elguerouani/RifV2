namespace ProjectManagment.Models
{
    using FasDemo.Models;
    using System.ComponentModel.DataAnnotations;
    public partial class ReceiveBusinessTaskLogImage : Base
    {
        public int ReceiveBusinessTaskLogImageId { get; set; }

        [StringLength(200)]
        public string ReceiveBusinessTaskLogImageUrl { get; set; }

        public int ReceiveBusinessTaskLogId { get; set; }
        public virtual ReceiveBusinessTaskLog ReceiveBusinessTaskLog { get; set; }

        public string FileName { get; internal set; }
        public string ImageType { get; internal set; }
    }
}
