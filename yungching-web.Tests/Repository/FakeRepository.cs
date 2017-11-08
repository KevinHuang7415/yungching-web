using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using yungching_web.Models;
using yungching_web.Repository;

namespace yungching_web.Tests.Repository
{
    class FakeRepository
        : IRepository<Customer>
    {
        private List<Customer> data;

        public FakeRepository()
        {
            data = new List<Customer> { TestData1.customer };
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
}
