using GuaranteedConsole.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace GuaranteedConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Person> PipeList = RunProgram.Run("PipeDelimited", "gender", "asc", '|');
            List<Person> CommaList = RunProgram.Run("CommaDelimited", "birth", "asc", ',');
            List<Person> SpaceList = RunProgram.Run("SpaceDelimited", "lastname", "desc", ' ');
            
            RunProgram.ShowOutput(PipeList, "Output 1 - Sorted By Gender");
            RunProgram.ShowOutput(CommaList, "Output 2 - Sorted By Birth Date Ascending");
            RunProgram.ShowOutput(SpaceList, "Output 3 - Sorted By Last Name Descending");
            Console.ReadLine();
        }
    }

    public static class RunProgram
    {
        public static List<Person> Run(string file, string sort, string order, char delimiter)
        {
            string basePath = AppContext.BaseDirectory;
            string document = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\TextFiles\" + file + ".txt"));
            string[] fileArr = File.ReadAllLines(document);
            List<Person> documentList = ParseFiles(fileArr, delimiter);

            switch (sort)
            {
                case "gender":
                    {
                        return documentList.SortByGender(order);
                    }
                case "birth":
                    {
                        return documentList.SortByBirthDate(order);
                    }
                case "lastname":
                    {
                        return documentList.SortByLastName(order);
                    }
                default:
                    return null;
            }
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
        public static List<Person> SortByGender(this List<Person> p, string order)
        {
            if (p.Any())
            {
                if (order == "asc")
                {
                    p.Sort((p1, p2) => p1.Gender.CompareTo(p2.Gender));
                }
                else if (order == "desc")
                {
                    p.Sort((p1, p2) => -1 * p1.Gender.CompareTo(p2.Gender));
                }

            }
            return p;
        }

        public static List<Person> SortByBirthDate(this List<Person> p, string order)
        {
            if (p.Any())
            {
                if (order == "asc")
                {
                    p.Sort((p1, p2) => p1.DateOfBirth.CompareTo(p2.DateOfBirth));
                }
                else if (order == "desc")
                {
                    p.Sort((p1, p2) => -1 * p1.DateOfBirth.CompareTo(p2.DateOfBirth));
                }
            }
            return p;
        }

        public static List<Person> SortByLastName(this List<Person> p, string order)
        {
            if (p.Any())
            {
                if (order == "asc")
                {
                    p.Sort((p1, p2) => p1.LastName.CompareTo(p2.LastName));
                }
                else if (order == "desc")
                {
                    p.Sort((p1, p2) => -1 * p1.LastName.CompareTo(p2.LastName));
                }
            }
            return p;
        }
    }
}