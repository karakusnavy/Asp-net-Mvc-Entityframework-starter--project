using PersonelMVCUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PersonelMVCUI.Controllers
{
    public class SecurityController : Controller
    {
        // GET: Security
        public ActionResult Login()
        {
            return View();
        }
        PersonelDbEntities1 db = new PersonelDbEntities1();
        [HttpPost]
        public ActionResult Login(Kullanici kullanici)
        {
            var kullaniciInDb = db.Kullanici.FirstOrDefault(x => x.Ad == kullanici.Ad && x.Sifre == kullanici.Sifre);
            if (kullaniciInDb!=null)
            {
                ViewBag.mesaj = "Giriş Başarılı";
                FormsAuthentication.SetAuthCookie(kullaniciInDb.Ad,false);
                return RedirectToAction("Index","Departman");
            }
            else
            {
                ViewBag.mesaj = "Geçersiz Kullanıcı Adı veya Şifre";
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Security");
        }

    }
}