using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;  

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.Tbl_Kategori.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(Tbl_Kategori t1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.Tbl_Kategori.Add(t1);
            db.SaveChanges();
            return RedirectToAction("Index"); 
        }
        public ActionResult Sil(int id)
        {
            var kategori = db.Tbl_Kategori.Find(id);
            db.Tbl_Kategori.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.Tbl_Kategori.Find(id);
            return View("KategoriGetir", ktgr);
        }
        public ActionResult Guncelle(Tbl_Kategori t1)
        {
            var ktg = db.Tbl_Kategori.Find(t1.KategoriId);
            ktg.KategordiAd = t1.KategordiAd;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}