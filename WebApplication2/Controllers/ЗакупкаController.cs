using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ЗакупкаController : Controller
    {
        private Course_ProjectEntities1 db = new Course_ProjectEntities1();

        // GET: Закупка



        [Authorize(Roles = "admin, accountant, consultant")]
        public ActionResult Index(string sortOrder, string searchStringId , string searchString, string searchStringDate, string searchStringDate1)
        {
            ViewBag.IDSortParm = sortOrder == "ID" ? "id_desc" : "id";
            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.DatePostSortParm = sortOrder == "DatePost" ? "date_descPost" : "DatePost";
            var zakupki = from s in db.Закупка select s;
            if (!String.IsNullOrEmpty(searchStringId))
            {
                zakupki = zakupki.Where(s => s.ID_закупки.ToString().Equals(searchStringId));
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                zakupki = zakupki.Where(s => s.Общая_цена.ToString().Contains(searchString));
            }
            if (!String.IsNullOrEmpty(searchStringDate))
            {
                zakupki = zakupki.Where(s => s.Дата_закупки.ToString().Equals(searchStringDate));
            }
            if (!String.IsNullOrEmpty(searchStringDate1))
            {
                zakupki = zakupki.Where(s => s.Дата_поставки.ToString().Equals(searchStringDate1));
            }
            switch (sortOrder)
            {
                case "id":
                    zakupki = zakupki.OrderBy(s => s.ID_закупки);
                    break;
                case "id_desc":
                    zakupki = zakupki.OrderByDescending(s => s.ID_закупки);
                    break;
                case "price_desc":
                    zakupki = zakupki.OrderByDescending(s => s.Общая_цена);
                    break;
                case "Date":
                    zakupki = zakupki.OrderBy(s => s.Дата_закупки);
                    break;
                case "date_desc":
                    zakupki = zakupki.OrderByDescending(s => s.Дата_закупки);
                    break;
                case "DatePost":
                    zakupki = zakupki.OrderBy(s => s.Дата_поставки);
                    break;
                case "date_descPost":
                    zakupki = zakupki.OrderByDescending(s => s.Дата_поставки);
                    break;
                default:
                    zakupki = zakupki.OrderBy(s => s.Общая_цена);
                    break;
            }
            return View(zakupki.ToList());
        }

        // GET: Закупка/Details/5
        [Authorize(Roles = "admin, accountant, consultant")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Закупка закупка = db.Закупка.Find(id);
            if (закупка == null)
            {
                return HttpNotFound();
            }
            return View(закупка);
        }

        // GET: Закупка/Create
        [Authorize(Roles = "admin, accountant")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Закупка/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, accountant")]
        public ActionResult Create([Bind(Include = "ID_закупки,Общая_цена,Дата_закупки,Дата_поставки, Состав_Закупки")] Закупка закупка)
        {

            if(закупка.Дата_закупки > закупка.Дата_поставки)
            {
                ModelState.AddModelError("Дата_закупки", "Некорректная дата закупки");
            }


            if (ModelState.IsValid)
            {
                db.Закупка.Add(закупка);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(закупка);
        }

        // GET: Закупка/Edit/5
        [Authorize(Roles = "admin, accountant")]
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Закупка закупка = db.Закупка.Find(id);
             
            if (закупка == null)
            {
                return HttpNotFound();
            }
            закупка.Дата_закупки = закупка.Дата_закупки;
            закупка.Дата_поставки = закупка.Дата_поставки;

            return View(закупка);
        }

        // POST: Закупка/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, accountant")]
        public ActionResult Edit([Bind(Include = "ID_закупки,Общая_цена,Дата_закупки,Дата_поставки")] Закупка закупка)
        {
            if (закупка.Дата_закупки > закупка.Дата_поставки)
            {
                ModelState.AddModelError("Дата_закупки", "Некорректная дата закупки");
            }

            if (ModelState.IsValid)
            {
                db.Entry(закупка).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(закупка);
        }

        // GET: Закупка/Delete/5
        [Authorize(Roles = "admin, accountant")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Закупка закупка = db.Закупка.Find(id);
            if (закупка == null)
            {
                return HttpNotFound();
            }
            return View(закупка);
        }

        // POST: Закупка/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, accountant")]
        public ActionResult DeleteConfirmed(int id)
        {
            Закупка закупка = db.Закупка.Find(id);
            db.Закупка.Remove(закупка);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FullTableZak()
        {
            var zakupki = from s in db.Закупка select s;
            return View(zakupki.ToList());
        }

        public ActionResult UpdateTableZak()
        {
            var zakupki = from s in db.Закупка select s;
            foreach (var item in zakupki)
            {
                int? overall_price = 0;
                foreach(var itemZak in item.Состав_закупки)
                {
                    overall_price = overall_price + (itemZak.Количество * itemZak.Цена);
                }
                if (overall_price.HasValue)
                {
                    item.Общая_цена = overall_price;
                }
            }
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
