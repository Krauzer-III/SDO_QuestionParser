using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDO_QuestionParser.WordLogic
{
    public class QA_Model
    {
        public int number { get; set; }
        public string questionText { get; set; }
        public List<Answer> answers { get; set; }
        public int CorrectAnswersCount { get; set; }
    }

    public class Answer
    {
        public string numberAnswer { get; set; }
        public string textAnswer { get; set; }
        public bool isCorrectAnswer { get; set; }
    }
}
