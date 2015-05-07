using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FireRosterMVC.Tests.Features
{
    [TestClass]
    public class FailureTest
    {
        [TestMethod]
        public void FailOnPurpose()
        {
            Assert.Fail("This test is added to have a control failure.");
        }
    }
}
