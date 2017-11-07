using System.Linq;
using System.Web.Mvc;
using yungching_web.Models;
using yungching_web.Repository;
using yungching_web.ViewModels;

namespace yungching_web.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<Customer> repo;

        public HomeController()
            : this(new Repository<Customer>(new NorthwindEntities()))
        {
        }

        public HomeController(IRepository<Customer> repo)
        {
            this.repo = repo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var titles = repo.Reads()
                            .GroupBy(customer => customer.ContactTitle)
                            .Select(customerTitleGroup => new CustomersTitle()
                            {
                                ContactTitle = customerTitleGroup.Key,
                                ContactTitleCount = customerTitleGroup.Count()
                            });

            return View(titles);
        }
    }
}