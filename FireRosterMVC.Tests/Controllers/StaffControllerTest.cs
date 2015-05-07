using Microsoft.VisualStudio.TestTools.UnitTesting;
using FireRosterMVC.Controllers;
using System.Web.Mvc;
using FireRosterMVC.Models;
using EntityFramework.Testing;
using Moq;
using Moq.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;

namespace FireRosterMVC.Tests.Controllers
{
    [TestClass]
    public class StaffControllerTest
    {
        private StaffController getController()
        {
            // Basing this on https://msdn.microsoft.com/en-us/data/dn314429.aspx#nonQuery
            var data = new List<Staff>{
                new Staff{ FirstName = "Scott"},
                new Staff{ FirstName = "Bob" },
                new Staff{ FirstName = "Ruqsaar"}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Staff>>();
            mockSet.As<IQueryable<Staff>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Staff>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Staff>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Staff>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var context = new Mock<FireRosterDB>();
            context.Setup(m => m.StaffList).Returns(mockSet.Object);

            return new StaffController(context.Object);
        }
        [TestMethod]
        public void Staff_Index()
        {
            var ctlr = getController();
            var rslt = ctlr.Index("", "", "", "", 1) as ViewResult;
            Assert.IsNotNull(rslt, "I did not find a response!");
        }
    }
}
