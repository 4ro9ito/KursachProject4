using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Classes
{
    public class Answers
    {
        public string Answer { get; set; }
        public bool Type { get; set; }
        public int CounterAnswers { get; set; }
        public Answers(string NewAnswer, bool NewType, int NewCounterAnswers)
        {
            Answer = NewAnswer;
            Type = NewType;
            CounterAnswers = NewCounterAnswers;
        }
        public void ShowAnswers()
        {
            Console.WriteLine($"{CounterAnswers}. {Answer}");
        }
        public void ShowAnswerOnly()
        {
            Console.WriteLine($"{CounterAnswers}. {Answer}");
        }
        protected Answers()
        {
            Console.WriteLine(Answer);
        }

    }
}
