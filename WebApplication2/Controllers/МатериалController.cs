using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Office.Interop.Word;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class МатериалController : Controller
    {
        private Course_ProjectEntities1 db = new Course_ProjectEntities1();

        // GET: Материал
        [Authorize(Roles = "admin, storage_worker, consultant")]
        public ActionResult Index(string sortOrder, string searchString, string searchStringKol, string searchStringType, string searchStringIzmer, string searchStringMin)

        {
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";
            ViewBag.KolSortParm =  sortOrder == "kol"? "kol_desc" : "kol";
            ViewBag.IzmerSortParm = sortOrder == "izmer" ? "ismer_desc" : "izmer";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.MinSortParm = sortOrder == "min" ? "min_desc" : "min";
            var materials = from s in db.Материал select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                materials = materials.Where(s => s.Наименование.ToString().Contains(searchString));
            }
            if (!String.IsNullOrEmpty(searchStringKol))
            {
                materials = materials.Where(s => s.Количество.ToString().Contains(searchStringKol));
            }
            if (!String.IsNullOrEmpty(searchStringType))
            {
                materials = materials.Where(s => s.Вид_товара.ToString().Contains(searchStringType));
            }
            if (!String.IsNullOrEmpty(searchStringIzmer))
            {
                materials = materials.Where(s => s.Единицы_измерения.ToString().Contains(searchStringIzmer));
            }
            if (!String.IsNullOrEmpty(searchStringMin))
            {
                materials = materials.Where(s => s.Минимальное_количество.ToString().Contains(searchStringMin));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    materials = materials.OrderByDescending(s => s.Наименование);
                    break;
                case "kol":
                    materials = materials.OrderBy(s => s.Вид_товара);
                    break;
                case "kol_desc":
                    materials = materials.OrderByDescending(s => s.Вид_товара);
                    break;
                case "min":
                    materials = materials.OrderBy(s => s.Единицы_измерения);
                    break;
                case "min_desc":
                    materials = materials.OrderByDescending(s => s.Единицы_измерения);
                    break;
                case "izmer":
                    materials = materials.OrderBy(s => s.Количество);
                    break;
                case "izmer_desc":
                    materials = materials.OrderByDescending(s => s.Количество);
                    break;
                case "type":
                    materials = materials.OrderBy(s => s.Минимальное_количество);
                    break;
                case "type_desc":
                    materials = materials.OrderByDescending(s => s.Минимальное_количество);
                    break;
                default:
                    materials = materials.OrderBy(s => s.Наименование);
                    break;
            }
                    return View(materials.ToList());
        }

        // GET: Материал/Details/5
        [Authorize(Roles = "admin, storage_worker, consultant")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Материал материал = db.Материал.Find(id);
            if (материал == null)
            {
                return HttpNotFound();
            }
            return View(материал);
        }

        // GET: Материал/Create
        [Authorize(Roles = "admin, storage_worker")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Материал/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, storage_worker")]
        public ActionResult Create([Bind(Include = "ID_материала,Наименование,Вид_товара,Количество,Единицы_измерения,Минимальное_количество")] Материал материал)
        {
            if (материал.Количество <= 0)
            {
                ModelState.AddModelError("Количество", "Количество не может быть отрицательным или равняться нулю");
            }

            if (материал.Минимальное_количество <= 0)
            {
                ModelState.AddModelError("Минимальное_количество", "Количество не может быть отрицательным или равняться нулю");
            }

            if (ModelState.IsValid)
            {
                db.Материал.Add(материал);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(материал);
        }

        // GET: Материал/Edit/5
        [Authorize(Roles = "admin, storage_worker")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Материал материал = db.Материал.Find(id);
            if (материал == null)
            {
                return HttpNotFound();
            }
            return View(материал);
        }

        // POST: Материал/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, storage_worker")]
        public ActionResult Edit([Bind(Include = "ID_материала,Наименование,Вид_товара,Количество,Единицы_измерения,Минимальное_количество")] Материал материал)
        {

            if (материал.Количество <= 0)
            {
                ModelState.AddModelError("Количество", "Количество не может быть отрицательным или равняться нулю");
            }

            if (материал.Минимальное_количество <= 0)
            {
                ModelState.AddModelError("Минимальное_количество", "Количество не может быть отрицательным или равняться нулю");
            }

            if (ModelState.IsValid)
            {
                db.Entry(материал).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(материал);
        }

        // GET: Материал/Delete/5
        [Authorize(Roles = "admin, storage_worker")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Материал материал = db.Материал.Find(id);
            if (материал == null)
            {
                return HttpNotFound();
            }
            return View(материал);
        }

        // POST: Материал/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, storage_worker")]
        public ActionResult DeleteConfirmed(int id)
        {
            Материал материал = db.Материал.Find(id);
            db.Материал.Remove(материал);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult PrintToWord()
        {
            try { 
                //var материал = from s in db.Материал select s;
                List<Материал> listOfNeededMaterials = new List<Материал>();
                foreach(var материал in db.Материал)
                {
                    if (материал.Количество < материал.Минимальное_количество)
                    {
                        listOfNeededMaterials.Add(материал);
                    }
                }
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
                winword.ShowAnimation = false;
                winword.Visible = true;
                object missing = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                Table firstTable = document.Tables.Add(para1.Range, listOfNeededMaterials.Count + 1, 4, ref missing, ref missing);
                firstTable.Borders.Enable = 1;
                foreach (Row row in firstTable.Rows)
                {
                    foreach (Cell cell in row.Cells)
                    {
                        if (cell.RowIndex == 1)
                        {
                            cell.Range.Font.Bold = 1;
                            //other format properties goes here  
                            cell.Range.Font.Name = "verdana";
                            cell.Range.Font.Size = 10;
                            cell.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
                            cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            if (cell.ColumnIndex == 1)
                            {
                                cell.Range.Text = "Наименование";
                            }
                            if (cell.ColumnIndex == 2)
                            {
                                cell.Range.Text = "Вид товара";
                            }
                            if (cell.ColumnIndex == 3)
                            {
                                cell.Range.Text = "Необходимое количество";
                            }
                            if (cell.ColumnIndex == 4)
                            {
                                cell.Range.Text = "Единицы измерения";
                            }
                        }
                        else
                        {
                            if (cell.ColumnIndex == 1)
                            {
                                cell.Range.Text = listOfNeededMaterials.GetRange(cell.RowIndex-2, 1).First().Наименование.ToString(); ;
                            }
                            if (cell.ColumnIndex == 2)
                            {
                                cell.Range.Text = listOfNeededMaterials.GetRange(cell.RowIndex-2, 1).First().Вид_товара.ToString();
                            }
                            if (cell.ColumnIndex == 3)
                            {
                                cell.Range.Text = (listOfNeededMaterials.GetRange(cell.RowIndex-2, 1).First().Минимальное_количество.Value - listOfNeededMaterials.GetRange(cell.RowIndex-2, 1).First().Количество.Value).ToString();
                            }
                            if (cell.ColumnIndex == 4)
                            {
                                cell.Range.Text = listOfNeededMaterials.GetRange(cell.RowIndex-2, 1).First().Единицы_измерения.ToString();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Материал", "Не удалось создать файл");
            }
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
