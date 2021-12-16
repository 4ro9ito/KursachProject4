using System;
using System.Collections.Generic;

namespace DAL.Classes
{
    public class Questions : Answers
    {
        public List<Answers> ListAnswers { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Questions(string NewTitle, string NewDescription, List<Answers> NewAnswers)
        {
            Title = NewTitle;
            Description = NewDescription;
            ListAnswers = NewAnswers;
        }
        public void ShowQuestions()
        {
            Console.WriteLine("");
            Console.WriteLine($"Название вопроса: {Title}");
            Console.WriteLine($"Вопрос: {Description}");
            for (int i = 0; i < ListAnswers.Count; i++)
            {
                if (ListAnswers[i] != null)
                    ListAnswers[i].ShowAnswers();
                else
                    continue;
            }
        }
        public void ShowQuestionsTitle()
        {
            Console.WriteLine("");
            Console.WriteLine($"Название вопроса: {Title}");
            Console.WriteLine($"Вопрос: {Description}");
        }
        protected Questions()
        {

        }

          
    }
}
