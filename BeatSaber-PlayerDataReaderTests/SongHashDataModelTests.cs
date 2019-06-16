using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeatSaber_PlayerDataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSaber_PlayerDataReader.Tests
{
    [TestClass()]
    public class SongHashDataModelTests
    {
        [TestMethod()]
        public void SongHashDataModelTest()
        {
            
        }

        [TestMethod()]
        public void InitializeTest()
        {
            var songHashData = new SongHashDataModel();
            songHashData.Initialize();

        }
    }
}