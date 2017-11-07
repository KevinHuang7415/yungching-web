using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using yungching_web.Models;
using yungching_web.Repository;

namespace yungching_web.Controllers
{
    public class CustomersController : Controller
    {
        private IRepository<Customer> repo;

        public CustomersController()
            : this(new Repository<Customer>(new NorthwindEntities()))
        {
        }

        public CustomersController(IRepository<Customer> repo)
        {
            this.repo = repo;
        }

        // GET: Customers
        public ActionResult Index()
        {
            return View(repo.Reads());
        }

        // GET: Customers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid CustomerID");
            }
            Customer customer = repo.Read(x => x.CustomerID == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customer customer)
        {
            if (customer.CustomerID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid CustomerID");
            }

            if (customer.CompanyName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid CompanyName");
            }

            if (ModelState.IsValid)
            {
                repo.Create(customer);
                repo.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid CustomerID");
            }
            Customer customer = repo.Read(x => x.CustomerID == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                repo.Update(customer);
                repo.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid CustomerID");
            }
            Customer customer = repo.Read(x => x.CustomerID == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Customer customer = repo.Read(x => x.CustomerID == id);
            repo.Delete(customer);
            repo.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
