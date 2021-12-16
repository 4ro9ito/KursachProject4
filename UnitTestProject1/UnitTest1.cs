using System;
using Xunit;
using DAL;
using BLL;
using PL;
using DAL.Classes;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitTestProject1
{
    public class UnitTest1 : EntityService
    {
        EntityService Test = new EntityService();
        [Fact]
        public void AddQuestionsUnitTest()
        {
            string path1 = "C:/Users/4ro9ito/source/repos/Project4/SerializeQuestionsPresets.xml";
            string AnswerTestTrue1 = "Да";
            bool BoolTestTrue1 = true;
            int CounterTestTrue1 = 1;
            string AnswerTestFalse1 = "Нет";
            bool BoolTestFalse1 = false;
            int CounterTestFalse1 = 2;

            Answers Answer1 = new Answers(AnswerTestTrue1, BoolTestTrue1, CounterTestTrue1);
            Answers Answer2 = new Answers(AnswerTestFalse1, BoolTestFalse1, CounterTestFalse1);

            string TitleTestTrue1 = "Тестовое Название 1";
            string DescriptionTestTrue1 = "Тестовое описание 1";
            List<Answers> NewAnswers = new List<Answers> { Answer1, Answer2 };

            Questions Question1 = new Questions(TitleTestTrue1, DescriptionTestTrue1, NewAnswers);

            string TitleTestTrue2 = "Тестовое Название 2";
            string DescriptionTestTrue2 = "Тестовое описание 2";

            Questions Question2 = new Questions(TitleTestTrue2, DescriptionTestTrue2, NewAnswers);

            string TitleTestTrue3 = "Тестовое Название 3";
            string DescriptionTestTrue3 = "Тестовое описание 3";

            Questions Question3 = new Questions(TitleTestTrue3, DescriptionTestTrue3, NewAnswers);

            List<Questions> QuestionForTest = new List<Questions> { Question1, Question2, Question3 };

            Test.ServiceShowAnswers();
            Test.AddQuestionType(TitleTestTrue1);
            Test.AddQuestionDescription(DescriptionTestTrue1);
            Test.AddAnswer(AnswerTestTrue1, BoolTestTrue1);
            Test.DeleteAnswer(1);
            Test.AddAnswer(AnswerTestTrue1, BoolTestTrue1);
            Test.AddAnswer(AnswerTestFalse1, BoolTestFalse1);
            Test.DeleteAnswer(2);
            Test.AddAnswer(AnswerTestFalse1, BoolTestFalse1);
            Test.AddAnswer(AnswerTestTrue1, BoolTestTrue1);
            Test.DeleteAnswer(3);
            Test.AddAnswer(AnswerTestFalse1, BoolTestFalse1);
            Test.AddAnswer(AnswerTestFalse1, BoolTestFalse1);
            Test.DeleteAnswer(3);
            Test.DeleteAnswer(3);
            Test.ServiceShowAnswers();
            Test.CreateQuestion();
            Test.ResetCounter();
            


            Test.AddQuestionType(TitleTestTrue2);
            Test.AddQuestionDescription(DescriptionTestTrue2);
            Test.AddAnswer(AnswerTestTrue1, BoolTestTrue1);
            Test.AddAnswer(AnswerTestFalse1, BoolTestFalse1);
            Test.CreateQuestion();
            Test.ResetCounter();

            Test.AddQuestionType(TitleTestTrue3);
            Test.AddQuestionDescription(DescriptionTestTrue3);
            Test.AddAnswer(AnswerTestTrue1, BoolTestTrue1);
            Test.AddAnswer(AnswerTestFalse1, BoolTestFalse1);
            Test.CreateQuestion();
            Test.ResetCounter();
            Test.ServiceShowQuestionsWithAnswers();

            var obj1Str = JsonConvert.SerializeObject(QuestionForTest);
            var obj2Str = JsonConvert.SerializeObject(Test.QuestionNewList);
            Assert.Equal(obj1Str, obj2Str);

        }

        [Fact]
        public void AddTests()
        {
            string Question1 = "Тест С Английского Языка №1 Вопрос 2";
            string Question2 = "Тест С Английского Языка №1 Вопрос 5";
            string TitleTest = "Новый тест";
            string path1 = "C:/Users/4ro9ito/source/repos/Project4/SerializeQuestionsPresets.xml";


            Test.LoadDBQuestions(path1);

            Test.AddTestType(TitleTest);
            Test.AddTestTime(100);
            Test.CheckQuestionTitleInTest(Question1);
            Test.CheckQuestionTitleInTest(Question2);


            Test.AddQuestionInTest(Test.QuestionNewList[0].Title, 0);
            Test.AddQuestionInTest(Test.QuestionNewList[1].Title, 0);
            Test.AddQuestionInTest(Test.QuestionNewList[2].Title, 0);

            Test.DeleteQuestionInTest(Test.QuestionNewList[0].Title);
            Test.DeleteQuestionInTest(Question2);
            Test.DeleteQuestionInTest(Question1);

            Test.ServiceShowQuestionsForTest();

            Test.CreateTest();
            Test.ResetCounterTest();
            Test.ShowTests();

            Assert.Equal(2, Test.TestsNewList[0].ListQuestions.Count);
        }

        [Fact]
        public void ShowQuestionsTest()
        {
            AddQuestionsUnitTest();

            Test.ServiceShowQuestions();

            Assert.Equal(3, Test.CounterForQuestions);
        }

        [Fact]
        public void UpdateQuestionsTest()
        {
            string path1 = "C:/Users/4ro9ito/source/repos/Project4/SerializeQuestionsPresets.xml";
            string Text1 = "Новый заголовок";
            string Text2 = "Новый заголовок23232323";
            string Text3 = "Новое описание";
            string TextForAnswer = "Новый ответ";
            bool ForAnswer = false;

        

            Test.LoadDBQuestions(path1);

            Test.CheckQuestion(Text2);
            Test.CheckQuestion(Test.QuestionNewList[0].Title);

            Test.UpdateTitle(Text1, 2);
            Test.UpdateDescription(Text3, 2);
            Test.UpdateAnswers(Text1, 2);
            Test.ServiceUpdateCountCorrectAnswers(2);
            Test.ServiceUpdateCountAnswers(2);
            Test.AddUpdateAnswer(TextForAnswer, ForAnswer, 2);
            Test.ServiceUpdateShowAnswers(2);
            Test.DeleteUpdateAnswer(4, 2);
            Test.DeleteUpdateAnswer(3, 2);
            Test.DeleteUpdateAnswer(1, 2);
            

            Assert.Equal(4, Test.QuestionNewList[0].ListAnswers.Count);
        }

        [Fact]
        public void PassingTestTest()
        {
            string path1 = "C:/Users/4ro9ito/source/repos/Project4/SerializeTestsPresets2.xml";

            string TitleTestTest1 = "Базовые";
            string TitleTestTest2 = "";

            Test.LoadDBTests(path1);
            
            int CounterTest = CheckPassingTest(TitleTestTest1);
            int CounterTest2 = CheckPassingTest(TitleTestTest2);
            Test.StartPassingTest(Test.TestsNewList[1].TitleTests, 0);
            Test.PassingShowQuestions(Test.TestsNewList[1].TitleTests, 0);
            Test.PassingTest(Test.TestsNewList[1].TitleTests, 0, 2);
            Test.PassingTest(Test.TestsNewList[1].TitleTests, 0, 4);
            Test.StartTime(Test.TestsNewList[1].TitleTests, 0);
            Test.Time(Test.TestsNewList[1].TitleTests, 0);
            Test.EndTimer();
            Test.PassingTest(Test.TestsNewList[1].TitleTests, 0, 2);

            Test.StartPassingTest(Test.TestsNewList[1].TitleTests, 0);
            Test.PassingShowQuestions(Test.TestsNewList[1].TitleTests, 0);
            Test.PassingTest(Test.TestsNewList[1].TitleTests, 0, 4);
            Test.PassingTest(Test.TestsNewList[1].TitleTests, 0, 5);

            Test.StartPassingTest(Test.TestsNewList[1].TitleTests, 0);
            Test.PassingShowQuestions(Test.TestsNewList[1].TitleTests, 0);
            Test.EndTimer();
            Test.PassingTest(Test.TestsNewList[1].TitleTests, 0, 4);

            Assert.Equal(CounterTest, -1);
            Assert.Equal(CounterTest2, -1);
        }

        [Fact]
        public void DeleteQuestionTest()
        {
            string TitleTestTrue1 = "Тестовое Название 1";
            AddQuestionsUnitTest();

            Test.DeleteQuestion(TitleTestTrue1);

            Assert.Equal(2, Test.QuestionNewList.Count);
        }

        [Fact]
        public void LoadDBTest()
        {
            string path1 = "C:/Users/4ro9ito/source/repos/Project4/SerializeQuestionsPresets.xml";
            string path2 = "C:/Users/4ro9ito/source/repos/Project4/SerializeTestsPresets.xml";

            Test.LoadDBQuestions(path1);
            Test.LoadDBTests(path2);

            Assert.Equal(6, Test.QuestionNewList.Count);
            Assert.Equal(2, Test.TestsNewList.Count);
        }

        [Fact]
        public void LoadDBTest2()
        {
            string path1 = "C:/Users/4ro9ito/source/repos/Project4/SerializeQuestionsPresets.xml";
            string path2 = "C:/Users/4ro9ito/source/repos/Project4/SerializeTestsPresets.xml";
            string path3 = "C:/Users/4ro9ito/source/repos/Project4/SerializeQuestionsPresets21111.xml";
            string path4 = "C:/Users/4ro9ito/source/repos/Project4/SerializeTestsPresets11112.xml";

            Test.LoadDBQuestions(path3);
            Test.LoadDBTests(path4);
            Test.LoadDBQuestions(path1);
            Test.LoadDBTests(path2);

            Test.CheckPassingTest("Тест");

            Assert.Equal(6, Test.QuestionNewList.Count);
            Assert.Equal(2, Test.TestsNewList.Count);
        }

        [Fact]
        public void SaveDBTest1()
        {
            string path1 = "C:/Users/4ro9ito/source/repos/Project4/SerializeQuestionsPresets.xml";
            string path2 = "C:/Users/4ro9ito/source/repos/Project4/SerializeTestsPresets.xml";

            string path3 = "C:/Users/4ro9ito/source/repos/Project4/SerializeSaveQuestionsPresets.xml";
            string path4 = "C:/Users/4ro9ito/source/repos/Project4/SerializeSaveTestsPresets.xml";

            string path5 = "C:/Users/4ro9ito/source/repos/Project4/SerializeSaveQuestionsPresets242424.xml";
            string path6 = "C:/Users/4ro9ito/source/repos/Project4/SerializeSaveTestsPresets242424.xml";

            Test.SaveDBQuestions(path5);
            Test.SaveDBTests(path6);

            Test.LoadDBQuestions(path1);
            Test.LoadDBTests(path2);

            Test.SaveDBQuestions(path3);
            Test.SaveDBTests(path4);

            Test.LoadDBQuestions(path3);
            Test.LoadDBTests(path4);

            Test.ShowTests();

            Test.DeleteDBQuestions();
            Test.DeleteDBTests();

            Test.LoadDBQuestions(path1);
            Test.LoadDBTests(path2);

            Assert.Equal(6, Test.QuestionNewList.Count);
            Assert.Equal(2, Test.TestsNewList.Count);
        }

        [Fact]
        public void SearchTestTest()
        {
            string path2 = "C:/Users/4ro9ito/source/repos/Project4/SerializeTestsPresets.xml";
            string Test23 = "Тест С Англ Языка";

            Test.LoadDBTests(path2);
            Test.SearchTest(Test23);
            Test.DeleteTest(Test23);
            Test.LoadDBTests(path2);

            Assert.Equal(3, Test.TestsNewList.Count);
        }

        [Fact]
        public void UpdateTests()
        {
            string path1 = "C:/Users/4ro9ito/source/repos/Project4/SerializeQuestionsPresets.xml";
            string path2 = "C:/Users/4ro9ito/source/repos/Project4/SerializeTestsPresets.xml";

            string Text = "Базовые";
            string Text2 = "Полный Оборот Земли";
            string Text3 = "Тест";
            string Text4Timer = "30000";

            Test.LoadDBQuestions(path1);
            Test.LoadDBTests(path2);

            int CounterTest1 = Test.UpdateTest(Text);
            Test.ServiceUpdateCountQuestions(0);
            Test.ServiceShowQuestionsForUpdateTest(0);
            Test.RemoveQuestionUpdateTitleInTest(Text2, 0);
            Test.AddQuestionUpdateTitleInTest(Text3, 0);
            Test.UpdateTime(Text4Timer, 0);

            Assert.Equal(5, Test.TestsNewList[0].ListQuestions.Count);


        }

        [Fact]
        public void DeleteAnswerFOr100Cover()
        {
            string path1 = "C:/Users/4ro9ito/source/repos/Project4/SerializeQuestionsPresets.xml";
            Test.LoadDBQuestions(path1);

            Test.AddAnswer(Test.QuestionNewList[0].ListAnswers[0].Answer, Test.QuestionNewList[0].ListAnswers[3].Type);
            Test.AddAnswer(Test.QuestionNewList[0].ListAnswers[0].Answer, Test.QuestionNewList[0].ListAnswers[3].Type);
            Test.AddAnswer(Test.QuestionNewList[0].ListAnswers[0].Answer, Test.QuestionNewList[0].ListAnswers[3].Type);
            Test.AddAnswer(Test.QuestionNewList[0].ListAnswers[0].Answer, Test.QuestionNewList[0].ListAnswers[3].Type);
            Test.DeleteAnswer(2);

        }
        
    }
}
