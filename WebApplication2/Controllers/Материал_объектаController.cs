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
    public class Материал_объектаController : Controller
    {
        private Course_ProjectEntities1 db = new Course_ProjectEntities1();

        // GET: Материал_объекта
        [Authorize(Roles = "admin, manager")]
        public ActionResult Index(int? usedID)
        {
            ViewBag.usedIParm = usedID;
            var материал_объекта = db.Материал_объекта.Include(м => м.Материал).Include(м => м.Объект);
            if (usedID.HasValue)
            {
                материал_объекта = материал_объекта.Where(s => s.ID_объекта == usedID);
            }
            return View(материал_объекта.ToList());
        }

        // GET: Материал_объекта/Details/5
        [Authorize(Roles = "admin, manager")]
        public ActionResult Details(int? id1, int? id2)
        {
            ViewBag.usedIParm = id1;
            if ((id1 == null) || (id2 == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Материал_объекта материал_объекта = db.Материал_объекта.Find(id1, id2);
            if (материал_объекта == null)
            {
                return HttpNotFound();
            }
            return View(материал_объекта);
        }

        // GET: Материал_объекта/Create

        [Authorize(Roles = "admin, manager")]
        public ActionResult Create(int? usedId)
        {
            ViewBag.usedIParm = usedId;
            ViewBag.ID_материала = new SelectList(db.Материал, "ID_материала", "Наименование");
            ViewBag.ID_объекта = new SelectList(db.Объект.Where(s => s.ID_объекта == usedId), "ID_объекта", "Наименование_заказа");
            return View();
        }

        // POST: Материал_объекта/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public ActionResult Create([Bind(Include = "ID_объекта,ID_материала,Единицы_измерения,Количество_материала")] Материал_объекта материал_объекта)
        {
            if(материал_объекта.Количество_материала <= 0)
            {
                ModelState.AddModelError("Количество_материала", "Количество не может быть отрицательным или равняться нулю");
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    db.Материал_объекта.Add(материал_объекта);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ID_объекта", "Неправильно заполнены поля");
                }
                return RedirectToAction("Index", new { usedId = материал_объекта.ID_объекта });
            }

            ViewBag.ID_материала = new SelectList(db.Материал, "ID_материала", "Наименование", материал_объекта.ID_материала);
            ViewBag.ID_объекта = new SelectList(db.Объект, "ID_объекта", "Наименование_заказа", материал_объекта.ID_объекта);
            return View(материал_объекта);
        }

        // GET: Материал_объекта/Edit/5
        [Authorize(Roles = "admin, manager")]
        public ActionResult Edit(int? id1, int? id2)
        {
            if ((id1 == null) || (id2 == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Материал_объекта материал_объекта = db.Материал_объекта.Find(id1, id2);
            if (материал_объекта == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_материала = new SelectList(db.Материал, "ID_материала", "Наименование", материал_объекта.ID_материала);
            ViewBag.ID_объекта = new SelectList(db.Объект.Where(s => s.ID_объекта == id1), "ID_объекта", "Наименование_заказа", материал_объекта.ID_объекта);
            return View(материал_объекта);
        }

        // POST: Материал_объекта/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public ActionResult Edit([Bind(Include = "ID_объекта,ID_материала,Единицы_измерения,Количество_материала")] Материал_объекта материал_объекта)
        {
            if (материал_объекта.Количество_материала <= 0)
            {
                ModelState.AddModelError("Количество_материала", "Количество не может быть отрицательным или равняться нулю");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(материал_объекта).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ID_объекта", "Неправильно заполнены поля");
                }
                return RedirectToAction("Index", new { usedId = материал_объекта.ID_объекта });
            }
            ViewBag.ID_материала = new SelectList(db.Материал, "ID_материала", "Наименование", материал_объекта.ID_материала);
            ViewBag.ID_объекта = new SelectList(db.Объект, "ID_объекта", "Наименование_заказа", материал_объекта.ID_объекта);
            return View(материал_объекта);
        }

        // GET: Материал_объекта/Delete/5
        [Authorize(Roles = "admin, manager")]
        public ActionResult Delete(int? id1, int? id2)
        {
            if ((id1 == null) || (id2 == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Материал_объекта материал_объекта = db.Материал_объекта.Find(id1, id2);
            if (материал_объекта == null)
            {
                return HttpNotFound();
            }
            return View(материал_объекта);
        }

        // POST: Материал_объекта/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public ActionResult DeleteConfirmed(int? id1, int? id2)
        {
            Материал_объекта материал_объекта = db.Материал_объекта.Find(id1, id2);
            int? id3 = материал_объекта.ID_объекта;
            db.Материал_объекта.Remove(материал_объекта);
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
