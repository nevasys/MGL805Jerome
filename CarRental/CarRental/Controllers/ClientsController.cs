using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace CarRental.Controllers
{
    public class ClientsController : Controller
    {
        private ApplicationDbContext _dbContext;

        public ClientsController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // GET: Clients
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new HttpUnauthorizedResult();
            }
            //if (!User.IsInRole("Admin") && !User.IsInRole("Agence"))
            //{
            //    return new HttpUnauthorizedResult();
            //}
            string MyUserId = User.Identity.GetUserId();
            bool IsClient = User.IsInRole("Client");
            var clients = _dbContext.Clients.Where(x => (IsClient == true) ? x.UserId.CompareTo(MyUserId) == 0 : true).ToList();

            return View(clients);
            
        }

        public ActionResult New()
        {

            var client = new Client();
            client.User = new ApplicationUser();

            if (!User.IsInRole("Admin") && !User.IsInRole("Agence"))
            {
                // default role: client
                IdentityUserRole iur = new IdentityUserRole();
                iur.RoleId = "3";
                client.User.Roles.Add(iur);
            }
            client.UserId = User.Identity.GetUserId();
            client.User.UserName = User.Identity.GetUserName();
            client.User.Email = User.Identity.GetUserName();
            client.IsActive = true;
            client.HasValidDriverLicense = true;
            client.BirthDate = DateTime.Now;

            return View(client);
        }

        public ActionResult Add(Client client)
        {
            // Current user already created as aspnet user
            client.User = null;

            client.UserId = User.Identity.GetUserId();
            client.CreatedBy = User.Identity.Name;
            client.CreatedOn = DateTime.Now;
            client.ModifiedBy = User.Identity.Name;
            client.ModifiedOn = DateTime.Now;
            //client.IsActive = true;

            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var client = _dbContext.Clients.SingleOrDefault(v => v.Id == id);

            if (client == null)
                return HttpNotFound();

            return View(client);
        }

        [HttpPost]
        public ActionResult Update(Client client)
        {
            var clientInDb = _dbContext.Clients.SingleOrDefault(v => v.Id == client.Id);

            if (clientInDb == null)
                return HttpNotFound();

            clientInDb.BirthDate = client.BirthDate;
            clientInDb.DriverLicenseNUmber = client.DriverLicenseNUmber;
            clientInDb.HasValidDriverLicense = client.HasValidDriverLicense;
            clientInDb.CreatedBy = client.CreatedBy != string.Empty ? client.CreatedBy : User.Identity.Name;
            clientInDb.CreatedOn = client.CreatedOn != DateTime.MinValue ? client.CreatedOn : DateTime.Now;
            clientInDb.ModifiedBy = User.Identity.Name;
            clientInDb.ModifiedOn = DateTime.Now;

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var client = _dbContext.Clients.SingleOrDefault(v => v.Id == id);

            if (client == null)
                return HttpNotFound();

            return View(client);
        }

        [HttpPost]
        public ActionResult DoDelete(int id)
        {
            var client = _dbContext.Clients.SingleOrDefault(v => v.Id == id);
            if (client == null)
                return HttpNotFound();
            _dbContext.Clients.Remove(client);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}