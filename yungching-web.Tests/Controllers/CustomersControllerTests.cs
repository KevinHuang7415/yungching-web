using Microsoft.VisualStudio.TestTools.UnitTesting;
using yungching_web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using yungching_web.Models;
using yungching_web.Repository;
using System.Linq.Expressions;

namespace yungching_web.Controllers.Tests
{
    [TestClass]
    public class CustomersControllerTests
    {
        [TestMethod]
        public void IndexTest()
        {
            // Arrange
            var controller = new CustomersController(new FakeRepository());

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DetailsTest()
        {
            // Arrange
            var repo = new FakeRepository();
            var controller = new CustomersController(repo);

            // Act
            var result = controller.Details(TestData.Id) as ViewResult;
            var customer = result.ViewData.Model as Customer;

            // Assert
            Assert.AreEqual("Kevin", customer.ContactName);
        }

        [TestMethod]
        public void CreateTest()
        {
            // Arrange
            var controller = new CustomersController(new FakeRepository());

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateTest1()
        {
            // Arrange
            var repo = new FakeRepository();
            var controller = new CustomersController(repo);

            // Act
            var result = controller.Create(TestData.customer) as ViewResult;

            // Assert
            var createdData = repo.Read(data => data.CustomerID == TestData.Id2);
            Assert.AreEqual("kevin", createdData.ContactName);
        }

        [TestMethod]
        public void EditTest()
        {
            // Arrange
            var repo = new FakeRepository();
            var controller = new CustomersController(repo);

            // Act
            var result = controller.Edit(TestData.Id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EditTest1()
        {
            // Arrange
            var repo = new FakeRepository();
            var controller = new CustomersController(repo);

            // Act
            var customer = repo.Read(data => data.CustomerID == TestData.Id);
            customer.ContactName = "Kelvin";
            var result = controller.Edit(customer) as ViewResult;

            // Assert
            customer = repo.Read(data => data.CustomerID == TestData.Id);
            Assert.AreEqual("Kelvin", customer.ContactName);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // Arrange
            var repo = new FakeRepository();
            var controller = new CustomersController(repo);

            // Act
            var result = controller.Delete(TestData.Id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteConfirmedTest()
        {
            // Arrange
            var repo = new FakeRepository();
            var controller = new CustomersController(repo);

            // Act
            var result = controller.DeleteConfirmed(TestData.Id) as ViewResult;

            // Assert
            var deletedData = repo.Read(data => data.CustomerID == TestData.Id);
            Assert.AreEqual(null, deletedData);
        }
    }

    public class FakeRepository
        :IRepository<Customer>
    {
        private List<Customer> data;

        public FakeRepository()
        {
            data = new List<Customer>
            {
                new Customer()
                {
                    CustomerID = TestData.Id,
                    CompanyName = "Yungching",
                    ContactName = "Kevin",
                    ContactTitle = "Software Engineer",
                    Address = "HOME",
                    City = "Taoyuan",
                    Region = "",
                    PostalCode = "12345",
                    Country = "Taiwan",
                    Phone = "0912345678",
                    Fax = "",
                }
            };
        }

        public void Create(Customer entity)
        {
            data.Add(entity);
        }

        public Customer Read(Expression<Func<Customer, bool>> predicate)
        {
            return data.AsQueryable().Where(predicate).FirstOrDefault();
        }

        public IQueryable<Customer> Reads()
        {
            return data.AsQueryable();
        }

        public void Update(Customer entity)
        {
            int index = Find(entity.CustomerID);
            if (index != -1)
            {
                data[index] = entity;
            }
        }

        public void Delete(Customer entity)
        {
            int index = Find(entity.CustomerID);
            if (index != -1)
            {
                data.RemoveAt(index);
            }
        }

        public void SaveChanges()
        { }

        private int Find(string id)
        {
            return data.FindIndex(item => item.CustomerID == id);
        }
    }

    struct TestData
    {
        public static String Id { get; } = "EDCBA";
        public static String Id2 { get; } = "ABCDE";

        public static Customer customer { get; } = new Customer()
        {
            CustomerID = Id2,
            CompanyName = "yungching",
            ContactName = "kevin",
            ContactTitle = "software engineer",
            Address = "home",
            City = "taoyuan",
            Region = "",
            PostalCode = "12345",
            Country = "taiwan",
            Phone = "0912345678",
            Fax = "",
        };
    }
}