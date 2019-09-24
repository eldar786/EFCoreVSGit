using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore2017.EF;
using EFCore2017.EntityModels;
using Microsoft.AspNetCore.Mvc;
using EFCore2017.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace EFCore2017.Controllers { 
    public class KorisnikController : Controller
    {
        MyContext db = new MyContext();
        public IActionResult DodajForm()
        {
            KorisnikUrediVM model = new KorisnikUrediVM();
            model.Igre = db.Igre.Select(i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList();

            return View("UrediForm", model);
        }

        public IActionResult UrediForm(int KorisnikId)
        {
            MyContext db = new MyContext();
            Korisnik k = db.Korisnici.Find(KorisnikId);
            if (k == null)
            {
                TempData["porukaError"] = "Greska pri brisanju";
                return RedirectToAction(nameof(Prikazi));
            }

            KorisnikUrediVM model = new KorisnikUrediVM();
            model.Igre = db.Igre.Select(i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList();
            model.KorisnikID = k.Id;
            model.KorisnikNaziv = k.Naziv;
            model.KorisnikAdresa = k.Adresa;

            return View("UrediForm", model);
        }

        public IActionResult Snimi(KorisnikUrediVM input)
        {
            Korisnik x;
            if (input.KorisnikID == 0)
            {
                //ako je 0, kreiramo novi objekat i dodamo ga u entity framework i kad se pozove funkcija SaveChanges objekat x ce dobiti vrijednnost
                x = new Korisnik();
                //posto se radi o referenci "x" mozemo prvo dodati, pa onda setovati
                db.Add(x);
            }
            else
            {
                x = db.Korisnici.Find(input.KorisnikID);
            }

            x.Id = input.KorisnikID;
            x.Naziv = input.KorisnikNaziv;
            x.Adresa = input.KorisnikAdresa;
            x.Igra.Naziv = input.IgraNaziv;
            //x.Igra.Naziv = input.Igra.Naziv;       ***Ne snima unesene vrijednosti za IGRE***


            db.SaveChanges();

            //Da se oslobode resursi
            db.Dispose();

            TempData["porukaSuccess"] = "Uspjesno ste dodali kupca";

            return Redirect("/Korisnik/Prikazi");
        }

        public IActionResult Prikazi()
        {
            List<NekiDrugiNaziv> podatak1 = db.Korisnici.Select(k => new NekiDrugiNaziv
            {
                KorisnikNaziv = k.Naziv,
                KorisnikAdresa = k.Adresa,
                KorisnikID = k.Id,
                IgraNaziv = k.Igra.Naziv,
                IznosDobitka = k.Igra.Iznos
            })
            .ToList();

            KorisnikPrikaziVM model = new KorisnikPrikaziVM();
            model.podaciZaHtmlTabelu = podatak1;
            model.podatak2 = 5;
            model.podatak3 = "neki podatak";


            //Da se oslobode resursi
            db.Dispose();
            return View("Prikazi", model);
        }

        public IActionResult Obrisi(int KorisnikId)
        {
            Korisnik k = db.Korisnici.Find(KorisnikId);

            if (k == null)
            {
                TempData["porukaError"] = "Greska pri brisanju";
            }
            else
            {
                db.Remove(k);
                db.SaveChanges();
                TempData["porukaSuccess"] = "Uspjesno ste obrisali korisnika";
            }
            //Da se oslobode resursi
            db.Dispose();

            //RedirectToAction - ne moramo navesti kontroler (za razliku od obicnog Redirect)
            return RedirectToAction("/Prikazi");

        }
    }
}