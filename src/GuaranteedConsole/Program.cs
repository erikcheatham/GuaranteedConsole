using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuaranteedConsole.Entities;
using System.IO;
using System.Text;

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
    }
}