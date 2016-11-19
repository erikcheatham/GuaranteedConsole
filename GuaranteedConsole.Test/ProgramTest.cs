using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GuaranteedConsole.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace GuaranteedConsole.Test
{
    [TestClass]
    public class ProgramTest
    {
        //TODO Find Out How To Run The Tests Better Instead of Repetition

        [TestMethod]
        public void TestMethod1()
        {
            List<Person> PipeList = RunProgram.Run("PipeDelimited", "gender", "asc", '|');
            List<Person> PipeList2 = RunProgram.Run("PipeDelimited", "gender", "asc", '|');
            Assert.AreEqual(PipeList, PipeList2);
        }

        [TestMethod]
        public void TestMethod2()
        {
            List<Person> CommaList = RunProgram.Run("CommaDelimited", "birth", "asc", ',');
            List<Person> CommaList2 = RunProgram.Run("CommaDelimited", "birth", "asc", ',');
            Assert.AreEqual(CommaList, CommaList2);
        }

        [TestMethod]
        public void TestMethod3()
        {
            List<Person> SpaceList = RunProgram.Run("SpaceDelimited", "lastname", "desc", ' ');
            List<Person> SpaceList2 = RunProgram.Run("SpaceDelimited", "lastname", "desc", ' ');
            Assert.AreEqual(SpaceList, SpaceList2);
        }
    }
}
