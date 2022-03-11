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
    public class Этап_выполненияController : Controller
    {
        private Course_ProjectEntities1 db = new Course_ProjectEntities1();

        // GET: Этап_выполнения
        [Authorize(Roles = "admin, manager")]
        public ActionResult Index(int? usedID)
        {
            ViewBag.usedIParm = usedID;
            var этап_выполнения = db.Этап_выполнения.Include(э => э.Объект);
            if (usedID.HasValue)
            {
                этап_выполнения = этап_выполнения.Where(s => s.ID_объекта == usedID);
            }
            return View(этап_выполнения.ToList());
        }

        // GET: Этап_выполнения/Details/5
        [Authorize(Roles = "admin, manager")]
        public ActionResult Details(int? id1, int? id2)
        {

            ViewBag.usedIParm = id2;
            if ((id1 == null) || (id2 == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Этап_выполнения этап_выполнения = db.Этап_выполнения.Find(id2, id1);
            if (этап_выполнения == null)
            {
                return HttpNotFound();
            }
            return View(этап_выполнения);
        }

        // GET: Этап_выполнения/Create
        [Authorize(Roles = "admin, manager")]
        public ActionResult Create(int? usedId)
        {
            ViewBag.usedIParm = usedId;
            ViewBag.ID_объекта = new SelectList(db.Объект.Where(s => s.ID_объекта == usedId), "ID_объекта", "Наименование_заказа");
            return View();
        }

        // POST: Этап_выполнения/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public ActionResult Create([Bind(Include = "ID_объекта,ID_этапа,Дата,Название_этапа")] Этап_выполнения этап_выполнения)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Этап_выполнения.Add(этап_выполнения);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ID_закупки", "Неправильно заполнены поля");
                }
                return RedirectToAction("Index", new { usedId = этап_выполнения.ID_объекта });
            }

            ViewBag.ID_объекта = new SelectList(db.Объект, "ID_объекта", "Наименование_заказа", этап_выполнения.ID_объекта);
            return View(этап_выполнения);
        }

        // GET: Этап_выполнения/Edit/5
        [Authorize(Roles = "admin, manager")]
        public ActionResult Edit(int? id1, int? id2)
        {
            ViewBag.usedIParm = id2;
            if ((id1 == null)||(id2 == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Этап_выполнения этап_выполнения = db.Этап_выполнения.Find(id2, id1);
            if (этап_выполнения == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_объекта = new SelectList(db.Объект.Where(s =>s.ID_объекта == id2), "ID_объекта", "Наименование_заказа", этап_выполнения.ID_объекта);
            return View(этап_выполнения);
        }

        // POST: Этап_выполнения/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public ActionResult Edit([Bind(Include = "ID_объекта,ID_этапа,Дата,Название_этапа")] Этап_выполнения этап_выполнения)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(этап_выполнения).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ID_объекта", "Неправильно заполнены поля");
                }
                    return RedirectToAction("Index", new { usedId = этап_выполнения.ID_объекта });
            }
            ViewBag.ID_объекта = new SelectList(db.Объект, "ID_объекта", "Наименование_заказа", этап_выполнения.ID_объекта);
            return View(этап_выполнения);
        }

        // GET: Этап_выполнения/Delete/5
        [Authorize(Roles = "admin, manager")]
        public ActionResult Delete(int? id1, int? id2)
        {
            ViewBag.usedIParm = id2;
            if ((id1 == null) || (id2 == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Этап_выполнения этап_выполнения = db.Этап_выполнения.Find(id2, id1);
            if (этап_выполнения == null)
            {
                return HttpNotFound();
            }
            return View(этап_выполнения);
        }

        // POST: Этап_выполнения/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public ActionResult DeleteConfirmed(int? id1, int? id2)
        {
            Этап_выполнения этап_выполнения = db.Этап_выполнения.Find(id1, id2);
            int? id3 = этап_выполнения.ID_объекта;
            db.Этап_выполнения.Remove(этап_выполнения);
            db.SaveChanges();
            return RedirectToAction("Index", new { usedId = id3 });
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
