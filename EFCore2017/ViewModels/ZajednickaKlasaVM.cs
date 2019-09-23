using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore2017.EntityModels;

namespace EFCore2017.ViewModels
{
    public class ZajednickaKlasaVM
    {
        public List<Igra> IgreList { get; set; }

        public List<Korisnik> KorisniciList { get; set; }
    }
}
