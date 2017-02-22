using CarRental.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{
    public class ReservationsController : Controller
    {
        
        private ApplicationDbContext _dbContext;

        public ReservationsController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // GET: Reservations
        public ActionResult Index()
        {

            if (!User.Identity.IsAuthenticated)
            {
                // SVP enregistrez-vous sur le site BazooPasCher pour pouvoir réserver une voiture!
                return new HttpUnauthorizedResult();
            }
            string MyUserId = User.Identity.GetUserId();
            bool IsClient = User.IsInRole("Client");
            bool IsAgency= User.IsInRole("Agence");
            var clients = _dbContext.Clients.Where(x => (IsClient == true) ? x.UserId.CompareTo(MyUserId) == 0 : true).FirstOrDefault();
            var reservations = _dbContext.Reservations.Where(x => (IsClient == true) ? x.ClientId == clients.Id : true).ToList();

            return View(reservations);
        }


        public ActionResult New()
        {
            string MyUserId = User.Identity.GetUserId();
            bool IsClient = User.IsInRole("Client");
            bool IsAgency = User.IsInRole("Agence");
            var clients = _dbContext.Clients.Where(x => (IsClient == true) ? x.UserId.CompareTo(MyUserId) == 0 : true).FirstOrDefault();

            IEnumerable<SelectListItem> clientitems =
               _dbContext.Clients.Where(x => x.IsActive == true && x.Id == clients.Id).Select(d =>
               new SelectListItem
               {
                   Value = d.Id.ToString(),
                   Text = d.User.UserName + " " + d.User.Email + " " + d.HasValidDriverLicense + " " + d.DriverLicenseNUmber 
               });

            ViewBag.ClientId = clientitems;

            IEnumerable<SelectListItem> inventoryitems =
               _dbContext.CarInventories.Where(x => x.IsActive == true).Select(d =>
               new SelectListItem
               {
                   Value = d.Id.ToString(),
                   Text = d.Agency.Name + d.Agency.Division + d.Car.Brand + " " + d.Car.model + " " + d.Car.Type + " " + d.Car.NumberOfDoors + " portes " + d.Car.NumberOfPassenger + " passagers " + d.Car.HorsePower + "hp " + d.Car.DailyRate + "$"
               });

            ViewBag.CarInventoryId = inventoryitems;

            return View();
        }

        public ActionResult Add(Reservation reservation)
        {
            reservation.CreatedBy = User.Identity.Name;
            reservation.CreatedOn = DateTime.Now;
            reservation.IsActive = true;

            calcTotalAmount(reservation);
            reservation.AgencyId = FindAgencyId(reservation.CarInventoryId);

            _dbContext.Reservations.Add(reservation);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var reservation = _dbContext.Reservations.SingleOrDefault(v => v.Id == id);

            if (reservation == null)
                return HttpNotFound();

            PopulateClientsDropDownList(reservation.ClientId);
            PopulateCarInventoriesDropDownList(reservation.CarInventoryId);

            return View(reservation);
        }

        private void PopulateClientsDropDownList(object selectedAgency = null)
        {
            string MyUserId = User.Identity.GetUserId();
            bool IsClient = User.IsInRole("Client");
            bool IsAgency = User.IsInRole("Agence");
            var clients = _dbContext.Clients.Where(x => (IsClient == true) ? x.UserId.CompareTo(MyUserId) == 0 : true).FirstOrDefault();

            IEnumerable<SelectListItem> clientitems =
               _dbContext.Clients.Where(x => x.IsActive == true && x.Id == clients.Id).Select(d =>
               new SelectListItem
               {
                   Value = d.Id.ToString(),
                   Text = d.User.UserName + " " + d.User.Email + " " + d.HasValidDriverLicense + " " + d.DriverLicenseNUmber
               });
            ViewBag.ClientsBag = new SelectList(clientitems, "Value", "Text", selectedAgency);
        }

        private void PopulateCarInventoriesDropDownList(object selectedCar = null)
        {
            IEnumerable<SelectListItem> carinventoryitems =
               _dbContext.CarInventories.Where(x => x.IsActive == true).Select(d =>
               new SelectListItem
               {
                   Value = d.Id.ToString(),
                   Text = d.Agency.Name + d.Agency.Division + d.Car.Brand + " " + d.Car.model + " " + d.Car.Type + " " + d.Car.NumberOfDoors + " portes " + d.Car.NumberOfPassenger + " passagers " + d.Car.HorsePower + "hp " + d.Car.DailyRate + "$"
               });
            ViewBag.carInventoriesBag = new SelectList(carinventoryitems, "Value", "Text", selectedCar);
        }

        [HttpPost]
        public ActionResult Update(Reservation reservation)
        {
            var reservationInDb = _dbContext.Reservations.SingleOrDefault(v => v.Id == reservation.Id);

            if (reservationInDb == null)
                return HttpNotFound();

            calcTotalAmount(reservation);

            reservationInDb.ClientId = reservation.ClientId;
            reservationInDb.CarInventoryId = reservation.CarInventoryId;
            reservationInDb.AgencyId = FindAgencyId(reservation.CarInventoryId);
            reservationInDb.DailyRate = reservation.DailyRate;
            reservationInDb.DateEnd = reservation.DateEnd;
            reservationInDb.DateStart = reservation.DateStart;
            reservationInDb.Days = reservation.Days;
            reservationInDb.Discount = reservation.Discount;
            reservationInDb.OdometerEnd = reservation.OdometerEnd;
            reservationInDb.OdometerStart = reservation.OdometerStart;
            reservationInDb.ReservationStatus = reservation.ReservationStatus;
            reservationInDb.ProvincialTax = reservation.ProvincialTax;
            reservationInDb.FederalTax = reservation.FederalTax;
            reservationInDb.Amount = reservation.Amount;
            reservationInDb.TotalAmount = reservation.TotalAmount;
            reservationInDb.IsActive = reservation.IsActive;
            reservationInDb.CreatedBy = reservation.CreatedBy != string.Empty ? reservation.CreatedBy : User.Identity.Name;
            reservationInDb.CreatedOn = reservation.CreatedOn != DateTime.MinValue ? reservation.CreatedOn : DateTime.Now;
            reservationInDb.ModifiedBy = User.Identity.Name;
            reservationInDb.ModifiedOn = DateTime.Now;

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        private void calcTotalAmount(Reservation reservation)
        {
            // Amount
            TimeSpan difference = reservation.DateEnd - reservation.DateStart;
            var days = difference.TotalDays;
            reservation.Amount = (float)days * reservation.DailyRate;
            reservation.Amount = (float)Math.Round(reservation.Amount, 2);

            // Tax
            reservation.FederalTax = (float)Math.Round(reservation.Amount * float.Parse(ConfigurationManager.AppSettings["FederalTax"], CultureInfo.InvariantCulture.NumberFormat), 2);
            reservation.ProvincialTax = (float)Math.Round(reservation.Amount * float.Parse(ConfigurationManager.AppSettings["ProvincialTax"], CultureInfo.InvariantCulture.NumberFormat), 2);
            reservation.TotalAmount = reservation.Amount + reservation.FederalTax + reservation.ProvincialTax;
        }

        private int FindAgencyId(int carInventoryId)
        {
            var carinventory = _dbContext.CarInventories.SingleOrDefault(v => v.Id == carInventoryId);

            if (carinventory == null)
                return -1;

            return carinventory.AgencyId;
        }
        public ActionResult Delete(int id)
        {
            var reservation = _dbContext.Reservations.SingleOrDefault(v => v.Id == id);

            if (reservation == null)
                return HttpNotFound();

            return View(reservation);
        }

        [HttpPost]
        public ActionResult DoDelete(int id)
        {
            var reservation = _dbContext.Reservations.SingleOrDefault(v => v.Id == id);
            if (reservation == null)
                return HttpNotFound();
            _dbContext.Reservations.Remove(reservation);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
    
}