namespace ProjectManagment.Models
{
    using FasDemo.Models;
    public partial class ContractorImage : Base
    {
        public int Id { get; set; }


        public string ContractorImageUrl { get; set; }
        public int ContractorId { get; set; }
        public virtual Contractor Contractor { get; set; }
    }
}
