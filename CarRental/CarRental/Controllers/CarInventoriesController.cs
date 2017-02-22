using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{
    public class CarInventoriesController : Controller
    {
        private ApplicationDbContext _dbContext;

        public CarInventoriesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // GET: CarInventories
        public ActionResult Index()
        {

            var carinventories = _dbContext.CarInventories.ToList();
            return View(carinventories);
        }


        public ActionResult New()
        {
            IEnumerable<SelectListItem> agencyitems =
               _dbContext.Agencies.Where(x => x.IsActive == true).Select(d =>
               new SelectListItem
               {
                   Value = d.Id.ToString(),
                   Text = d.Name + " " + d.Division + " " + d.CivicNumber + " " + d.StreetName + " " + d.Province + " " + d.PostalCode
               });

            ViewBag.AgencyId = agencyitems;

            IEnumerable<SelectListItem> caritems =
               _dbContext.Cars.Where(x => x.IsActive == true).Select(d =>
               new SelectListItem
               {
                   Value = d.Id.ToString(),
                   Text = d.Brand + " " + d.model + " " + d.Type + " " + d.NumberOfDoors + " portes " + d.NumberOfPassenger + " passagers " + d.HorsePower + "hp " + d.DailyRate + "$"
               });

            ViewBag.CarId = caritems;

            return View();
        }

        public ActionResult Add(CarInventory carinventory)
        {
            carinventory.CreatedBy = User.Identity.Name;
            carinventory.CreatedOn = DateTime.Now;
            carinventory.IsActive = true;

            _dbContext.CarInventories.Add(carinventory);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            //IEnumerable<SelectListItem> agencyitems =
            //   _dbContext.Agencies.Where(x => x.IsActive == true).Select(d =>
            //   new SelectListItem
            //   {
            //       Value = d.Id.ToString(),
            //       Text = d.Name + " " + d.Division + " " + d.CivicNumber + " " + d.StreetName + " " + d.Province + " " + d.PostalCode
            //   });

            //ViewBag.AgencyId = agencyitems;

            //IEnumerable<SelectListItem> caritems =
            //   _dbContext.Cars.Where(x => x.IsActive == true).Select(d =>
            //   new SelectListItem
            //   {
            //       Value = d.Id.ToString(),
            //       Text = d.Brand + " " + d.model + " " + d.Type + " " + d.NumberOfDoors + " portes " + d.NumberOfPassenger + " passagers " + d.HorsePower + "hp " + d.DailyRate + "$"
            //   });
             
            //ViewBag.CarId = caritems;
            var carinventory = _dbContext.CarInventories.SingleOrDefault(v => v.Id == id);

            if (carinventory == null)
                return HttpNotFound();

            PopulateAgenciesDropDownList(carinventory.AgencyId);
            PopulateCarsDropDownList(carinventory.CarId);

            return View(carinventory);
        }

        private void PopulateAgenciesDropDownList(object selectedAgency = null)
        {
            IEnumerable<SelectListItem> agencyitems =
               _dbContext.Agencies.Where(x => x.IsActive == true).Select(d =>
               new SelectListItem
               {
                   Value = d.Id.ToString(),
                   Text = d.Name + " " + d.Division + " " + d.CivicNumber + " " + d.StreetName + " " + d.Province + " " + d.PostalCode
               });
            ViewBag.AgenciesBag = new SelectList(agencyitems, "Value", "Text", selectedAgency);
        }

        private void PopulateCarsDropDownList(object selectedCar = null)
        {
            IEnumerable<SelectListItem> caritems =
               _dbContext.Cars.Where(x => x.IsActive == true).Select(d =>
               new SelectListItem
               {
                   Value = d.Id.ToString(),
                   Text = d.Brand + " " + d.model + " " + d.Type + " " + d.NumberOfDoors + " portes " + d.NumberOfPassenger + " passagers " + d.HorsePower + "hp " + d.DailyRate + "$"
               });
            ViewBag.CarsBag = new SelectList(caritems, "Value", "Text", selectedCar);
        }

        [HttpPost]
        public ActionResult Update(CarInventory carinventory)
        {
            var carinventoryInDb = _dbContext.CarInventories.SingleOrDefault(v => v.Id == carinventory.Id);

            if (carinventoryInDb == null)
                return HttpNotFound();

            carinventoryInDb.AgencyId = carinventory.AgencyId;
            carinventoryInDb.CarId = carinventory.CarId;
            carinventoryInDb.AirConditionner = carinventory.AirConditionner;
            carinventoryInDb.DailyRate = carinventory.DailyRate;
            carinventoryInDb.DVDplayer = carinventory.DVDplayer;
            carinventoryInDb.IsReserved = carinventory.IsReserved;
            carinventoryInDb.MP3player = carinventory.MP3player;
            carinventoryInDb.NavigationSystem = carinventory.NavigationSystem;
            carinventoryInDb.Odometer = carinventory.Odometer;
            carinventoryInDb.SerialNumber = carinventory.SerialNumber;
            carinventoryInDb.year = carinventory.year;
            carinventoryInDb.IsActive = carinventory.IsActive;
            carinventoryInDb.CreatedBy = carinventory.CreatedBy != string.Empty ? carinventory.CreatedBy : User.Identity.Name;
            carinventoryInDb.CreatedOn = carinventory.CreatedOn != DateTime.MinValue ? carinventory.CreatedOn : DateTime.Now;
            carinventoryInDb.ModifiedBy = User.Identity.Name;
            carinventoryInDb.ModifiedOn = DateTime.Now;

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var carinventory = _dbContext.CarInventories.SingleOrDefault(v => v.Id == id);

            if (carinventory == null)
                return HttpNotFound();

            return View(carinventory);
        }

        [HttpPost]
        public ActionResult DoDelete(int id)
        {
            var carinventory = _dbContext.CarInventories.SingleOrDefault(v => v.Id == id);
            if (carinventory == null)
                return HttpNotFound();
            _dbContext.CarInventories.Remove(carinventory);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}