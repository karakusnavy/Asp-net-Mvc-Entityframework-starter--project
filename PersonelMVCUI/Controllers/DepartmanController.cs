using PersonelMVCUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonelMVCUI.Controllers
{
    [Authorize]
    public class DepartmanController : Controller
    {
        PersonelDbEntities1 db = new PersonelDbEntities1();
        
        public ActionResult Index()
        {
            var model = db.Departman.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Yeni()
        {

            return View("DepartmanForm", new Departman()/* id değerinin oraya 0 olarak gitmesini sağlıyoruz */);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Departman departman)
        {
            if (!ModelState.IsValid)//veritabanına veriyi eklerken sıkıntı çıkarsa yani yanlış veri girdiyse)
            {
                return View("DepartmanForm");//sayfayı yenile
            }
            if (departman.Id==0)
            {
                db.Departman.Add(departman);
            }
            else
            {
                var guncellenecekDepartman = db.Departman.Find(departman.Id);
                if (guncellenecekDepartman==null)
                {
                    return HttpNotFound();
                }
                guncellenecekDepartman.Ad = departman.Ad;
                
            }
             
            db.SaveChanges();
            return RedirectToAction("Index","Departman");
        }
      
        public ActionResult Guncelle(int Id)
        {
            var model = db.Departman.Find(Id);
            if (model==null)
            {
                return HttpNotFound();
            }
            return View("DepartmanForm", model);
        }
    
        public ActionResult Sil(int Id)
        {
            var silinecekDepartman = db.Departman.Find(Id);
            if (silinecekDepartman==null)
            {
                return HttpNotFound();
            }
            db.Departman.Remove(silinecekDepartman);
            db.SaveChanges();
            return RedirectToAction("Index", "Departman");
        }
        

    }
}