using EFCore2017.EntityModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore2017.ViewModels
{
    public class KorisnikUrediVM
    {
        public int KorisnikID { get; set;  }
        public string KorisnikNaziv { get; set;  }
        public string KorisnikAdresa { get; set;  }

        public string IgraNaziv { get; set; }
        public int? IznosDobitka { get; set; }

        public List<SelectListItem> Igre { get; set; }
    }
}
