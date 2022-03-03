using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDO_QuestionParser.WordLogic
{
    public class ParseMassive
    {
        string questionTrigger;
        List<string> answerTrigger;
        Dictionary<int, List<string>> correctAnswers;

        public ParseMassive(string questionTrigger, List<string> answerTrigger)
        {
            this.questionTrigger = questionTrigger;
            this.answerTrigger = answerTrigger;
        }

        public List<QA_Model> ParseStrings(IEnumerable<string> data)
        {
            List<QA_Model> result = new List<QA_Model>();
            int q = -1, a = -1;
            foreach (var item in data)
            {
                if (item.StartsWith(questionTrigger))
                {
                    q++;
                    result.Add(new QA_Model());
                    result[q].number = q + 1;
                    result[q].questionText = item;
                    result[q].answers = new List<Answer>();
                    result[q].CorrectAnswersCount = 0;
                    a = 0;
                }
                if(StartsWithOneOfTrigger(item))
                {
                    result[q].answers.Add(new Answer());
                    result[q].answers[a].textAnswer = item;
                    result[q].answers[a].numberAnswer = item[0].ToString(); 
                    if(correctAnswers[q].Contains(result[q].answers[a].numberAnswer))
                    {
                        result[q].answers[a].isCorrectAnswer = true;
                        result[q].CorrectAnswersCount++;
                    }
                    else
                    {
                        result[q].answers[a].isCorrectAnswer = false;
                    }

                    a++;
                }
            }

            return result;
        }

        private bool StartsWithOneOfTrigger(string s)
        {
            foreach (var item in answerTrigger)
                if (s.StartsWith(item)) 
                    return true;
            return false;
        }
    }
}
