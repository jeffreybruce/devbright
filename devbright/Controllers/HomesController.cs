using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using devbright.Models;

namespace devbright.Controllers
{
    public class HomesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Homes
        public ActionResult Index()
        {
            return View(db.Homes.ToList());
        }

        public ActionResult Search(string sortOrder = "", string sortDir = "desc")
        {
            List<Home> homes = db.Homes.ToList();

            if (sortOrder == "title")
            {
                if (sortDir == "asc") homes = homes.OrderBy(h => h.title).ToList();
                else if (sortDir == "desc") homes = homes.OrderByDescending(h => h.title).ToList();
            }

            if (sortDir == "desc") ViewData["sortDir"] = "asc"; else ViewData["sortDir"] = "desc";

            return View(homes);
        }

        [HttpPost]
        public ActionResult Search(FormCollection form)
        {
            List<Home> homes = db.Homes.ToList();

            string title = form["TitleFilter"].ToString();
            decimal minPrice = 0;
            decimal maxPrice = 0;
            if (form["MinPrice"].ToString() != "") minPrice = Convert.ToDecimal(form["MinPrice"]);
            if (form["MaxPrice"].ToString() != "") maxPrice = Convert.ToDecimal(form["MaxPrice"]);
            string zipCode = form["ZipCodeFilter"].ToString();
            decimal minBedrooms = 0;
            decimal maxBedrooms = 0;
            if (form["MinBedrooms"].ToString() != "") minBedrooms = Convert.ToDecimal(form["MinBedrooms"]);
            if (form["MaxBedrooms"].ToString() != "") maxBedrooms = Convert.ToDecimal(form["MaxBedrooms"]);

            if (title != "") homes = homes.Where(h => h.title.ToLower().Contains(title)).ToList();
            if (minPrice > 0) homes = homes.Where(h => h.salePrice >= minPrice).ToList();
            if (maxPrice > 0) homes = homes.Where(h => h.salePrice <= maxPrice).ToList();
            if (zipCode != "") homes = homes.Where(h => h.zipCode == zipCode).ToList();
            if (minBedrooms > 0) homes = homes.Where(h => h.numberOfBedrooms >= minBedrooms).ToList();
            if (maxBedrooms > 0) homes = homes.Where(h => h.numberOfBedrooms <= maxBedrooms).ToList();

            ViewData["MinBedrooms"] = minBedrooms;
            ViewData["MaxBedrooms"] = maxBedrooms;
            ViewData["MinPrice"] = minPrice;
            ViewData["MaxPrice"] = maxPrice;
            ViewData["TitleFilter"] = title;
            ViewData["ZipCodeFilter"] = zipCode;

            return View(homes.ToList());
        }

        // GET: Homes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Home home = db.Homes.Find(id);
            if (home == null)
            {
                return HttpNotFound();
            }
            return View(home);
        }

        public ActionResult MoreInformation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Home home = db.Homes.Find(id);
            if (home == null)
            {
                return HttpNotFound();
            }
            return View(home);
        }

        [HttpPost]
        public ActionResult MoreInformation(FormCollection form)
        {
            int homeID = Convert.ToInt32(form["homeID"]);
            string emailAddress = form["EmailAddress"].ToString();
            string name = form["Name"].ToString();
            var title = (from h in db.Homes
                         where h.homeID == homeID
                         select h.title).Single();

            Lead lead = new Lead();

            lead.homeID = homeID;
            lead.emailAddress = emailAddress;
            lead.name = name;
            lead.homeTitle = title.ToString();

            db.Leads.Add(lead);
            db.SaveChanges();
            return RedirectToAction("Search");

        }

        // GET: Homes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Homes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "homeID,title,salePrice,zipCode,numberOfBedrooms")] Home home)
        {
            if (ModelState.IsValid)
            {
                db.Homes.Add(home);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(home);
        }

        // GET: Homes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Home home = db.Homes.Find(id);
            if (home == null)
            {
                return HttpNotFound();
            }
            return View(home);
        }

        // POST: Homes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "homeID,title,salePrice,zipCode,numberOfBedrooms")] Home home)
        {
            if (ModelState.IsValid)
            {
                db.Entry(home).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(home);
        }

        // GET: Homes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Home home = db.Homes.Find(id);
            if (home == null)
            {
                return HttpNotFound();
            }
            return View(home);
        }

        // POST: Homes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Home home = db.Homes.Find(id);
            db.Homes.Remove(home);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
