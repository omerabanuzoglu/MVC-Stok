using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa = 1)
        {
            //var degerler = db.Tbl_Urunler.ToList();
            var degerler = db.Tbl_Kategori.ToList().ToPagedList(sayfa, 7);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.Tbl_Kategori.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategordiAd,
                                                 Value = i.KategoriId.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(Tbl_Urunler t1)
        {
            var ktg = db.Tbl_Kategori.Where(m => m.KategoriId == t1.Tbl_Kategori.KategoriId).FirstOrDefault();
            t1.Tbl_Kategori = ktg;
            db.Tbl_Urunler.Add(t1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var urun = db.Tbl_Urunler.Find(id);
            db.Tbl_Urunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.Tbl_Urunler.Find(id);
            return View("UrunGetir", urun);
        }
        public ActionResult Guncelle(Tbl_Urunler t1)
        {
            var gncl = db.Tbl_Urunler.Find(t1.UrunId);
            gncl.UrunAd = t1.UrunAd;
            gncl.UrunKategori = t1.UrunKategori;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}