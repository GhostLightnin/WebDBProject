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
    public class СотрудникController : Controller
    {
        private Course_ProjectEntities1 db = new Course_ProjectEntities1();

        // GET: Сотрудник
        [Authorize(Roles = "admin, consultant, accountant")]
        public ActionResult Index(string sortOrder, string searchStringFIO, string searchStringTime, string searchStringPhone, string searchStringOkl, string searchStringRole)
        {
            ViewBag.RoleSortParm = sortOrder == "role" ? "role_desc" : "role";
            ViewBag.FIOSortParm = String.IsNullOrEmpty(sortOrder) ? "FIO_desc" : "";
            ViewBag.PhoneSortParm = sortOrder == "phone" ? "phone_desc" : "phone";
            ViewBag.TimeSortParm = sortOrder == "time" ? "time_desc" : "time";
            ViewBag.OklSortParm = sortOrder == "okl" ? "okl_desc" : "okl";

            var collegues = from s in db.Сотрудник select s;
            if (!String.IsNullOrEmpty(searchStringFIO))
            {
                collegues = collegues.Where(s => s.ФИО.ToString().Contains(searchStringFIO));
            }
            if (!String.IsNullOrEmpty(searchStringRole))
            {
                collegues = collegues.Where(s => s.Должность.ToString().Contains(searchStringRole));
            }
            if (!String.IsNullOrEmpty(searchStringPhone))
            {
                collegues = collegues.Where(s => s.Телефон.ToString().Contains(searchStringPhone));
            }
            if (!String.IsNullOrEmpty(searchStringOkl))
            {
                collegues = collegues.Where(s => s.Оклад.ToString().Contains(searchStringOkl));
            }
            if (!String.IsNullOrEmpty(searchStringTime))
            {
                collegues = collegues.Where(s => s.Стаж.ToString().Contains(searchStringTime));
            }
            
            switch (sortOrder)
            {
                case "okl_desc":
                    collegues = collegues.OrderByDescending(s => s.Оклад);
                    break;
                case "okl":
                    collegues = collegues.OrderBy(s => s.Оклад);
                    break;
                case "phone":
                    collegues = collegues.OrderBy(s => s.Телефон);
                    break;
                case "phone_desc":
                    collegues = collegues.OrderByDescending(s => s.Телефон);
                    break;
                case "time":
                    collegues = collegues.OrderBy(s => s.Стаж);
                    break;
                case "time_desc":
                    collegues = collegues.OrderByDescending(s => s.Стаж);
                    break;
                case "role":
                    collegues = collegues.OrderBy(s => s.Должность);
                    break;
                case "role_desc":
                    collegues = collegues.OrderByDescending(s => s.Должность);
                    break;
                case "FIO_desc":
                    collegues = collegues.OrderByDescending(s => s.ФИО);
                    break;
                default:
                    collegues = collegues.OrderBy(s => s.ФИО);
                    break;
            }
            return View(collegues.ToList());
        }

        // GET: Сотрудник/Details/5
        [Authorize(Roles = "admin, consultant, accountant")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сотрудник сотрудник = db.Сотрудник.Find(id);
            if (сотрудник == null)
            {
                return HttpNotFound();
            }
            return View(сотрудник);
        }

        // GET: Сотрудник/Create
        [Authorize(Roles = "admin,  accountant")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Сотрудник/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,  accountant")]
        public ActionResult Create([Bind(Include = "ID_сотрудника,ФИО,Должность,Стаж,Телефон,Оклад")] Сотрудник сотрудник)
        {
            if(сотрудник.Стаж > 60)
            {
                ModelState.AddModelError("Стаж", "Стаж сотрудника должен быть не более 60 лет");
            }

            if (сотрудник.Стаж <0)
            {
                ModelState.AddModelError("Стаж", "Стаж сотрудника не может быть отрицательным");
            }

            if (сотрудник.Оклад < 12298)
            {
                ModelState.AddModelError("Оклад", "Оклад сотрудника должен быть более 12298 рублей");
            }

            if (ModelState.IsValid)
            {
                db.Сотрудник.Add(сотрудник);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(сотрудник);
        }

        // GET: Сотрудник/Edit/5
        [Authorize(Roles = "admin,  accountant")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сотрудник сотрудник = db.Сотрудник.Find(id);
            if (сотрудник == null)
            {
                return HttpNotFound();
            }
            return View(сотрудник);
        }

        // POST: Сотрудник/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,  accountant")]
        public ActionResult Edit([Bind(Include = "ID_сотрудника,ФИО,Должность,Стаж,Телефон,Оклад")] Сотрудник сотрудник)
        {

            if (сотрудник.Стаж > 60)
            {
                ModelState.AddModelError("Стаж", "Стаж сотрудника должен быть не более 60 лет");
            }

            if (сотрудник.Оклад < 12298)
            {
                ModelState.AddModelError("Оклад", "Оклад сотрудника должен быть более 12298 рублей");
            }

            if (ModelState.IsValid)
            {
                db.Entry(сотрудник).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(сотрудник);
        }

        // GET: Сотрудник/Delete/5
        [Authorize(Roles = "admin,  accountant")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Сотрудник сотрудник = db.Сотрудник.Find(id);
            if (сотрудник == null)
            {
                return HttpNotFound();
            }
            return View(сотрудник);
        }

        // POST: Сотрудник/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,  accountant")]
        public ActionResult DeleteConfirmed(int id)
        {
            Сотрудник сотрудник = db.Сотрудник.Find(id);
            db.Сотрудник.Remove(сотрудник);
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
