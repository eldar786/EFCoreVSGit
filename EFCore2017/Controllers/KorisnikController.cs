using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore2017.EF;
using EFCore2017.EntityModels;
using Microsoft.AspNetCore.Mvc;
using EFCore2017.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EFCore2017.Controllers
{
    public class KorisnikController : Controller
    {
        MyContext db = new MyContext();
        public IActionResult DodajForm()
        {
            // *** MOZE SE IZOSTAVITI "DodajForm" , PA CE SE KORISTITI PO DEFAULTU NAZIV AKCIJE
            return View("DodajForm");
        }

        public IActionResult DodajSnimi(KorisnikPrikaziViewModel model)
        {

            Korisnik x = new Korisnik
            //*** KORISTIMO OBJECT INICIJALIZATOR ***
            {
                Naziv = model.Naziv,
                Adresa = model.Adresa,
                KorisnikId = model.KorisnikId
            };

            db.Add(x);
            db.SaveChanges();

            int zadnjiKorisnikId = x.KorisnikId;

            Igra y = new Igra
            {
                Naziv = model.IgraNaziv,
                Iznos = model.IznosDobitka,
                KorisnikId = zadnjiKorisnikId
            };

            db.Add(y);
            db.SaveChanges();

            db.Dispose();
            return Redirect("/Korisnik/Prikazi");
        }

        public IActionResult UrediForm(int KorisnikId)
        {
            MyContext db = new MyContext();
            Korisnik k = db.Korisnici.Find(KorisnikId);
            Igra y = db.Igre.Find(KorisnikId);
            if (k == null)
            {
                TempData["porukaError"] = "Greska pri brisanju";
                return RedirectToAction(nameof(Prikazi));
            }

            KorisnikPrikaziViewModel model = new KorisnikPrikaziViewModel();
            // Konverzija iz Igre u SELECT LIST ITEM, konvertovati svaki objekat iz Igre u objekte SELECT LIST ITEM, 
            //ima funkcija koja pojednostavljuje da ne pravimo rucno for petlju 
            //model.Igre = db.Igre.Select(i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList();
            model.KorisnikId = k.KorisnikId;
            model.Naziv = k.Naziv;
            model.Adresa = k.Adresa;
            model.IgraNaziv = y.Naziv;
            model.IznosDobitka = y.Iznos; 

            return View("UrediForm", model);
        }

        public IActionResult Snimi(KorisnikPrikaziViewModel input)
        {
            Igra y = new Igra();
            Korisnik x;
            if (input.KorisnikId == 0)
            {
                //ako je 0, kreiramo novi objekat i dodamo ga u entity framework i kad se pozove funkcija SaveChanges objekat x ce dobiti vrijednnost
                x = new Korisnik();
                //posto se radi o referenci "x" mozemo prvo dodati, pa onda setovati
                db.Add(x);
            }
            else
            {
                x = db.Korisnici.Find(input.KorisnikId);
                y = db.Igre.Find(input.KorisnikId);
            }

            x.KorisnikId = input.KorisnikId;
            x.Naziv = input.Naziv;
            x.Adresa = input.Adresa;
            y.Iznos = input.IznosDobitka;
            y.Naziv = input.IgraNaziv;

            db.SaveChanges();

            //Da se oslobode resursi
            db.Dispose();

            TempData["porukaSuccess"] = "Uspjesno ste dodali kupca";

            return Redirect("/Korisnik/Prikazi");
        }

        // *** PRVI NACIN ***
        public IActionResult Prikazi()
        {
            //*** ENTITY KLASE PRETVARAMO U VIEW MODEL KLASU (POMOCU FUNKCIJE SELECT);; Lamda expression  ili delegate ***

            //*** AKO IMAMO KONSTRUKTOR MOZEMO PROSLJEDJIVATI PARAMETRE npr (k.naziv,k.adresa...) ***
            //.Select(delegate(Korisnik k){
            //return new KorisnikPrikaziViewModel(k.Naziv,k.Adresa...); }

            List<KorisnikPrikaziViewModel> podaci = db.Korisnici
            .Select(k => new KorisnikPrikaziViewModel
            //*** ILI POMOCU OBJECT INCIALIZATORA SVAKI ATRIBUT POSEBNO NAVEDEMO ***
            {
                Naziv = k.Naziv,
                Adresa = k.Adresa,
                KorisnikId = k.KorisnikId,
                IgraNaziv = k.Igra.Naziv,
                IznosDobitka = k.Igra.Iznos
            })
                .ToList();

            //JEDAN NACIN UBACIVANJA U LISTU
            //List<Korisnik> podaci = db.Korisnici
            //    //*** NAZIV VEZE KOJU ZELIMO PREUZETI ***
            //    .Include(s=>s.Igra)
            //    .ToList();

            ViewData["podaci-kljuc"] = podaci;
            return View();
        }



        // *** DRUGI NACIN ***

        //public IActionResult Prikazi()
        //{
        //    List<NekiDrugiNaziv> podatak1 = db.Korisnici.Select(k => new NekiDrugiNaziv
        //    {
        //        KorisnikNaziv = k.Naziv,
        //        KorisnikAdresa = k.Adresa,
        //        KorisnikID = k.Id,
        //        IgraNaziv = k.Igra.Naziv,
        //        IznosDobitka = k.Igra.Iznos
        //    })
        //    .ToList();

        //    KorisnikPrikaziVM model = new KorisnikPrikaziVM();
        //    model.podaciZaHtmlTabelu = podatak1;
        //    model.podatak2 = 5;
        //    model.podatak3 = "neki podatak";


        //    //Da se oslobode resursi
        //    db.Dispose();
        //    return View("Prikazi", model);
        //}

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