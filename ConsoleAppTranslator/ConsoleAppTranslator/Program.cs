using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Data;

namespace ConsoleAppTranslator
{
    class Program
    {
        static void WriteXml(string pathXml, List<Word> words, XmlSerializer xmlSerialaizer)
        {
            List<Word> list = new List<Word>();
            list.AddRange(words);

            using (FileStream fw = new FileStream(pathXml, FileMode.OpenOrCreate))
                xmlSerialaizer.Serialize(fw, list);
        }
        static void ReadXml(string pathXml, XmlSerializer xmlSerialaizer, string WordValue)
        {
            List<Word> WordsToRead = new List<Word>();

            using (FileStream fr = new FileStream(pathXml, FileMode.Open))
                WordsToRead = (List<Word>)xmlSerialaizer.Deserialize(fr);

            foreach (Word i in WordsToRead)
            {
                if (i.Russian == WordValue)
                    Console.WriteLine($"{i.Russian} - {i.English}");
            }
        }

        static void ReadListToKey(List<Word> Words, string key)
        {
            foreach (var item in Words)
            {
                if (item.Russian == key)
                    Console.WriteLine(item.English + " - " + item.Russian);
            }
        }

        static void Main(string[] args)
        {
            try
            {
                var Words = new List<Word>();
                Words.Add(new Word() { Russian = "Привет", English = "Hello" });
                Words.Add(new Word() { Russian = "Мир", English = "World" });
                Words.Add(new Word() { Russian = "Мир", English = "World" });

                XmlSerializer xmlSerialaizer = new XmlSerializer(typeof(List<Word>));

                string pathXml = "Translate.xml";

                string WordValue = Console.ReadLine();

                WriteXml(pathXml, Words, xmlSerialaizer);

                Console.WriteLine(" READ FILE .xml:\n");
                ReadXml(pathXml, xmlSerialaizer, WordValue);

                Console.WriteLine("--------");
                ReadListToKey(Words, WordValue);
                Console.WriteLine("what type Translate: ");
                byte count = byte.Parse(Console.ReadLine());

                do
                {
                    switch (count)
                    {
                        case 1:
                            {
                                string key = Console.ReadLine();
                                foreach (var item in Words)
                                {
                                    if (item.Russian == key)
                                        Console.WriteLine(item.English);
                                }
                            }
                            break;
                        case 2:
                            {
                                string key = Console.ReadLine();
                                foreach (var item in Words)
                                {
                                    if (item.English == key)
                                        Console.WriteLine(item.Russian);
                                }
                            }
                            break;
                        case 3:
                            {
                                Console.WriteLine("Add rus: ");
                                string AddWordRus = Console.ReadLine();
                                Console.WriteLine("Add eng: ");
                                string AddWordEng = Console.ReadLine();

                                Words.Add(new Word() { Russian = AddWordRus, English = AddWordEng });
                                WriteXml(pathXml, Words, xmlSerialaizer);
                                Console.WriteLine(" READ FILE .xml:\n");
                                 ReadXml(pathXml, xmlSerialaizer, WordValue);
                            }
                            break;
                        case 4:
                            {

                            }
                            break;
                    }                 

                } while (count != 0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
