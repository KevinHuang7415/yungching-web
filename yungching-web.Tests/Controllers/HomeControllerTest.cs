using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using yungching_web.Controllers;
using yungching_web.Tests.Repository;
using yungching_web.ViewModels;

namespace yungching_web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var controller = new HomeController(new FakeRepository());

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            var controller = new HomeController(new FakeRepository());

            // Act
            var result = controller.About() as ViewResult;
            var titles = result.ViewData.Model as IEnumerable<CustomersTitle>;

            // Assert
            Assert.AreEqual(1, titles.Count());
        }
    }
}