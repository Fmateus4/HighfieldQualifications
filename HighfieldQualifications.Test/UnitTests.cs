namespace HighfieldQualifications.Test
{
    using HighfieldQualifications.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void Check_CalculateAge_returns_Success()
        {
            var age = DataService.CalculateAge(
                DateTimeOffset.Now.AddYears(-25), 
                DateTimeOffset.Now);

            Assert.AreEqual(25, age);
        }

        [TestMethod]
        public void Check_CalculateAge_OtherTimeZone_returns_Success()
        {
            var knownDob = new DateTimeOffset(2008, 8, 22, 1, 0, 0, new TimeSpan(-13, 0, 0));

            var age = DataService.CalculateAge(
                knownDob,
                DateTimeOffset.Now);

            Assert.AreEqual(10, age);
        }
    }
}
