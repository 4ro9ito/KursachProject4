using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DAL.Classes;
using DAL;
using System.Threading;

namespace BLL
{
    public class EntityService
    {
        EntityContext DB = new EntityContext();
        public List<Answers> AnswersNewList = new List<Answers>();
        public List<Questions> QuestionNewList = new List<Questions>();
        public List<Questions> QuestionNewListForTest = new List<Questions>();
        public List<Tests> TestsNewList = new List<Tests>();
        public string TypeQuestionService;
        public string DescriptionQuestionService;
        public int CounterForAnswers = 1;
        public int CounterForQuestions = 0;
        public int CounterForTest = 0;
        public int CounterForTestQuestion = 0;
        public string TypeTestService;
        public int NewTimeForTest;
        public int CheckCorrectDeleting;

        public int TimeIsWorking = 0;
        public int NumberOfQuestion = 0;
        public double NumberOfCorrectAnswers;
        public double AllCorrectAnswers;


        //Show Questions
        public int ServiceShowQuestions()
        {
            CounterForQuestions = 0;
            for (int i = 0; i < QuestionNewList.Count; i++)
            {
                if (QuestionNewList[i] != null)
                {
                    QuestionNewList[i].ShowQuestions();
                    CounterForQuestions++;
                }
            }
            return CounterForQuestions;

        }
        public int ServiceShowQuestionsForTest()
        {
            CounterForQuestions = 0;
            for (int i = 0; i < QuestionNewListForTest.Count; i++)
            {
                if (QuestionNewListForTest[i] != null)
                {
                    QuestionNewListForTest[i].ShowQuestions();
                    CounterForQuestions++;
                }
            }
            return CounterForQuestions;

        }


        //Add Question
        public string AddQuestionType(string NewTypeOfQuestion)
        {

            TypeQuestionService = NewTypeOfQuestion;


            return NewTypeOfQuestion;
        }
        public string AddQuestionDescription(string NewDescriptionOfQuestion)
        {

            DescriptionQuestionService = NewDescriptionOfQuestion;


            return NewDescriptionOfQuestion;
        }
        public void AddAnswer(string CreateNewAnswer, bool CreateBool)
        {
            Answers NewAnswer = new Answers(CreateNewAnswer, CreateBool, CounterForAnswers);
            AnswersNewList.Add(NewAnswer);
            CounterForAnswers++;
            for (int i = 0; i < AnswersNewList.Count; i++)
            {
                if (AnswersNewList[i] != null)
                {
                    AnswersNewList[i].CounterAnswers = i + 1;
                }
            }
        }
        public int DeleteAnswer(int IndexOfAnswer)
        {
            int CorrectIndexOfAnswer = IndexOfAnswer - 1;
            if (AnswersNewList[CorrectIndexOfAnswer].Type == true)
            {
                AnswersNewList.RemoveAt(CorrectIndexOfAnswer);
                for (int i = 0; i < AnswersNewList.Count; i++)
                {
                    if (AnswersNewList[i] != null)
                    {
                        AnswersNewList[i].CounterAnswers = i + 1;
                    }
                }
                return -1;
            }
            else
            {
                AnswersNewList.RemoveAt(CorrectIndexOfAnswer);
                for (int i = 0; i < AnswersNewList.Count; i++)
                {
                    if (AnswersNewList[i] != null)
                    {
                        AnswersNewList[i].CounterAnswers = i + 1;
                    }
                }
                return 1;
            }

        }
        public void CreateQuestion()
        {
            List<Answers> AnswersNewListForQuest = new List<Answers>(AnswersNewList);
            Questions NewQuestion = new Questions(TypeQuestionService, DescriptionQuestionService, AnswersNewListForQuest);
            QuestionNewList.Add(NewQuestion);
            DB.QuestionsDB(QuestionNewList);
        }
        public void ResetCounter()
        {
            CounterForAnswers = 1;
            AnswersNewList.RemoveRange(0, AnswersNewList.Count);
        }
        public void ServiceShowAnswers()
        {
                for (int i = 0; i < AnswersNewList.Count; i++)
                {
                    if (AnswersNewList[i] != null)
                        AnswersNewList[i].ShowAnswers();
                }
        }

        //Delete Question
        public void DeleteQuestion(string NewDeleteQuestion)
        {
            for (int i = 0; i < QuestionNewList.Count; i++)
            {
                if ((QuestionNewList[i].Title != null) && (Regex.IsMatch(QuestionNewList[i].Title, NewDeleteQuestion)))
                {
                    QuestionNewList.RemoveAt(i);
                }
            }

        }

        //Update Question
        public int CheckQuestion(string TitleCheck)
        {
            CounterForTestQuestion = -1;
            for (int i = 0; i < QuestionNewList.Count; i++)
            {
                if ((QuestionNewList[i] != null) & (Regex.IsMatch(QuestionNewList[i].Title, TitleCheck)))
                {
                    CounterForTestQuestion = i;
                    break;
                }
            }
                return CounterForTestQuestion;

        }
        public void UpdateTitle(string NewUpdateTitle, int NewCheckQuestion)
        {
            QuestionNewList[NewCheckQuestion].Title = NewUpdateTitle;
        }
        public void UpdateDescription(string NewUpdateDescription, int NewCheckQuestion)
        {
            QuestionNewList[NewCheckQuestion].Description = NewUpdateDescription;
        }
        public void UpdateAnswers(string NewUpdateTitle, int NewCheckQuestion)
        {
            QuestionNewList[NewCheckQuestion].Title = NewUpdateTitle;
        }
        public int ServiceUpdateCountCorrectAnswers(int NewCheckQuestion)
        {
            int Count = 0;
            for (int i = 0; i < QuestionNewList[NewCheckQuestion].ListAnswers.Count; i++)
            {
                if (QuestionNewList[NewCheckQuestion].ListAnswers[i].Type == true)
                {
                    Count++;
                }

            }
            return Count;

        }
        public int ServiceUpdateCountAnswers(int NewCheckQuestion)
        {
            int Count = 0;
            for (int i = 0; i < QuestionNewList[NewCheckQuestion].ListAnswers.Count; i++)
            {
                if (QuestionNewList[NewCheckQuestion].ListAnswers[i] != null)
                {
                    Count++;
                }

            }
            return Count;

        }
        public void AddUpdateAnswer(string CreateNewAnswer, bool CreateBool, int NewCheckQuestion)
        {
            Answers NewAnswer = new Answers(CreateNewAnswer, CreateBool, CounterForAnswers);
            QuestionNewList[NewCheckQuestion].ListAnswers.Add(NewAnswer);
            CounterForAnswers++;
            for (int i = 0; i < QuestionNewList[NewCheckQuestion].ListAnswers.Count; i++)
            {
                if (QuestionNewList[NewCheckQuestion].ListAnswers[i] != null)
                {
                    QuestionNewList[NewCheckQuestion].ListAnswers[i].CounterAnswers = i + 1;
                }
            }
        }
        public void ServiceUpdateShowAnswers(int NewCheckQuestion)
        {
                for (int i = 0; i < QuestionNewList[NewCheckQuestion].ListAnswers.Count; i++)
                {
                    if (QuestionNewList[NewCheckQuestion].ListAnswers[i].Answer != null)
                        QuestionNewList[NewCheckQuestion].ListAnswers[i].ShowAnswers();
                }
        }
        public int DeleteUpdateAnswer(int IndexOfAnswer, int NewCheckQuestion)
        {
            int CorrectIndexOfAnswer = IndexOfAnswer - 1;
            if (QuestionNewList[NewCheckQuestion].ListAnswers[CorrectIndexOfAnswer].Type == true)
            {
                QuestionNewList[NewCheckQuestion].ListAnswers.RemoveAt(CorrectIndexOfAnswer);
                for (int i = 0; i < AnswersNewList.Count; i++)
                {
                    if (QuestionNewList[NewCheckQuestion].ListAnswers[i] != null)
                    {
                        QuestionNewList[NewCheckQuestion].ListAnswers[i].CounterAnswers = i + 1;
                    }
                }
                return -1;
            }
            else
            {
                QuestionNewList[NewCheckQuestion].ListAnswers.RemoveAt(CorrectIndexOfAnswer);
                for (int i = 0; i < QuestionNewList[NewCheckQuestion].ListAnswers.Count; i++)
                {
                    if (QuestionNewList[NewCheckQuestion].ListAnswers[i] != null)
                    {
                        QuestionNewList[NewCheckQuestion].ListAnswers[i].CounterAnswers = i + 1;
                    }
                }
                return 1;
            }

        }

        //Check Correct Question
        public void ServiceShowQuestionsWithAnswers()
        {
            for (int i = 0; i < QuestionNewList.Count; i++)
            {
                if (QuestionNewList[i] != null)
                {
                    QuestionNewList[i].ShowQuestionsTitle();
                    for (int j = 0; j < QuestionNewList[i].ListAnswers.Count; j++)
                    {
                        if ((QuestionNewList[i].ListAnswers[j] != null) & (QuestionNewList[i].ListAnswers[j].Type == true))
                            QuestionNewList[i].ListAnswers[j].ShowAnswerOnly();
                    }

                }
            }
        }

        //Time Test
        public int AddTestTime(int NewTime)
        {
            NewTimeForTest = NewTime * 10000;

            return NewTimeForTest;
        }

        //Add Test
        public string AddTestType(string NewTestType)
        {
            TypeTestService = NewTestType;

            return TypeTestService;
        }
        public int CheckQuestionTitleInTest(string CheckTitleQuestion)
        {
            CounterForTest = 0;
            for (int i = 0; i < QuestionNewList.Count; i++)
            {
                if ((QuestionNewList[i] != null) & (Regex.IsMatch(QuestionNewList[i].Title, CheckTitleQuestion)))
                {
                    CounterForTest = 2;
                    break;
                }
            }
            if (CounterForTest == 0)
                return 0;
            else
                return CounterForTest;
        }
        public void AddQuestionInTest(string NewNewQuestionInTest, int IndexTest)
        {
            for (int i = 0; i < QuestionNewList.Count; i++)
            {
                if ((QuestionNewList[i] != null) & (Regex.IsMatch(QuestionNewList[i].Title, NewNewQuestionInTest)))
                {
                    QuestionNewListForTest.Add(QuestionNewList[i]);
                }
            }
        }
        public void CreateTest()
        {
            List<Questions> QuestionsNewListForTests = new List<Questions>(QuestionNewListForTest);
            Tests NewTest = new Tests(TypeTestService, QuestionsNewListForTests, 0, NewTimeForTest);
            TestsNewList.Add(NewTest);
            DB.TestsDB(TestsNewList);
        }
        public void ResetCounterTest()
        {
            CounterForTest = 0;
            QuestionNewListForTest.RemoveRange(0, QuestionNewListForTest.Count);
        }

        //Delete Test
        public int DeleteQuestionInTest(string NewDeleteQuestionInTest)
        {
            CheckCorrectDeleting = 0;
            for (int i = 0; i < QuestionNewListForTest.Count; i++)
            {
                if ((QuestionNewListForTest[i] != null) & (Regex.IsMatch(QuestionNewListForTest[i].Title, NewDeleteQuestionInTest)))
                {
                    QuestionNewListForTest.Remove(QuestionNewListForTest[i]);
                    CheckCorrectDeleting = 1;
                }
            }
            if (CheckCorrectDeleting == 0)
                return 0;
            else
                return 1;
        }

        //Show Tests
        public int ShowTests()
        {
            CounterForTest = 0;
            for (int i = 0; i < TestsNewList.Count; i++)
            {
                if (TestsNewList[i] != null)
                {
                    TestsNewList[i].ShowTestsTitle();
                    CounterForTest++;
                }
            }
            return CounterForTest;
        }

        //Delete DB
        public void DeleteDBQuestions()
        {
            EntityContext.DeleteDBQuestions();
            QuestionNewList = new List<Questions>();
        }
        public void DeleteDBTests()
        {
            EntityContext.DeleteDBTests();
            TestsNewList = new List<Tests>();
        }

        //Load DB
        public int LoadDBQuestions(string path)
        {
            try
            {
                List<Questions> NewDBQuestionsList = DB.LoadDBQuestions(path);
                for (int i = 0; i < NewDBQuestionsList.Count; i++)
                {
                    QuestionNewList.Add(NewDBQuestionsList[i]);
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public int LoadDBTests(string path)
        {
            try
            {
                List<Tests> NewDBTestsList = DB.LoadDBTests(path);
                for (int i = 0; i < NewDBTestsList.Count; i++)
                {
                    TestsNewList.Add(NewDBTestsList[i]);
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        //Save DB
        public void SaveDBQuestions(string path)
        {
            DB.SaveDBQuestions(QuestionNewList, path);
        }
        public void SaveDBTests(string path)
        {
               DB.SaveDBTests(TestsNewList, path);
        }

        //Search Tests
        public int SearchTest(string SearchString)
        {
            CounterForTest = 0;
            for (int i = 0; i < TestsNewList.Count; i++)
            {
                if ((TestsNewList[i] != null) & (Regex.IsMatch(TestsNewList[i].TitleTests, SearchString)))
                {
                    TestsNewList[i].ShowTests();
                    CounterForTest++;
                }
            }
            return CounterForTest;
        }

        //Delete Tests
        public int DeleteTest(string DeleteString)
        {
            CounterForTest = 0;
            for (int i = 0; i < TestsNewList.Count; i++)
            {
                if ((TestsNewList[i] != null) & (Regex.IsMatch(TestsNewList[i].TitleTests, DeleteString)))
                {
                    TestsNewList.Remove(TestsNewList[i]);
                    CounterForTest++;
                }
            }
            return CounterForTest;
        }

        //Update Test
        public int UpdateTest(string NewUpdateTests)
        {
            int Count = -1;
            for (int i = 0; i < TestsNewList.Count; i++)
            {
                if ((TestsNewList[i] != null) & (Regex.IsMatch(TestsNewList[i].TitleTests, NewUpdateTests)))
                {
                    Count = i;
                }
            }
            return Count;
        }
        public int ServiceUpdateCountQuestions(int NewCheckTest)
        {
            int Count = 0;
            for (int i = 0; i < TestsNewList[NewCheckTest].ListQuestions.Count; i++)
            {
                if (TestsNewList[NewCheckTest].ListQuestions[i] != null)
                {
                    Count++;
                }

            }
            return Count;
        }
        public void ServiceShowQuestionsForUpdateTest(int NewCheckTest)
        {
            for (int i = 0; i < TestsNewList[NewCheckTest].ListQuestions.Count; i++)
            {
                if (TestsNewList[NewCheckTest].ListQuestions[i] != null)
                {
                    TestsNewList[NewCheckTest].ListQuestions[i].ShowQuestions();
                }

            }
        }
        public int RemoveQuestionUpdateTitleInTest(string NewQuestionUpdateDelete, int NewCheckTest)
        {
            int Count = 0;
            for (int i = 0; i < TestsNewList[NewCheckTest].ListQuestions.Count; i++)
            {
                if ((TestsNewList[NewCheckTest].ListQuestions[i] != null)&(Regex.IsMatch(TestsNewList[NewCheckTest].ListQuestions[i].Title, NewQuestionUpdateDelete)))
                {
                    TestsNewList[NewCheckTest].ListQuestions.Remove(TestsNewList[NewCheckTest].ListQuestions[i]);
                    Count++;
                }

            }
            return Count;
        }
        public int AddQuestionUpdateTitleInTest(string NewQuestionUpdateAdd, int NewCheckTest)
        {
            CounterForTest = 0;
            for (int i = 0; i < QuestionNewList.Count; i++)
            {
                if ((QuestionNewList[i] != null) & (Regex.IsMatch(QuestionNewList[i].Title, NewQuestionUpdateAdd)))
                {
                    TestsNewList[NewCheckTest].ListQuestions.Add(QuestionNewList[i]);
                    CounterForTest++;
                }
            }
            return CounterForTest;
        }
        public void UpdateTime(string NewUpdateTime, int NewCheckTest)
        {
            int IndexAnswer = Convert.ToInt32(NewUpdateTime);
            TestsNewList[NewCheckTest].TimeForTest = NewCheckTest*1000;
            
        }

        //Passing Test
        public int CheckPassingTest(string NewTestPassingStart)
        {
            int Count = -1;
            for (int i = 0; i < TestsNewList.Count; i++)
            {
                if ((TestsNewList[i] != null)&((Regex.IsMatch(TestsNewList[i].TitleTests, NewTestPassingStart))))
                {
                    Count = i;
                    break;
                }
            }
            return Count;
        }
        public double StartPassingTest(string NewTestPassing, int NewCount)
        {
            TimeIsWorking = 0;
            NumberOfCorrectAnswers = 0;
            NumberOfQuestion = 0;
            AllCorrectAnswers = 0;
            for (int j = 0; j < TestsNewList[NewCount].ListQuestions.Count; j++)
            {
                if (TestsNewList[NewCount].ListQuestions[j] != null)
                {
                    AllCorrectAnswers++;
                }
            }
            return AllCorrectAnswers;
        }
        public void PassingShowQuestions(string NewTestPassing, int NewCount)
        {
            TestsNewList[NewCount].ListQuestions[NumberOfQuestion].ShowQuestions();
        }
        public int PassingTest(string NewTestPassing, int NewCount, int IndexAnswer)
        {
            if ((IndexAnswer > 0) & (IndexAnswer <= TestsNewList[NewCount].ListQuestions[NumberOfQuestion].ListAnswers.Count))
            {
                if (TestsNewList[NewCount].ListQuestions[NumberOfQuestion].ListAnswers[IndexAnswer - 1].Type == true)
                {
                    if ((TimeIsWorking == 1) | (TimeIsWorking == 2))
                        return 0;
                    else
                    {
                        NumberOfCorrectAnswers++;
                        NumberOfQuestion++;
                        return 1;
                    }
                    


                }
                else
                {
                    if ((TimeIsWorking == 1) | (TimeIsWorking == 2))
                        return 0;
                    else
                    {
                        NumberOfQuestion++;
                        return -1;
                    }
                    
                }
            }
            else
            {
                return 0;
            }
        }
        public void StartTime(string NewTestPassing, int NewCount)
        {
            NumberOfCorrectAnswers = 0;
            Thread.Sleep(TestsNewList[NewCount].TimeForTest);
            if (TimeIsWorking == 0)
            {
                Time(NewTestPassing, NewCount);
                TimeIsWorking = 2;
            }
            TimeIsWorking = 2;


        }
        public void Time(string NewTestPassing, int NewCount)
        {
            Console.WriteLine("Тест завершен!");
            TestsNewList[NewCount].NumberOfPoints = (NumberOfCorrectAnswers / AllCorrectAnswers) * 100;
            NumberOfCorrectAnswers = 0;
            TestsNewList[NewCount].ShowResults();
        }
        public void EndTimer()
        {
            TimeIsWorking = 1;
        }
    }
    
}
