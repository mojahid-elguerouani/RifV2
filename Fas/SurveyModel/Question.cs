using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FasDemo.SurveyModel
{
    public class Question
    {
        public Question()
        {
            this.Choices = new HashSet<Choice>();
        }
        public int QuestionId { get; set; }
        public string QuestionBody { get; set; }
        public string AnswerType { get; set; }
        public string SurveyId { get; set; }
        public Survey Survey { get; set; }
        public virtual ICollection<Choice> Choices { get; set; }
    }
    public class Choice
    {
        public int ChoiceId { get; set; }
        public string SurveyId { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public string ChoiceText { get; set; }
        public int orderid { get; set; }

    }
    public class Response
    {
        [Key]
        public int Id { get; set; }

        public string ResponseId { get; set; }
        public string SurveyId { get; set; } 
        
        public int QuestionId { get; set; } 

        public string ChoiceText { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public int ChoiceId { get; set; } 


    }
    

}
