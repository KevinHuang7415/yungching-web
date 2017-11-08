using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using yungching_web.Controllers;
using yungching_web.Models;
using yungching_web.Repository;
using yungching_web.Tests.Repository;

namespace yungching_web.Controllers.Tests
{
    [TestClass]
    public class CustomersControllerTests
    {
        [TestMethod]
        public void IndexTest()
        {
            // Arrange
            var (repo, controller) = Arrange();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DetailsTest()
        {
            // Arrange
            var (repo, controller) = Arrange();

            // Act
            var result = controller.Details(TestData1.Id) as ViewResult;
            var customer = result.ViewData.Model as Customer;

            // Assert
            Assert.AreEqual("Kevin", customer.ContactName);
        }

        [TestMethod]
        public void CreateTest()
        {
            // Arrange
            var (repo, controller) = Arrange();

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateTest1()
        {
            // Arrange
            var (repo, controller) = Arrange();

            // Act
            var result = controller.Create(TestData2.customer) as ViewResult;

            // Assert
            var createdData = repo.Read(data => data.CustomerID == TestData2.Id);
            Assert.AreEqual("kevin", createdData.ContactName);
        }

        [TestMethod]
        public void EditTest()
        {
            // Arrange
            var (repo, controller) = Arrange();

            // Act
            var result = controller.Edit(TestData1.Id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EditTest1()
        {
            // Arrange
            var (repo, controller) = Arrange();

            // Act
            var customer = repo.Read(data => data.CustomerID == TestData1.Id);
            customer.ContactName = "Kelvin";
            var result = controller.Edit(customer) as ViewResult;

            // Assert
            customer = repo.Read(data => data.CustomerID == TestData1.Id);
            Assert.AreEqual("Kelvin", customer.ContactName);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // Arrange
            var (repo, controller) = Arrange();

            // Act
            var result = controller.Delete(TestData1.Id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteConfirmedTest()
        {
            // Arrange
            var (repo, controller) = Arrange();

            // Act
            var result = controller.DeleteConfirmed(TestData1.Id) as ViewResult;

            // Assert
            var deletedData = repo.Read(data => data.CustomerID == TestData1.Id);
            Assert.AreEqual(null, deletedData);
        }

        private (FakeRepository, CustomersController) Arrange()
        {
            var repo = new FakeRepository();
            var controller = new CustomersController(repo);
            return (repo, controller);
        }
    }
}