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
    public class PlayerDataModelTests
    {
        [TestMethod()]
        public void PlayerDataModelTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void InitializeTest()
        {
            var dataModel = new PlayerDataModel();
            dataModel.Initialize();
            //dataModel.WriteFile();
        }

        [TestMethod()]
        public void WriteFileTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void UpdateHashesTest()
        {
            var dataModel = new PlayerDataModel();
            dataModel.Initialize();
            dataModel.UpdateHashes();
        }

        [TestMethod()]
        public void GetBackupFilesTest()
        {
            var dataModel = new PlayerDataModel();
            dataModel.Initialize();
            var test = dataModel.GetBackupFiles();
        }
    }
}