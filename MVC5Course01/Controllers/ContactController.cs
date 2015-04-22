using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course01.Models;

namespace MVC5Course01.Controllers
{
    public class ContactController : Controller
    {
        private Entities db = new Entities();

        // GET: Contact
        public ActionResult Index()
        {
            var 客戶聯絡人 = db.客戶聯絡人.Where(i=>i.IsValid==true).Include(客 => 客.客戶資料);
            return View(客戶聯絡人.ToList());
        }

        // GET: Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料.Where(i=>i.IsValid==true), "Id", "客戶名稱");
            return View();
        }

        // POST: Contact/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,IsValid")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                客戶聯絡人.IsValid = true;
                db.客戶聯絡人.Add(客戶聯絡人);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料.Where(i => i.IsValid == true), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: Contact/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            //db.客戶聯絡人.Remove(客戶聯絡人);
            客戶聯絡人.IsValid = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        /// <summary>
        /// 檢查此客戶的客戶聯絡人是否已有此Email
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="客戶Id"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual JsonResult doesEmailExist(string Email, int 客戶Id)
        {
            var user = db.客戶聯絡人.Where(i => i.IsValid == true && i.Email == Email && i.客戶Id == 客戶Id).FirstOrDefault();
            bool result = true;
            if (user != null)
            {
                return Json("此Email已被使用", JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
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
