using System;
using System.Text;
using System.Collections.Generic;
using LoanInterestCalculator;
using NUnit.Framework;
using System.IO;

namespace LoanInterestCalculatorTest
{
    /// <summary>
    /// Summary description for TestFileLoanOutput
    /// </summary>
    [TestFixture]
    public class TestFileLoanOutput
    {
        private TestContext testContextInstance;
        private FileLoanOutput fileOutput;

        public TestFileLoanOutput()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [SetUp]
        public void Instantiate_FileLoanOutput_Object()
        {
            fileOutput = null;
            fileOutput = new FileLoanOutput();
        }

        /// <summary>
        /// Not a unit test as this is testing that a path is created for 
        /// where the output file is to be created.
        /// </summary>
        [Test]
        [Category("Test_Output_SaveFile")]
        public void TestCreateDirectoryForOutputFile()
        {
            string localDirectoryToCreate;

            localDirectoryToCreate = fileOutput.DirectoryPath;
            fileOutput.CreateDirectoryForOutputFile(localDirectoryToCreate);

            Assert.IsTrue(Directory.Exists(localDirectoryToCreate));
        }

        [Test]
        [Category("Test_Output_SaveFile")]
        [Ignore("Not Implemented")]
        public void Test_StringCreatedForFileOutput()
        {
            throw new NotImplementedException();


        }
    }
}
