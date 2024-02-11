using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FasDemo.SurveyModel.ViewModel
{
    public class UserVM
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string ProfilImage { get; set; }
    }

    public class QuizVM
    {
        public string QuizID { get; set; }
        public string QuizName { get; set; }
        public List<SelectListItem> ListOfQuizz { get; set; }

    }

    public class QuestionVM
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public string Anwser { get; set; }
        public ICollection<ChoiceVM> Choices { get; set; }
    }

    public class ChoiceVM
    {
        public int ChoiceID { get; set; }
        public string ChoiceText { get; set; }
    }

    public class QuizAnswersVM
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string AnswerQ { get; set; }
        public bool isCorrect { get; set; }
        public int ChoiceId { get; set; }
    }
}
