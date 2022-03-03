using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDO_QuestionParser.WordLogic
{
    public class GIFT_Maker
    {
        List<QA_Model> data;
        string fileName;

        public GIFT_Maker(List<QA_Model> data, string fileName)
        {
            this.data = data;
            this.fileName = fileName;
        }


        #region StringBuilder


        string MakeString()
        {
            StringBuilder sb = new StringBuilder(@"// question: 0  name: Switch category to $course$/top/" + fileName + "\n" + @"$CATEGORY: $course$/top/" + fileName);

            sb.AppendLine();
            sb.AppendLine();


            foreach (var item in data)
            {
                sb.AppendLine(CommentQuestion(item.number));
                sb.AppendLine(HeaderQuestion(item.number, item.questionText));
                int correctCount = item.answers.Count(e => e.isCorrectAnswer);
                if (correctCount > 1)
                {
                    foreach (var aitem in item.answers)
                    {
                        sb.AppendLine(ManyAnswer(aitem.textAnswer, aitem.isCorrectAnswer, strPercent(correctCount)));
                    }
                }
                else
                {
                    foreach (var aitem in item.answers)
                    {
                        sb.AppendLine(OneAnswer(aitem.textAnswer, aitem.isCorrectAnswer));
                    }
                }

                sb.AppendLine("}");
                sb.AppendLine();
            }

            return sb.ToString();

        }

        string CommentQuestion(int num)
            => $"// question: {num}  name: {num}.";

        string HeaderQuestion(int num, string text)
        => $"::{num}.::[html]<p> {text}<br></p>{{";


        string OneAnswer(string answerText, bool rigth)
        {
            try
            {
                if (answerText[answerText.Length - 1] == '.')
                    answerText = answerText.Remove(answerText.Length - 1, 1) + ";";


                if (answerText[answerText.Length - 1] != ';')
                    answerText += ";";
            }
            catch { }
            return rigth ?
                   $"=<P> {answerText}<br></p>" :
                   $"~<P> {answerText}<br></p>";
        }

        string ManyAnswer(string answerText, bool rigth, string percent)
        {
            try
            {
                if (answerText[answerText.Length - 1] == '.')
                    answerText = answerText.Remove(answerText.Length - 1, 1) + ";";


                if (answerText[answerText.Length - 1] != ';')
                    answerText += ";";
            }
            catch { }
            return rigth ?
                   $"~%{percent}%<P> {answerText}<br></p>" :
                   $"~%-{percent}%<P> {answerText}<br></p>";
        }


        string strPercent(int corrects)
        {
            switch (corrects)
            {
                case 2: return "50";
                case 3: return "33.33";
                case 4: return "25";
                case 5: return "20";
                case 6: return "16.66";
                case 7: return "14.28";
                case 8: return "12.5";
                case 9: return "11.11";
                case 10: return "10";
                default: return "100";
            }
        }
        #endregion




    }
}
