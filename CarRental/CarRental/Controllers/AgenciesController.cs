using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class AgenciesController : Controller
    {
        private ApplicationDbContext _dbContext;

        public AgenciesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // GET: Agencies
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new HttpUnauthorizedResult();
            }
            if (!User.IsInRole("Admin") && !User.IsInRole("Agence"))
            {
                return new HttpUnauthorizedResult();
            }

            var agencies = _dbContext.Agencies.ToList();

            return View(agencies);
        }

        private IEnumerable<Agency> GetData(string selectedCategory)
        {
            IEnumerable<Agency> data = _dbContext.Agencies.ToList();
            if (selectedCategory != "All")
            {
                //Category selected = (Category)Enum.Parse(typeof(Category), selectedCategory);
                //data = products.Where(p => p.Category == selected);
            }
            return data;
        }

        public JsonResult GetAgencyDataJson(string selectedCategory = "All")
        {
            IEnumerable<Agency> data = GetData(selectedCategory);
            IEnumerable<Geodata> geodata = data
                .Select(i => new Geodata
                { Id= i.Id,
                    Name = i.Name,
                    GeoLong = i.Longitude.ToString(),
                    GeoLat = i.Latitude.ToString()}).ToArray();
            return Json(geodata, JsonRequestBehavior.AllowGet);
        }

        public class Geodata
        {
            public int Id;
            public string Name;
            public string GeoLong;
            public string GeoLat;
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Add(Agency agency)
        {
            agency.CreatedBy = User.Identity.Name;
            agency.CreatedOn = DateTime.Now;
            agency.IsActive = true;

            _dbContext.Agencies.Add(agency);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var agency = _dbContext.Agencies.SingleOrDefault(v => v.Id == id);

            if (agency == null)
                return HttpNotFound();

            return View(agency);
        }

        [HttpPost]
        public ActionResult Update(Agency agency)
        {
            var agencyInDb = _dbContext.Agencies.SingleOrDefault(v => v.Id == agency.Id);

            if (agencyInDb == null)
                return HttpNotFound();

            agencyInDb.AffiliatedWith = agency.AffiliatedWith;
            agencyInDb.City = agency.City;
            agencyInDb.CivicNumber = agency.CivicNumber;
            agencyInDb.Division = agency.Division;
            agencyInDb.Email = agency.Email;
            agencyInDb.FaxNumber = agency.FaxNumber;
            agencyInDb.Latitude = agency.Latitude;
            agencyInDb.Longitude = agency.Longitude;
            agencyInDb.Name = agency.Name;
            agencyInDb.PostalCode = agency.PostalCode;
            agencyInDb.Province = agency.Province;
            agencyInDb.State = agency.State;
            agencyInDb.StreetName = agency.StreetName;
            agencyInDb.Suite = agency.Suite;
            agencyInDb.TelephoneNumber = agency.TelephoneNumber;
            agencyInDb.IsActive = agency.IsActive;
            agencyInDb.CreatedBy = agency.CreatedBy != string.Empty ? agency.CreatedBy : User.Identity.Name;
            agencyInDb.CreatedOn = agency.CreatedOn != DateTime.MinValue ? agency.CreatedOn : DateTime.Now;
            agencyInDb.ModifiedBy = User.Identity.Name;
            agencyInDb.ModifiedOn = DateTime.Now;

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var agency = _dbContext.Agencies.SingleOrDefault(v => v.Id == id);

            if (agency == null)
                return HttpNotFound();

            return View(agency);
        }

        [HttpPost]
        public ActionResult DoDelete(int id)
        {
            var agency = _dbContext.Agencies.SingleOrDefault(v => v.Id == id);
            if (agency == null)
                return HttpNotFound();
            _dbContext.Agencies.Remove(agency);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}