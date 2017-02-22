using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarRental.Controllers
{
    public class CarsController : Controller
    {
        private ApplicationDbContext _dbContext;

        public CarsController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // GET: Cars
        public ActionResult Index()
        {
            
            if (!User.Identity.IsAuthenticated)
            {
                return new HttpUnauthorizedResult();
            }
            if (!User.IsInRole("Admin"))
            {
                return new HttpUnauthorizedResult();
            }


            var cars = _dbContext.Cars.ToList();

            return View(cars);
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Add(Car car)
        {
            car.CreatedBy = User.Identity.Name;
            car.CreatedOn = DateTime.Now;
            car.IsActive = true;

            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var car = _dbContext.Cars.SingleOrDefault(v => v.Id == id);

            if (car == null)
                return HttpNotFound();

            return View(car);
        }

        [HttpPost]
        public ActionResult Update(Car car)
        {
            var carInDb = _dbContext.Cars.SingleOrDefault(v => v.Id == car.Id);

            if (carInDb == null)
                return HttpNotFound();

            carInDb.Brand = car.Brand;
            carInDb.model = car.model;
            carInDb.Description = car.Description;
            carInDb.Type = car.Type;
            carInDb.HorsePower = car.HorsePower;
            carInDb.NumberOfDoors = car.NumberOfDoors;
            carInDb.NumberOfPassenger = car.NumberOfPassenger;
            carInDb.DailyRate = car.DailyRate;
            carInDb.IsActive = car.IsActive;
            carInDb.CreatedBy = car.CreatedBy != string.Empty ? car.CreatedBy : User.Identity.Name;
            carInDb.CreatedOn = car.CreatedOn != DateTime.MinValue ? car.CreatedOn : DateTime.Now;
            carInDb.ModifiedBy = User.Identity.Name;
            carInDb.ModifiedOn = DateTime.Now;

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var car = _dbContext.Cars.SingleOrDefault(v => v.Id == id);

            if (car == null)
                return HttpNotFound();

            return View(car);
        }

        [HttpPost]
        public ActionResult DoDelete(int id)
        {
            var car = _dbContext.Cars.SingleOrDefault(v => v.Id == id);
            if (car == null)
                return HttpNotFound();
            _dbContext.Cars.Remove(car);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}