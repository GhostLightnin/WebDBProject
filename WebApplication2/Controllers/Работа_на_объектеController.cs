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
    public class Работа_на_объектеController : Controller
    {
        private Course_ProjectEntities1 db = new Course_ProjectEntities1();

        // GET: Работа_на_объекте
        [Authorize(Roles = "admin, manager")]
        public ActionResult Index(int? usedID)
        {
            var работа_на_объекте = db.Работа_на_объекте.Include(р => р.Объект).Include(р => р.Сотрудник);

            if (usedID.HasValue)
            {
                работа_на_объекте = работа_на_объекте.Where(s => s.ID_объекта == usedID);
            }

            работа_на_объекте = работа_на_объекте.OrderByDescending(s => s.Сотрудник.ФИО);

            ViewBag.usedIParm = usedID;

            return View(работа_на_объекте.ToList());
        }

        // GET: Работа_на_объекте/Details/5
        [Authorize(Roles = "admin, manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Работа_на_объекте работа_на_объекте = db.Работа_на_объекте.Find(id);
            if (работа_на_объекте == null)
            {
                return HttpNotFound();
            }
            return View(работа_на_объекте);
        }

        // GET: Работа_на_объекте/Create
        [Authorize(Roles = "admin, manager")]
        public ActionResult Create(int? usedId)
        {
            ViewBag.ID_объекта = new SelectList(db.Объект.Where(s => s.ID_объекта == usedId), "ID_объекта", "Наименование_заказа");
            ViewBag.ID_сотрудника = new SelectList(db.Сотрудник.OrderBy(s => s.ФИО), "ID_сотрудника", "ФИО");
            return View();
        }

        // POST: Работа_на_объекте/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public ActionResult Create([Bind(Include = "ID,ID_объекта,ID_сотрудника,Выполняемая_работа")] Работа_на_объекте работа_на_объекте)
        {
            if (ModelState.IsValid)
            {
                db.Работа_на_объекте.Add(работа_на_объекте);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_объекта = new SelectList(db.Объект, "ID_объекта", "Наименование_заказа", работа_на_объекте.ID_объекта);
            ViewBag.ID_сотрудника = new SelectList(db.Сотрудник, "ID_сотрудника", "ФИО", работа_на_объекте.ID_сотрудника);
            return View(работа_на_объекте);
        }

        // GET: Работа_на_объекте/Edit/5
        [Authorize(Roles = "admin, manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Работа_на_объекте работа_на_объекте = db.Работа_на_объекте.Find(id);
            if (работа_на_объекте == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_объекта = new SelectList(db.Объект, "ID_объекта", "Наименование_заказа", работа_на_объекте.ID_объекта);
            ViewBag.ID_сотрудника = new SelectList(db.Сотрудник.OrderBy(s => s.ФИО), "ID_сотрудника", "ФИО", работа_на_объекте.ID_сотрудника);
            return View(работа_на_объекте);
        }

        // POST: Работа_на_объекте/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public ActionResult Edit([Bind(Include = "ID,ID_объекта,ID_сотрудника,Выполняемая_работа")] Работа_на_объекте работа_на_объекте)
        {
            if (ModelState.IsValid)
            {
                db.Entry(работа_на_объекте).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_объекта = new SelectList(db.Объект, "ID_объекта", "Наименование_заказа", работа_на_объекте.ID_объекта);
            ViewBag.ID_сотрудника = new SelectList(db.Сотрудник, "ID_сотрудника", "ФИО", работа_на_объекте.ID_сотрудника);
            return View(работа_на_объекте);
        }

        // GET: Работа_на_объекте/Delete/5
        [Authorize(Roles = "admin, manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Работа_на_объекте работа_на_объекте = db.Работа_на_объекте.Find(id);
            if (работа_на_объекте == null)
            {
                return HttpNotFound();
            }
            return View(работа_на_объекте);
        }

        // POST: Работа_на_объекте/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            Работа_на_объекте работа_на_объекте = db.Работа_на_объекте.Find(id);
            db.Работа_на_объекте.Remove(работа_на_объекте);
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
