using EFCore2017.EntityModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore2017.ViewModels
{
    public class KorisnikPrikaziViewModel
    {
        public int KorisnikId { get; set; }

        public string Naziv { get; set; }
        public string Adresa { get; set; }
        
        public string IgraNaziv { get; set; }

        //? - savljamo ako nema zapisa, da ne javi gresku, moze biti null
        public int? IznosDobitka { get; set; }

        //Klasa "SelectListItem" koja samo sadrzi text i value (treba uraditi konverziju u kontroleru u select list item)
        public List<SelectListItem> Igre { get; set; }


    }
}
