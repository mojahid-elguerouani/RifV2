using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FasDemo.SurveyModel
{
    public class Survey
    {
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
        public virtual IList<Question> Questions { get; set; }
    }
}
