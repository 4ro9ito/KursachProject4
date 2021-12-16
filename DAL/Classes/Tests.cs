using System;
using System.Collections.Generic;

namespace DAL.Classes
{
    public class Tests : Questions
    {
        public List<Questions> ListQuestions { get; set; }
        public string TitleTests { get; set; }
        public double NumberOfPoints { get; set; }
        public int TimeForTest { get; set; }
        public Tests(string NewTitleTests, List<Questions> NewListQuestions, long NewNumberOfPoints, int NewTime)
        {
            TitleTests = NewTitleTests;
            ListQuestions = NewListQuestions;
            NumberOfPoints = NewNumberOfPoints;
            TimeForTest = NewTime;
        }
        public void ShowTests()
        {
            Console.WriteLine("");
            Console.WriteLine($"Название теста: {TitleTests}, количество вопросов: {ListQuestions.Count}");
            for (int i = 0; i < ListQuestions.Count; i++)
            {
                if (ListQuestions[i] != null)
                {
                    ListQuestions[i].ShowQuestions();
                }
                else
                    continue;
                
            }
            Console.WriteLine($"Тест пройден на: {NumberOfPoints}%");
            double Time = TimeForTest / 1000;
            Console.WriteLine($"Отведенное время на тест: {Time} секунд");

        }
        public void ShowTestsTitle()
        {
            Console.WriteLine("");
            Console.WriteLine($"Название теста: {TitleTests}, количество вопросов: {ListQuestions.Count}");
            Console.WriteLine($"Тест пройден на: {NumberOfPoints}%");
            double Time = TimeForTest / 1000;
            Console.WriteLine($"Отведенное время на тест: {Time} секунд");

        }
        public void ShowResults()
        {
            Console.WriteLine($"Вы прошли тест на {NumberOfPoints}%!");
        }
        protected Tests()
        {

        }
    }
}
