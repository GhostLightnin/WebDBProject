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
    public class КлиентController : Controller
    {
        private Course_ProjectEntities1 db = new Course_ProjectEntities1();

        // GET: Клиент
        [Authorize(Roles = "admin, manager, consultant")]
        public ActionResult Index(string sortOrder, string searchStringCity, string searchStringAddress, string searchStringPhone, string searchStringContact, string searchStringMail, string searchStringName)
        {
            ViewBag.CitySortParm = sortOrder == "city" ? "city_desc" : "city";
            ViewBag.AddressSortParm = sortOrder == "address" ? "address_desc" : "address";
            ViewBag.PhoneSortParm = sortOrder == "phone" ? "phone_desc" : "phone";
            ViewBag.MailSortParm = sortOrder == "mail" ? "mail_desc" : "mail";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ContactSortParm = sortOrder == "contact" ? "contact_desc" : "contact";

            var clients = from s in db.Клиент select s;
            if (!String.IsNullOrEmpty(searchStringCity))
            {
                clients = clients.Where(s => s.Город.ToString().Contains(searchStringCity));
            }
            if (!String.IsNullOrEmpty(searchStringAddress))
            {
                clients = clients.Where(s => s.Адрес.ToString().Contains(searchStringAddress));
            }
            if (!String.IsNullOrEmpty(searchStringPhone))
            {
                clients = clients.Where(s => s.Телефон.ToString().Contains(searchStringPhone));
            }
            if (!String.IsNullOrEmpty(searchStringMail))
            {
                clients = clients.Where(s => s.Почта.ToString().Contains(searchStringMail));
            }
            if (!String.IsNullOrEmpty(searchStringName))
            {
                clients = clients.Where(s => s.Название_организации.ToString().Contains(searchStringName));
            }
            if (!String.IsNullOrEmpty(searchStringContact))
            {
                clients = clients.Where(s => s.Контактное_лицо.ToString().Contains(searchStringContact));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    clients = clients.OrderByDescending(s => s.Название_организации);
                    break;
                case "phone":
                    clients = clients.OrderBy(s => s.Телефон);
                    break;
                case "phone_desc":
                    clients = clients.OrderByDescending(s => s.Телефон);
                    break;
                case "contact":
                    clients = clients.OrderBy(s => s.Контактное_лицо);
                    break;
                case "contact_desc":
                    clients = clients.OrderByDescending(s => s.Контактное_лицо);
                    break;
                case "mail":
                    clients = clients.OrderBy(s => s.Почта);
                    break;
                case "mail_desc":
                    clients = clients.OrderByDescending(s => s.Почта);
                    break;
                case "city":
                    clients = clients.OrderBy(s => s.Город);
                    break;
                case "city_desc":
                    clients = clients.OrderByDescending(s => s.Город);
                    break;
                case "address":
                    clients = clients.OrderBy(s => s.Адрес);
                    break;
                case "address_desc":
                    clients = clients.OrderByDescending(s => s.Адрес);
                    break;
                default:
                    clients = clients.OrderBy(s => s.Название_организации);
                    break;
            }
            return View(clients.ToList());
        }

        // GET: Клиент/Details/5
        [Authorize(Roles = "admin, manager, consultant")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Клиент клиент = db.Клиент.Find(id);
            if (клиент == null)
            {
                return HttpNotFound();
            }
            return View(клиент);
        }

        // GET: Клиент/Create
        [Authorize(Roles = "admin, manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Клиент/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public ActionResult Create([Bind(Include = "ID_клиента,Название_организации,Город,Адрес,Телефон,Почта,Контактное_лицо")] Клиент клиент)
        {
            
            
            
            if (ModelState.IsValid)
            {
                db.Клиент.Add(клиент);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(клиент);
        }

        // GET: Клиент/Edit/5
        [Authorize(Roles = "admin, manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Клиент клиент = db.Клиент.Find(id);
            if (клиент == null)
            {
                return HttpNotFound();
            }
            return View(клиент);
        }

        // POST: Клиент/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public ActionResult Edit([Bind(Include = "ID_клиента,Название_организации,Город,Адрес,Телефон,Почта,Контактное_лицо")] Клиент клиент)
        {
            if (ModelState.IsValid)
            {
                db.Entry(клиент).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(клиент);
        }

        // GET: Клиент/Delete/5
        [Authorize(Roles = "admin, manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Клиент клиент = db.Клиент.Find(id);
            if (клиент == null)
            {
                return HttpNotFound();
            }
            return View(клиент);
        }

        // POST: Клиент/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            Клиент клиент = db.Клиент.Find(id);
            db.Клиент.Remove(клиент);
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
