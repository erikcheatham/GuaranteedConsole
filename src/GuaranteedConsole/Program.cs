using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuaranteedConsole.Entities;
using System.IO;
using System.Text;
using System.ComponentModel;

namespace GuaranteedConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string basePath = AppContext.BaseDirectory;
            string pipFile = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\TextFiles\PipeDelimited.txt"));
            string comFile = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\TextFiles\CommaDelimited.txt"));
            string spaceFile = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\TextFiles\SpaceDelimited.txt"));

            string[] pipes = File.ReadAllLines(pipFile);
            string[] commas = File.ReadAllLines(comFile);
            string[] spaces = File.ReadAllLines(spaceFile);

            List<Person> PipeList = ParseFiles(pipes, '|');
            List<Person> CommaList = ParseFiles(commas, ',');
            List<Person> SpaceList = ParseFiles(spaces, ' ');

            PipeList.SortByGender();
            CommaList.SortByBirthDate();
            SpaceList.SortByLastName();

            ShowOutput(PipeList, "Output 1 - Sorted By Gender");
            ShowOutput(CommaList, "Output 2 - Sorted By Birth Date Ascending");
            ShowOutput(SpaceList, "Output 3 - Sorted By Last Name Descending");
            Console.ReadLine();
        }

        public static List<Person> ParseFiles(string[] file, char type)
        {
            List<Person> List = new List<Person>();
            int header = 0;

            switch (type)
            {
                case '|':
                    {
                        foreach (string pipeLine in file)
                        {
                            Person p = new Person();
                            if (header != 0)
                            {
                                string[] fields = pipeLine.Split('|');
                                p.LastName = fields[0];
                                p.FirstName = fields[1];
                                p.Gender = fields[2];
                                p.FavoriteColor = fields[3];
                                p.DateOfBirth = fields[4];
                                List.Add(p);
                            }
                            header++;
                        }
                        break;
                    }
                case ',':
                    {
                        foreach (string commaLine in file)
                        {
                            Person p = new Person();
                            if (header != 0)
                            {
                                string[] fields = commaLine.Split(',');
                                p.LastName = fields[0];
                                p.FirstName = fields[1];
                                p.Gender = fields[2];
                                p.FavoriteColor = fields[3];
                                p.DateOfBirth = fields[4];
                                List.Add(p);
                            }
                            header++;
                        }
                        break;
                    }
                case ' ':
                    {
                        foreach (string spaceLine in file)
                        {
                            Person p = new Person();
                            if (header != 0)
                            {
                                string[] fields = spaceLine.Split(' ');
                                p.LastName = fields[0];
                                p.FirstName = fields[1];
                                p.Gender = fields[2];
                                p.FavoriteColor = fields[3];
                                p.DateOfBirth = fields[4];
                                List.Add(p);
                            }
                            header++;
                        }
                        break;
                    }
                default:
                    return null;
            }
            return List;
        }


        public static void ShowOutput(List<Person> person, string type)
        {
            Console.WriteLine();
            Console.WriteLine(type);
            foreach (Person p in person)
            {
                ////Print Headers
                //foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(p))
                //{
                //    string name = descriptor.Name;
                //    Console.Write(name);
                //}
                ////Print Line Break
                //Console.WriteLine();
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(p))
                {
                    object value = descriptor.GetValue(p);
                    Console.Write(value + "     ");
                }
                Console.WriteLine();
            }
        }
    }

    public static class ExtensionMethods
    {
        public static List<Person> SortByGender(this List<Person> p)
        {
            if (p.Any())
            {
                p.Sort((p1, p2) => p1.Gender.CompareTo(p2.Gender));
            }
            return p;
        }

        public static List<Person> SortByBirthDate(this List<Person> p)
        {
            if (p.Any())
            {
                p.Sort((p1, p2) => p1.DateOfBirth.CompareTo(p2.DateOfBirth));
            }
            return p;
        }

        public static List<Person> SortByLastName(this List<Person> p)
        {
            if (p.Any())
            {
                p.Sort((p1, p2) => -1 * p1.LastName.CompareTo(p2.LastName));
            }
            return p;
        }
    }
}