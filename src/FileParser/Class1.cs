using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileParser
{
    public class ParserMain
    {
        [DelimitedRecord("|")]
        public class Pipes
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Gender { get; set; }
            public string FavoriteColor { get; set; }
            [FieldConverter(ConverterKind.Date, "M/d/yyyy")]
            public DateTime DateOfBirth;
        }

        [DelimitedRecord(",")]
        public class Commas
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Gender { get; set; }
            public string FavoriteColor { get; set; }
            [FieldConverter(ConverterKind.Date, "M/d/yyyy")]
            public DateTime DateOfBirth;
        }

        [DelimitedRecord(" ")]
        public class Spaces
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Gender { get; set; }
            public string FavoriteColor { get; set; }
            [FieldConverter(ConverterKind.Date, "M/d/yyyy")]
            public DateTime DateOfBirth;
        }
    }
}
