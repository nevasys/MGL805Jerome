using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{
    public class FAQsController : Controller
    {
        private ApplicationDbContext _dbContext;

        public FAQsController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // GET: FAQs
        public ActionResult Index()
        {

            var faqs = _dbContext.FAQs.ToList();

            return View(faqs);
        }


        public ActionResult New()
        {
            return View();
        }

        public ActionResult Add(FAQ faq)
        {
            faq.CreatedBy = User.Identity.Name;
            faq.CreatedOn = DateTime.Now;
            faq.IsActive = true;
            _dbContext.FAQs.Add(faq);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var faq = _dbContext.FAQs.SingleOrDefault(v => v.Id == id);

            if (faq == null)
                return HttpNotFound();

            return View(faq);
        }

        [HttpPost]
        public ActionResult Update(FAQ faq)
        {
            var faqInDb = _dbContext.FAQs.SingleOrDefault(v => v.Id == faq.Id);

            if (faqInDb == null)
                return HttpNotFound();

            faqInDb.Question = faq.Question;
            faqInDb.Answer = faq.Answer;
            faqInDb.IsActive = faq.IsActive;
            faqInDb.CreatedBy = faq.CreatedBy != string.Empty ? faq.CreatedBy : User.Identity.Name;
            faqInDb.CreatedOn = faq.CreatedOn != DateTime.MinValue ? faq.CreatedOn : DateTime.Now;
            faqInDb.ModifiedBy = User.Identity.Name;
            faqInDb.ModifiedOn =  DateTime.Now;

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var faq = _dbContext.FAQs.SingleOrDefault(v => v.Id == id);

            if (faq == null)
                return HttpNotFound();

            return View(faq);
        }

        [HttpPost]
        public ActionResult DoDelete(int id)
        {
            var faq = _dbContext.FAQs.SingleOrDefault(v => v.Id == id);
            if (faq == null)
                return HttpNotFound();
            _dbContext.FAQs.Remove(faq);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}