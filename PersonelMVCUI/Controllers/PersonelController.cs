using PersonelMVCUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PersonelMVCUI.ViewModels;

namespace PersonelMVCUI.Controllers
{
    [Authorize]
    public class PersonelController : Controller
    {
        PersonelDbEntities1 db = new PersonelDbEntities1();
        public ActionResult Index()
        {
            var model = db.Personel.Include(x=>x.Departman).ToList();//DepartmanId lere direk adı yazma
            return View(model);
        }
        
        public ActionResult Yeni()
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Personel = new Personel()
            };
            return View("PersonelForm",model);
        }
        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Personel personel)
        {
            if (!ModelState.IsValid)
            {
                var model = new PersonelFormViewModel()
                {
                    Departmanlar = db.Departman.ToList(),
                    Personel = personel
                };
                return View("PersonelForm",model);
            }
            if (personel.Id == 0)//Ekleme yaptırmak isteniyor.
            {
                db.Personel.Add(personel);
            }
            else//guncelleme
            {
                var guncellenecekPersonel = db.Personel.Find(personel.Id);
                if (guncellenecekPersonel == null)
                    return HttpNotFound();
                //db.Entry(personel).State = System.Data.Entity.EntityState.Modified;
                guncellenecekPersonel.Ad = personel.Ad;
                guncellenecekPersonel.Soyad = personel.Soyad;
                guncellenecekPersonel.Maas = personel.Maas;
                guncellenecekPersonel.EvliMi = personel.EvliMi;
                guncellenecekPersonel.DogumTarihi = personel.DogumTarihi;
                guncellenecekPersonel.cinsiyet = personel.cinsiyet;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    
        public ActionResult Guncelle(int id)
        {
            var model = new PersonelFormViewModel(){
                Departmanlar = db.Departman.ToList(),
                Personel = db.Personel.Find(id)
            };
            return View("PersonelForm",model);
        }
        
        public ActionResult Sil(int Id)
        {
            var silinecekPersonel = db.Personel.Find(Id);
            if (silinecekPersonel==null)
            {
                return HttpNotFound();
            }
            db.Personel.Remove(db.Personel.Find(Id));
            db.SaveChanges();
            return RedirectToAction("Index","Personel");
        }
    }
}