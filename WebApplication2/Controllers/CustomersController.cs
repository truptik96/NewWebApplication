using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Data.Entity;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class CustomersController : Controller
    {
        public ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool Disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            var customer = _context.Customers.Include(m => m.MemberShipType).ToList();
            return View(customer);
        }
        public ActionResult New()
        {
            var membershiptype = _context.MemberShipTypes.ToList();
            var viewmodel = new NewCustomerViewModel()
            {
                MemberShipTypes = membershiptype
            };

            return View(viewmodel);
        }
        public ActionResult Save(Customer customer)
        {
            if(customer.Id==0)
            _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.IsSubscribedtoNewsLetter = customer.IsSubscribedtoNewsLetter;
                customerInDb.MemberShipTypeId = customer.MemberShipTypeId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            var viewmodel = new NewCustomerViewModel()
            {
                Customer = customer,
                MemberShipTypes = _context.MemberShipTypes.ToList()
            };
            return View("New",viewmodel);
        }
    }
}