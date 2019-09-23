using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore2017.ViewModels
{

    public class KorisnikPrikaziVM
    {
        public List<NekiDrugiNaziv> podaciZaHtmlTabelu;
        public int podatak2;
        public string podatak3;
    }
    public class NekiDrugiNaziv
    {
        public string KorisnikNaziv { get; set; }
        public string KorisnikAdresa { get; set; }
        public int KorisnikID { get; set; }
        public string IgraNaziv { get; set; }
        public int? IznosDobitka { get; set; }

               

    }
}
