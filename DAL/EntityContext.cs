using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Classes;
using System.Xml.Serialization;
using System.IO;

namespace DAL
{
    public class EntityContext
    {
        XmlSerializer formatterXml = new XmlSerializer(typeof(List<Questions>));
        XmlSerializer formatterXml2 = new XmlSerializer(typeof(List<Tests>));

        public void QuestionsDB(List<Questions> QuestionToDb)
        {
            using (FileStream fs = new FileStream("C:/Users/4ro9ito/source/repos/Project4/SerializeQuestions.xml", FileMode.OpenOrCreate))
            {
                formatterXml.Serialize(fs, QuestionToDb);
            }
        }
        public void TestsDB(List<Tests> TestsToDb)
        {
            using (FileStream fs = new FileStream("C:/Users/4ro9ito/source/repos/Project4/SerializeTests.xml", FileMode.OpenOrCreate))
            {
                formatterXml2.Serialize(fs, TestsToDb);
            }
        }

        public static void DeleteDBQuestions()
        {
            XmlSerializer formatterXml = new XmlSerializer(typeof(List<Questions>));
            using (FileStream fs = new FileStream("C:/Users/4ro9ito/source/repos/Lab3Chapt2/SerializeQuestions.xml", FileMode.Create))
            {

            }
        }
        public static void DeleteDBTests()
        {
            XmlSerializer formatterXml = new XmlSerializer(typeof(List<Tests>));
            using (FileStream fs = new FileStream("C:/Users/4ro9ito/source/repos/Lab3Chapt2/SerializeTests.xml", FileMode.Create))
            {

            }
        }

        public List<Questions> LoadDBQuestions(string path)
        {
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    try
                    {
                        List<Questions> List = (List<Questions>)formatterXml.Deserialize(fs);
                        for (int i = 0; i < List.Count; i++)
                        {
                            List[i].ShowQuestions();

                        }
                        return List;
                    }
                    catch
                    {

                    }
                }
            }
            return null;
        }
        public List<Tests> LoadDBTests(string path)
        {
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    try
                    {
                        List<Tests> List = (List<Tests>)formatterXml2.Deserialize(fs);
                        for (int i = 0; i < List.Count; i++)
                        {
                            List[i].ShowTests();

                        }
                        return List;
                    }
                    catch
                    {

                    }
                }
            }
            return null;
        }

        public List<Questions> SaveDBQuestions(List<Questions> QuestionToDb, string path)
        {
            using (FileStream fs1 = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatterXml.Serialize(fs1, QuestionToDb);
            }
            return null;
        }
        public List<Tests> SaveDBTests(List<Tests> TestsToDb, string path)
        {
            using (FileStream fs1 = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatterXml2.Serialize(fs1, TestsToDb);
            }
            return null;
        }
    }
}
