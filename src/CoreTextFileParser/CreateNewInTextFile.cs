using CoreTextFileParser.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTextFileParser
{
    public class CreateNewInTextFile
    {
        public bool CreateNewRecord(string file, Person p)
        {
            if (p == null) { return false; }

            string basePath = AppContext.BaseDirectory;
            string document = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\TextFiles\" + file + ".txt"));
            string stringify = String.Format("{0}|{1}|{2}|{3}|{4}", p.LastName, p.FirstName, p.Gender, p.FavoriteColor, p.DateOfBirth);
            IEnumerable<string> newLine = new string[] { stringify };
            File.AppendAllText(document, Environment.NewLine);
            File.AppendAllLines(document, newLine);
            return true;
        }
    }
}
