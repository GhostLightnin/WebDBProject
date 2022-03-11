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
    public class ОбъектController : Controller
    {
        private Course_ProjectEntities1 db = new Course_ProjectEntities1();

        // GET: Объект
        [Authorize(Roles = "admin, manager, consultant")]
        public ActionResult Index(string sortOrder, string searchString, string searchStringDate, string searchStringTime, string searchStringType, string searchStringStatus, string searchStringOrgName)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewBag.DateSortParm = sortOrder == "date" ? "date_desc" : "date";
            ViewBag.TimeSortParm = sortOrder == "time" ? "time_desc" : "time";
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";
            ViewBag.StatusSortParm = sortOrder == "status" ? "status_desc" : "status";
            ViewBag.OrgNameSortParm = sortOrder == "OrgName" ? "OrgName_desc" : "OrgName";
            ViewBag.Cтатус_заказа = new SelectList(db.Объект, "ID объекта", "Статус_заказа");

            var объект =from s in db.Объект.Include(о => о.Клиент) select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                объект = объект.Where(s => s.Наименование_заказа.ToString().Contains(searchString));
            }
            if (!String.IsNullOrEmpty(searchStringDate))
            {
                объект = объект.Where(s => s.Дата_принятия_заказа.ToString().Contains(searchStringDate));
            }
            if (!String.IsNullOrEmpty(searchStringTime))
            {
                объект = объект.Where(s => s.Срок_выполнения_работ.ToString().Contains(searchStringTime));
            }
            if (!String.IsNullOrEmpty(searchStringType))
            {
                объект = объект.Where(s => s.Вид_работ.ToString().Contains(searchStringType));
            }
            if (!String.IsNullOrEmpty(searchStringStatus))
            {
                объект = объект.Where(s => s.Статус_заказа.ToString().Contains(searchStringStatus));
            }
            if (!String.IsNullOrEmpty(searchStringOrgName))
            {
                объект = объект.Where(s => s.Клиент.Название_организации.ToString().Contains(searchStringOrgName));
            }

            switch (sortOrder)
            {
                case "name":
                    объект = объект.OrderBy(s => s.Наименование_заказа);
                    break;
                case "date":
                    объект = объект.OrderBy(s => s.Дата_принятия_заказа);
                    break;
                case "date_desc":
                    объект = объект.OrderByDescending(s => s.Дата_принятия_заказа);
                    break;
                case "time":
                    объект = объект.OrderBy(s => s.Срок_выполнения_работ);
                    break;
                case "time_desc":
                    объект = объект.OrderByDescending(s => s.Срок_выполнения_работ);
                    break;
                case "type":
                    объект = объект.OrderBy(s => s.Вид_работ);
                    break;
                case "type_desc":
                    объект = объект.OrderByDescending(s => s.Вид_работ);
                    break;
                case "status":
                    объект = объект.OrderBy(s => s.Статус_заказа);
                    break;
                case "status_desc":
                    объект = объект.OrderByDescending(s => s.Статус_заказа);
                    break;
                case "OrgName":
                    объект = объект.OrderBy(s => s.Клиент.Название_организации);
                    break;
                case "OrgName_desc":
                    объект = объект.OrderByDescending(s => s.Клиент.Название_организации);
                    break;
                default:
                    объект = объект.OrderByDescending(s => s.Наименование_заказа);
                    break;
            }

            return View(объект.ToList());
        }

        // GET: Объект/Details/5
        [Authorize(Roles = "admin, manager, consultant")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Объект объект = db.Объект.Find(id);
            if (объект == null)
            {
                return HttpNotFound();
            }
            return View(объект);
        }

        // GET: Объект/Create
        [Authorize(Roles = "admin, manager")]
        public ActionResult Create()
        {
            ViewBag.ID_заказчика = new SelectList(db.Клиент, "ID_клиента", "Название_организации");
            return View();
        }

        // POST: Объект/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public ActionResult Create([Bind(Include = "ID_объекта,Наименование_заказа,ID_заказчика,Дата_принятия_заказа,Срок_выполнения_работ,Вид_работ,Статус_заказа")] Объект объект)
        {
            if (ModelState.IsValid)
            {
                db.Объект.Add(объект);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_заказчика = new SelectList(db.Клиент, "ID_клиента", "Название_организации", объект.ID_заказчика);
            return View(объект);
        }

        // GET: Объект/Edit/5
        [Authorize(Roles = "admin, manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Объект объект = db.Объект.Find(id);
            if (объект == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_заказчика = new SelectList(db.Клиент, "ID_клиента", "Название_организации", объект.ID_заказчика);
            return View(объект);
        }

        // POST: Объект/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public ActionResult Edit([Bind(Include = "ID_объекта,Наименование_заказа,ID_заказчика,Дата_принятия_заказа,Срок_выполнения_работ,Вид_работ,Статус_заказа")] Объект объект)
        {
            if (ModelState.IsValid)
            {
                db.Entry(объект).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_заказчика = new SelectList(db.Клиент, "ID_клиента", "Название_организации", объект.ID_заказчика);
            return View(объект);
        }

        // GET: Объект/Delete/5
        [Authorize(Roles = "admin, manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Объект объект = db.Объект.Find(id);
            if (объект == null)
            {
                return HttpNotFound();
            }
            return View(объект);
        }

        // POST: Объект/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            Объект объект = db.Объект.Find(id);
            db.Объект.Remove(объект);
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
