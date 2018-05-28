namespace EmpyrionManagerTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using EmpyrionManager.Extensions;

    [TestClass]
    public class BaseUnitTests
    {
        [TestMethod]
        public void InvalidPath()
        {
            var validPath = "C:\\Temp\\Valid\\Path\\";
            var invalidPath = "C:\\Temp\n\\Valid\\Path\\";

            Assert.IsTrue(invalidPath.ContainsInvalidPathCharacter());
            Assert.IsFalse(validPath.ContainsInvalidPathCharacter());
        }

        [TestMethod]
        public void InvalidFiles()
        {
            var validFile = "ValidFile.txt";
            var invalidFile = "ValidFile?.txt";

            Assert.IsTrue(invalidFile.ContainsInvalidFileCharacter());
            Assert.IsFalse(validFile.ContainsInvalidFileCharacter());
        }

        [TestMethod]
        public void TrailingString()
        {
            var orig = "TestString";
            var trailing = "Trailing";

            Assert.IsTrue(orig.TrailingString(trailing).EndsWith(trailing));
        }

        [TestMethod]
        public void PrecedingString()
        {
            var orig = "TestString";
            var precede = "Preceding";

            Assert.IsTrue(orig.PrecedingString(precede).StartsWith(precede));
        }

        ////TODO: Test Backup/BackupComponent functionality
    }
}