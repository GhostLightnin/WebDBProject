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
    public class Состав_закупкиController : Controller
    {
        private Course_ProjectEntities1 db = new Course_ProjectEntities1();
        private int? usedZakId = null;

        // GET: Состав_закупки
        [Authorize(Roles = "admin, accountant")]
        public ActionResult Index(int? usedId)
        {
            var состав_закупки = db.Состав_закупки.Include(с => с.Закупка).Include(с => с.Материал);
            ViewBag.usedIParm = usedId;
            if (usedId.HasValue)
            {
                состав_закупки = состав_закупки.Where(s => s.ID_закупки == usedId);
            }
            return View(состав_закупки.ToList());
        }

        // GET: Состав_закупки/Details/5
        [Authorize(Roles = "admin, accountant")]
        public ActionResult Details(int? id1, int? id2)
        {
            ViewBag.usedIParm = id1;
            if ((id1 == null) || (id2 == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Состав_закупки состав_закупки = db.Состав_закупки.Find(id1, id2);
            if (состав_закупки == null)
            {
                return HttpNotFound();
            }
            return View(состав_закупки);
        }

        // GET: Состав_закупки/Create
        [Authorize(Roles = "admin, accountant")]
        public ActionResult Create(int? usedId)
        {
            ViewBag.usedIParm = usedId;
            usedZakId = usedId;
            ViewBag.ID_закупки = new SelectList(db.Закупка, "ID_закупки", "ID_закупки", usedId);
            ViewBag.ID_материала = new SelectList(db.Материал, "ID_материала", "Наименование");
            return View();
        }

        // POST: Состав_закупки/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, accountant")]
        public ActionResult Create([Bind(Include = "ID_закупки,ID_материала,Количество,Цена")] Состав_закупки состав_закупки)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Состав_закупки.Add(состав_закупки);
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("ID_закупки", "Неправильно заполнены поля");
                }

                return RedirectToAction("Index", new { usedId = состав_закупки.ID_закупки});
            }

            ViewBag.ID_закупки = new SelectList(db.Закупка, "ID_закупки", "ID_закупки", состав_закупки.ID_закупки);
            ViewBag.ID_материала = new SelectList(db.Материал, "ID_материала", "Наименование", состав_закупки.ID_материала);
            return View(состав_закупки);
        }

        // GET: Состав_закупки/Edit/5
        [Authorize(Roles = "admin, accountant")]
        public ActionResult Edit(int? id1, int? id2)
        {
;
            if ((id1 == null) || (id2 == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Состав_закупки состав_закупки = db.Состав_закупки.Find(id1, id2);
            if (состав_закупки == null)
            {
                return HttpNotFound();
            }

            usedZakId = id1;
            ViewBag.usedIParm = id1;
            ViewBag.ID_закупки = new SelectList(db.Закупка, "ID_закупки", "ID_закупки", состав_закупки.ID_закупки);
            ViewBag.ID_материала = new SelectList(db.Материал, "ID_материала", "Наименование", состав_закупки.ID_материала);
            return View(состав_закупки);
        }

        // POST: Состав_закупки/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, accountant")]
        public ActionResult Edit([Bind(Include = "ID_закупки,ID_материала,Количество,Цена")] Состав_закупки состав_закупки)
        {
            
            
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(состав_закупки).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("ID_закупки", "Неправильно заполнены поля");
                }
                return RedirectToAction("Index", new { usedId = состав_закупки.ID_закупки });
            }
            ViewBag.ID_закупки = new SelectList(db.Закупка, "ID_закупки", "ID_закупки", состав_закупки.ID_закупки);
            ViewBag.ID_материала = new SelectList(db.Материал, "ID_материала", "Наименование", состав_закупки.ID_материала);
            return View(состав_закупки);
        }

        // GET: Состав_закупки/Delete/5
        [Authorize(Roles = "admin, accountant")]
        public ActionResult Delete(int? id1, int? id2)
        {
            usedZakId = id1;
            if ((id1 == null) || (id2 == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Состав_закупки состав_закупки = db.Состав_закупки.Find(id1, id2);
            if (состав_закупки == null)
            {
                return HttpNotFound();
            }
            return View(состав_закупки);
        }

        // POST: Состав_закупки/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, accountant")]
        public ActionResult DeleteConfirmed(int? id1, int? id2)
        {
            
            Состав_закупки состав_закупки = db.Состав_закупки.Find(id1, id2);
            int? id3 = состав_закупки.ID_закупки;
            db.Состав_закупки.Remove(состав_закупки);
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
