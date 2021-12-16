using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using BLL;


namespace PL
{
    internal class Menu
    {
        EntityService Service = new EntityService();
        bool BoolString;
        int CounterCheck = 0;
        int AmountOfAnswers = 0;
        int AmountOfQuestions = 0;
        int Test = 0;
        int MenuNumberOfQuestion = 0;
        double MenuAllCorrectAnswers;

        public void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("|Управление тестами: введите \"0\"");
                Console.WriteLine("|Управление вопросами: введите \"1\"");
                Console.WriteLine("|Управление БД: введите \"2\"");
                Console.WriteLine("|Завершить программу: нажмите энтер");
                Console.WriteLine("--------------------------------------");
                string Answer = Console.ReadLine();
                if (Answer == "0")
                {
                    while (true)
                    {
                        Console.WriteLine("--------------------------------------");
                        Console.WriteLine("|Показать все тесты: введите \"0\"");
                        Console.WriteLine("|Найти тест: введите \"1\"");
                        Console.WriteLine("|Добавить тест: введите \"2\"");
                        Console.WriteLine("|Удалить тест: введите \"3\"");
                        Console.WriteLine("|Обновить тест: введите \"4\"");
                        Console.WriteLine("|Пройти какой-либо тест: введите \"5\"");
                        Console.WriteLine("|Вернуться в меню: нажмите энтер");
                        Console.WriteLine("--------------------------------------");
                        string Answer2 = Console.ReadLine();
                        if (Answer2 == "0")
                        {
                            int CounterTests = Service.ShowTests();
                            Console.WriteLine($"Всего тестов: {CounterTests}");
                            continue;
                        }
                        if (Answer2 == "1")
                        {
                            Console.WriteLine("Введите поисковой запрос(по названию тестов):");
                            string AnswerSearch = Console.ReadLine();
                            int CounterTests = Service.SearchTest(AnswerSearch);
                            Console.WriteLine($"Всего найдено {CounterTests} тест(ов)");
                            continue;
                        }
                        if (Answer2 == "2")
                        {
                            Console.WriteLine("Введите название теста:");
                            string TestName = Console.ReadLine();
                            Service.AddTestType(TestName);
                            Console.WriteLine("Время на выполнение теста(в секундах):");
                            string TestTime = Console.ReadLine();
                            try
                            {
                                int TimeConvert = Convert.ToInt32(TestTime);
                                if (TimeConvert > 0)
                                {
                                    Service.AddTestTime(TimeConvert);

                                    Console.WriteLine("Время добавлено!");
                                    AmountOfAnswers--;
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Ошибка! Невозможное время!");
                                continue;
                            }
                                
                                Console.WriteLine("Добавление вопросов...");
                            while (true)
                            {
                                Console.WriteLine("");
                                Console.WriteLine("|Добавить вопрос: введите 0");
                                Console.WriteLine("|Удалить добавленный вопрос: введите 1");
                                Console.WriteLine("|Продолжить: введите 2");
                                Console.WriteLine("|Посмотреть все вопросы: введите 3");
                                Console.WriteLine("|Отмена: введите энтер");
                                string TestAnswer = Console.ReadLine();
                                if (TestAnswer == "0")
                                {
                                    Console.WriteLine("Введите название вопроса из БД:");
                                    string NewQuestionInTest = Console.ReadLine();
                                    int CheckCorrectQuestion = Service.CheckQuestionTitleInTest(NewQuestionInTest);
                                    if (CheckCorrectQuestion != 0)
                                    {
                                        Service.AddQuestionInTest(NewQuestionInTest, Test);
                                        Console.WriteLine("Вопрос успешно добавлен!");
                                        AmountOfQuestions++;

                                    }
                                    else 
                                        Console.WriteLine("Вопрос не найден!");
                                    continue;
                                }
                                if (TestAnswer == "1")
                                {
                                    Console.WriteLine("Введите полное название вопроса, что имеется в тесте:");
                                    string DeleteQuestionInTest = Console.ReadLine();
                                    int DeleteQuestionInTestIndex = Service.DeleteQuestionInTest(DeleteQuestionInTest);
                                    if (DeleteQuestionInTestIndex == 0)
                                    {
                                        Console.WriteLine("Вопрос не найден!");
                                        continue;
                                    }
                                    else
                                    { 
                                        Console.WriteLine("Вопрос успешно удален!");
                                        AmountOfQuestions--;
                                        continue;
                                    }
                                }
                                if (TestAnswer == "2")
                                {
                                    if (AmountOfQuestions > 0)
                                    {
                                        Service.CreateTest();
                                        Console.WriteLine("Тест успешно добавлен!");
                                        Test++;
                                        Service.ResetCounterTest();
                                        break;
                                        
                                    }
                                    else if (AmountOfQuestions <= 0)
                                    {
                                        Console.WriteLine($"Ошибка! У вас всего {AmountOfAnswers} вопрос(ов)!");
                                        continue;
                                    }

                                   
                                }
                                if (TestAnswer == "3")
                                {
                                    Service.ServiceShowQuestionsForTest();
                                    continue;
                                }
                                AmountOfQuestions = 0;
                                break;
                            }
                            

                        }
                        if (Answer2 == "3")
                        {
                            Console.WriteLine("Введите полное название теста, чтобы его удалить");
                            string AnswerDelete = Console.ReadLine();
                            int CounterTests = Service.DeleteTest(AnswerDelete);
                            if (CounterTests == 0)
                            {
                                Console.WriteLine("Не найдено!");
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Тест успешно удален!");
                                continue;
                            }
                        }
                        if (Answer2 == "4")
                        {
                            Console.WriteLine("Введите полное название теста, чтобы его изменить");
                            string UpdateTest = Console.ReadLine();
                            int CheckTest = Service.UpdateTest(UpdateTest);
                            if (CheckTest != -1)
                            {
                                AmountOfQuestions = Service.ServiceUpdateCountQuestions(CheckTest);
                                while (true)
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine("|Добавить вопрос: введите 0");
                                    Console.WriteLine("|Удалить добавленный вопрос: введите 1");
                                    Console.WriteLine("|Продолжить: введите 2");
                                    Console.WriteLine("|Посмотреть все вопросы: введите 3");
                                    Console.WriteLine("|Изменить время, уделенное на тест: введите 4");
                                    Console.WriteLine("|Отмена: введите энтер");
                                    string TestAnswer = Console.ReadLine();
                                    if (TestAnswer == "0")
                                    {
                                        Console.WriteLine("Введите название вопроса из БД:");
                                        string NewQuestionInTest = Console.ReadLine();
                                        string[] s2 = NewQuestionInTest.Split(' ');
                                        for (int i = 0; i < s2.Length; i++)
                                        {
                                            if (s2[i].Length > 1)
                                                s2[i] = s2[i].Substring(0, 1).ToUpper() + s2[i].Substring(1, s2[i].Length - 1).ToLower();
                                            else
                                                s2[i] = s2[i].ToUpper();
                                        }
                                        NewQuestionInTest = String.Join(" ", s2);
                                        int CheckCorrectQuestion = Service.AddQuestionUpdateTitleInTest(NewQuestionInTest, CheckTest);
                                        if (CheckCorrectQuestion != 0)
                                        {
                                            Service.AddQuestionInTest(NewQuestionInTest, Test);
                                            Console.WriteLine("Вопрос успешно добавлен!");
                                            AmountOfQuestions++;

                                        }
                                        else
                                            Console.WriteLine("Вопрос не найден!");
                                        continue;
                                    }
                                    if (TestAnswer == "1")
                                    {
                                        Console.WriteLine("Введите полное название вопроса, что имеется в тесте:");
                                        string DeleteQuestionInTest = Console.ReadLine();
                                        int DeleteQuestionInTestIndex = Service.RemoveQuestionUpdateTitleInTest(DeleteQuestionInTest, CheckTest);
                                        if (DeleteQuestionInTestIndex == 0)
                                        {
                                            Console.WriteLine("Вопрос не найден!");
                                            continue;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Вопрос успешно удален!");
                                            AmountOfQuestions--;
                                            continue;
                                        }
                                    }
                                    if (TestAnswer == "2")
                                    {
                                        if (AmountOfQuestions > 0)
                                        {
                                            Console.WriteLine("Тест успешно изменен!");
                                            Service.ResetCounterTest();
                                            break;

                                        }
                                        else if (AmountOfQuestions <= 0)
                                        {
                                            Console.WriteLine($"Ошибка! У вас всего {AmountOfAnswers} вопрос(ов)!");
                                            continue;
                                        }


                                    }
                                    if (TestAnswer == "3")
                                    {
                                        Service.ServiceShowQuestionsForUpdateTest(CheckTest);
                                        continue;
                                    }
                                    if (TestAnswer == "4")
                                    {
                                        Console.WriteLine("Введите время в секундах:");
                                        string UpdateTime = Console.ReadLine();
                                        string patternOfTypeOfQuestion = @"([^0-9])";
                                        if ((Regex.IsMatch(UpdateTime, patternOfTypeOfQuestion)))
                                        {
                                            Console.WriteLine("Неправильный формат!");
                                            continue;
                                        }
                                        else
                                        {
                                            Service.UpdateTime(UpdateTime, CheckTest);
                                            break;
                                        }
                                    }
                                    AmountOfQuestions = 0;
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Не найдено!");
                                continue;
                            }

                        }
                        if (Answer2 == "5")
                        {
                            Console.WriteLine("Выберите тест для прохождения(Введите полное его название, чтобы начать прохождение):");
                            string TestPassingStart = Console.ReadLine();
                            int CheckPassing = Service.CheckPassingTest(TestPassingStart);
                            if (CheckPassing == -1)
                            {
                                Console.WriteLine("Ошибка: тест не найден!");
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Старт");
                                MenuNumberOfQuestion = 0;
                                MenuAllCorrectAnswers = 0;
                                Task task = Task.Factory.StartNew(() => Service.StartTime(TestPassingStart, CheckPassing));
                                MenuAllCorrectAnswers = Service.StartPassingTest(TestPassingStart, CheckPassing);
                                while (true)
                                {
                                    if (MenuNumberOfQuestion < MenuAllCorrectAnswers)
                                    {
                                        while (true)
                                        {
                                            Service.PassingShowQuestions(TestPassingStart, CheckPassing);
                                            Console.WriteLine("Ваш ответ?(Чтобы выйти, введите 0. Внимание: при окончании времени на тест следующие ответы засчитываться НЕ будут!)");
                                            string AnswerPassing = Console.ReadLine();
                                            if (AnswerPassing == "0")
                                            {
                                                MainMenu();
                                            }
                                            try
                                            {
                                                int IndexAnswer = Convert.ToInt16(AnswerPassing);
                                                int Exceptions = Service.PassingTest(TestPassingStart, CheckPassing, IndexAnswer);
                                                if (Exceptions == 0)
                                                {
                                                    Console.WriteLine("Ошибка! Невозможный индекс!");
                                                    continue;
                                                }
                                                if (Exceptions == 1)
                                                {
                                                    Console.WriteLine("Правильно!");
                                                    MenuNumberOfQuestion++;
                                                    break;
                                                }
                                                if (Exceptions ==  -1)
                                                {
                                                    Console.WriteLine("Ответ неверный!");
                                                    MenuNumberOfQuestion++;
                                                    break;
                                                }

                                            }
                                            catch
                                            {
                                                Console.WriteLine("Ошибка! Должны быть только целочисленные значения!");
                                                continue;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Конец теста!");
                                        Service.EndTimer();
                                        Service.Time(TestPassingStart, CheckPassing);
                                        break;
                                    }
                                }   
                            }
                        }
                        break;
                    }
                    continue;
                }
                if (Answer == "1")
                {
                    while (true)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("--------------------------------------");
                        Console.WriteLine("|Показать все вопросы: введите \"0\"");
                        Console.WriteLine("|Добавить вопрос: введите \"1\"");
                        Console.WriteLine("|Удалить вопрос: введите \"2\"");
                        Console.WriteLine("|Обновить вопрос: введите \"3\"");
                        Console.WriteLine("|Посмотреть ответы: введите \"4\"");
                        Console.WriteLine("|Вернуться в меню: нажмите энтер");
                        Console.WriteLine("--------------------------------------");
                        string Answer3 = Console.ReadLine();
                        if (Answer3 == "0")
                        {
                            int CounterQuestions = Service.ServiceShowQuestions();
                            Console.WriteLine($"Всего найдено {CounterQuestions}");
                            continue;
                        }
                        if (Answer3 == "1")
                        {
                            while (true)
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Название вопроса(Делайте уникальным!)(Чтобы отменить процесс - введите 0):");
                                string TypeOfQuestion = Console.ReadLine();
                                string patternOfTypeOfQuestion = @"([^1-9])";
                                string[] s1 = TypeOfQuestion.Split(' ');
                                for (int i = 0; i < s1.Length; i++)
                                {
                                    if (s1[i].Length > 1)
                                        s1[i] = s1[i].Substring(0, 1).ToUpper() + s1[i].Substring(1, s1[i].Length - 1).ToLower();
                                    else
                                        s1[i] = s1[i].ToUpper();
                                }
                                TypeOfQuestion = String.Join(" ", s1);
                                if (!(Regex.IsMatch(TypeOfQuestion, patternOfTypeOfQuestion)))
                                {
                                    Console.WriteLine("Неправильный формат!");
                                    continue;
                                }
                                else if (TypeOfQuestion == "0")
                                    MainMenu();
                                else
                                {
                                    Service.AddQuestionType(TypeOfQuestion);
                                    break;
                                }
                            }
                            Console.WriteLine("");
                            Console.WriteLine("Описание вопроса");
                            string DescriptionOfQuestion = Console.ReadLine();
                            Service.AddQuestionDescription(DescriptionOfQuestion);
                            AnswerOptions();
                        }
                        if (Answer3 == "2")
                        {
                            Console.WriteLine("Введите название вопроса(полностью)");
                            string DeleteQuestion = Console.ReadLine();
                            Service.DeleteQuestion(DeleteQuestion);
                        }
                        if (Answer3 == "3")
                        {
                            Console.WriteLine("Введите название обновляемого вопроса");
                            string UpdateQuestion = Console.ReadLine();
                            int CheckUpdateQuestion = Service.CheckQuestion(UpdateQuestion);
                            if (CheckUpdateQuestion != -1)
                            {
                                Console.WriteLine("Что желаете обновить?");
                                Console.WriteLine("|Заголовок: введите \"0\"");
                                Console.WriteLine("|Описание: введите \"1\"");
                                Console.WriteLine("|Ответы: введите \"2\"");
                                Console.WriteLine("|Отмена: введите энтер");
                                string UpdateQuestion2 = Console.ReadLine();
                                if (UpdateQuestion2 == "0")
                                {
                                    Console.WriteLine("Введите название заголовка:");
                                    string UpdateTitle = Console.ReadLine();
                                    Service.UpdateTitle(UpdateTitle, CheckUpdateQuestion);
                                    Console.WriteLine("Успех!");
                                }
                                if (UpdateQuestion2 == "1")
                                if (UpdateQuestion2 == "1")
                                {
                                    Console.WriteLine("Введите описание вопроса:");
                                    string UpdateDescription = Console.ReadLine();
                                    Service.UpdateDescription(UpdateDescription, CheckUpdateQuestion);
                                    Console.WriteLine("Успех!");
                                }
                                if (UpdateQuestion2 == "2")
                                {

                                    Console.WriteLine("");
                                    Console.WriteLine("Добавление ответов... (Внимание! Должен быть хотя бы 1 правильный ответ. Минимальное количество ответов на вопрос: 2).");
                                    CounterCheck = Service.ServiceUpdateCountCorrectAnswers(CheckUpdateQuestion);
                                    AmountOfAnswers = Service.ServiceUpdateCountAnswers(CheckUpdateQuestion);
                                    while (true)
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine("|Добавить новый ответ: введите 0");
                                        Console.WriteLine("|Удалить добавленный ответ: введите 1");
                                        Console.WriteLine("|Продолжить: введите 2");
                                        Console.WriteLine("|Посмотреть все ответы: введите 3");
                                        
                                        string Answer4 = Console.ReadLine();
                                        if (Answer4 == "0")
                                        {
                                            while (true)
                                            {
                                                Console.WriteLine("Ложный ответ - 0, Правильный ответ - 1:");
                                                string StringBoolAnswer = Console.ReadLine();
                                                if (StringBoolAnswer == "0")
                                                {
                                                    BoolString = false;
                                                    break;
                                                }
                                                if (StringBoolAnswer == "1")
                                                {
                                                    BoolString = true;
                                                    CounterCheck++;
                                                    break;
                                                }
                                                Console.WriteLine("Ошибка!");
                                                continue;

                                            }
                                            Console.WriteLine("Введите ответ:");
                                            string CreateAnswer = Console.ReadLine();
                                            Service.AddUpdateAnswer(CreateAnswer, BoolString, CheckUpdateQuestion);
                                            Console.WriteLine("Ответ успешно добавлен!");
                                            AmountOfAnswers++;
                                            continue;

                                        }
                                        if (Answer4 == "1")
                                        {
                                            if (AmountOfAnswers > 0)
                                            {
                                                Service.ServiceUpdateShowAnswers(CheckUpdateQuestion);
                                                Console.WriteLine("Выберите номер ответа:");
                                                string StringIndexAnswer = Console.ReadLine();
                                                try
                                                {
                                                    int IndexAnswer = Convert.ToInt32(StringIndexAnswer);
                                                    if ((IndexAnswer > 0) & (IndexAnswer <= AmountOfAnswers))
                                                    {
                                                        int IndexForCorrectAnswer = Service.DeleteUpdateAnswer(IndexAnswer, CheckUpdateQuestion);
                                                        if (IndexForCorrectAnswer == -1)
                                                        {
                                                            CounterCheck--;
                                                        }


                                                        Console.WriteLine("Ответ успешно удален!");
                                                        AmountOfAnswers--;
                                                        continue;
                                                    }

                                                    Console.WriteLine("Ошибка! Невозможный индекс!");

                                                }
                                                catch
                                                {
                                                    Console.WriteLine("Ошибка! Должны быть только целочисленные значения!");
                                                }

                                                continue;
                                            }
                                            if (AmountOfAnswers <= 0)
                                            {
                                                Console.WriteLine("У вас пока нет добавленных ответов!");
                                                continue;
                                            }
                                        }
                                        if (Answer4 == "2")
                                        {
                                            if (CounterCheck > 0 && AmountOfAnswers > 1)
                                            {
                                                Console.WriteLine("Вопрос изменен!");
                                                CounterCheck = 0;
                                                AmountOfAnswers = 0;
                                                Service.ResetCounter();
                                                break;
                                            }
                                            else if (CounterCheck <= 0)
                                            {
                                                Console.WriteLine($"Ошибка! У вас {CounterCheck} правильных ответов!");
                                                continue;
                                            }
                                            else if (AmountOfAnswers <= 1)
                                            {
                                                Console.WriteLine($"Ошибка! У вас всего {AmountOfAnswers} ответ(ов)!");
                                                continue;
                                            }
                                        }
                                        if (Answer4 == "3")
                                        {
                                            if (AmountOfAnswers > 0)
                                            {
                                                Service.ServiceUpdateShowAnswers(CheckUpdateQuestion);
                                                continue;
                                            }
                                            if (AmountOfAnswers <= 0)
                                            {
                                                Console.WriteLine("У вас пока нет добавленных ответов!");
                                                continue;
                                            }
                                        }
                                        
                                        continue;

                                    }
                                }
                                continue;
                            }
                            else
                                Console.WriteLine("Вопрос не найден!");

                            
                        }
                        if (Answer3 == "4")
                        {
                            Service.ServiceShowQuestionsWithAnswers();
                            continue;
                        }
                        break;
                    }
                    continue;
                }
                if (Answer == "2")
                {
                    while (true)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("--------------------------------------");
                        Console.WriteLine("|Загрузить БД вопросов: введите \"1\"");
                        Console.WriteLine("|Сохранить БД вопросов: введите \"2\"");
                        Console.WriteLine("|Очистить БД вопросов: введите \"3\"");
                        Console.WriteLine("|Загрузить БД тестов: введите \"4\"");
                        Console.WriteLine("|Сохранить БД тестов: введите \"5\"");
                        Console.WriteLine("|Очистить БД тестов: введите \"6\"");
                        Console.WriteLine("|Вернуться в меню: нажмите энтер");
                        Console.WriteLine("--------------------------------------");
                        string AnswerDB = Console.ReadLine();
                        if (AnswerDB == "1")
                        {
                            Console.WriteLine("Укажите ПОЛНУЮ ссылку на файл XML");
                            string ReferenceLoad = Console.ReadLine();
                            int CORRECTPATH = Service.LoadDBQuestions(ReferenceLoad);
                            if (CORRECTPATH == 1)
                                Console.WriteLine("Операция выполнена!");
                            if (CORRECTPATH == 0)
                                Console.WriteLine("Ошибка: файл не найден!");
                            break;
                        }
                        if (AnswerDB == "2")
                        {
                            Console.WriteLine("Укажите ПОЛНЫЙ путь для сохранения готовых вопросов");
                            string ReferenceSaveQuestions = Console.ReadLine();
                            Service.SaveDBQuestions(ReferenceSaveQuestions);
                            break;
                        }
                        if (AnswerDB == "3")
                        {
                            Service.DeleteDBQuestions();
                            Console.WriteLine("Операция выполнена!");
                            break;
                        }
                        if (AnswerDB == "4")
                        {
                            Console.WriteLine("Укажите ПОЛНУЮ ссылку на файл XML");
                            string ReferenceSave = Console.ReadLine();
                            int CORRECTPATH = Service.LoadDBTests(ReferenceSave);
                            if (CORRECTPATH == 1)
                                Console.WriteLine("Операция выполнена!");
                            if (CORRECTPATH == 0)
                                Console.WriteLine("Ошибка: файл не найден!");
                            break;
                        }
                        if (AnswerDB == "5")
                        {
                            Console.WriteLine("Укажите ПОЛНЫЙ путь для сохранения готовых тестов");
                            string ReferenceSaveTests = Console.ReadLine();
                            Service.SaveDBTests(ReferenceSaveTests);
                            break;
                        }
                        if (AnswerDB == "6")
                        {
                            Service.DeleteDBTests();
                            Console.WriteLine("Операция выполнена!");
                        }
                        break;
                    }
                    continue;
                }
                break;
            }
        }
        public void AnswerOptions()
        {
            Console.WriteLine("");
            Console.WriteLine("Добавление ответов... (Внимание! Должен быть хотя бы 1 правильный ответ. Минимальное количество ответов на вопрос: 2).");
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("|Добавить новый ответ: введите 0");
                Console.WriteLine("|Удалить добавленный ответ: введите 1");
                Console.WriteLine("|Продолжить: введите 2");
                Console.WriteLine("|Посмотреть все ответы: введите 3");
                Console.WriteLine("|Отмена: введите энтер");
                string Answer4 = Console.ReadLine();
                if (Answer4 == "0")
                {
                    while (true)
                    {
                        Console.WriteLine("Ложный ответ - 0, Правильный ответ - 1:");
                        string StringBoolAnswer = Console.ReadLine();
                        if (StringBoolAnswer == "0")
                        {
                            BoolString = false;
                            break;
                        }
                        if (StringBoolAnswer == "1")
                        {
                            BoolString = true;
                            CounterCheck++;
                            break;
                        }
                        Console.WriteLine("Ошибка!");
                        continue;

                    }
                    Console.WriteLine("Введите ответ:");
                    string CreateAnswer = Console.ReadLine();
                    Service.AddAnswer(CreateAnswer, BoolString);
                    Console.WriteLine("Ответ успешно добавлен!");
                    AmountOfAnswers++;
                    continue;

                }
                if (Answer4 == "1")
                {
                    if (AmountOfAnswers > 0)
                    {
                        Service.ServiceShowAnswers();
                        Console.WriteLine("Выберите номер ответа:");
                        string StringIndexAnswer = Console.ReadLine();
                        try
                        {
                            int IndexAnswer = Convert.ToInt32(StringIndexAnswer);
                            if ((IndexAnswer > 0) & (IndexAnswer <= AmountOfAnswers))
                            {
                                int IndexForCorrectAnswer = Service.DeleteAnswer(IndexAnswer);
                                if (IndexForCorrectAnswer == -1)
                                {
                                    CounterCheck--;
                                }


                                Console.WriteLine("Ответ успешно удален!");
                                continue;
                            }

                            Console.WriteLine("Ошибка! Невозможный индекс!");

                        }
                        catch
                        {
                            Console.WriteLine("Ошибка! Должны быть только целочисленные значения!");
                        }

                        continue;
                    }
                    if (AmountOfAnswers <= 0)
                    {
                        Console.WriteLine("У вас пока нет добавленных ответов!");
                        continue;
                    }
                }
                if (Answer4 == "2")
                {
                    if (CounterCheck > 0 && AmountOfAnswers > 1)
                    {
                        Service.CreateQuestion();
                        Console.WriteLine("Вопрос добавлен!");
                        Service.ResetCounter();
                    }
                    else if (CounterCheck <= 0)
                    {
                        Console.WriteLine($"Ошибка! У вас {CounterCheck} правильных ответов!");
                        continue;
                    }
                    else if (AmountOfAnswers <= 1)
                    {
                        Console.WriteLine($"Ошибка! У вас всего {AmountOfAnswers} ответ(ов)!");
                        continue;
                    }
                }
                if (Answer4 == "3")
                {
                    if (AmountOfAnswers > 0)
                    {
                        Service.ServiceShowAnswers();
                        continue;
                    }
                    if (AmountOfAnswers <= 0)
                    {
                        Console.WriteLine("У вас пока нет добавленных ответов!");
                        continue;
                    }
                }
                CounterCheck = 0;
                AmountOfAnswers = 0;
                break;

            }
        }
        
    }
}
