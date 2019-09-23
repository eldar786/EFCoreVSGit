using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EFCore2017.ViewModels;

namespace EFCore2017.EntityModels
{
    public class Korisnik
    {
        [Key]
        public int Id { get; set; }

        public string Naziv { get; set; }

        public string Adresa { get; set; }

        public Igra Igra { get; set; }
        
    }
}
