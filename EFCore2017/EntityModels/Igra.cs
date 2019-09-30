using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EFCore2017.ViewModels;


namespace EFCore2017.EntityModels
{
    public class Igra
    { 
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int? Iznos { get; set; }

        
        [ForeignKey("Korisnik")]
        public int KorisnikId { get; set; }
        //public virtual Korisnik Korisnik { get; set; }

    }
}
