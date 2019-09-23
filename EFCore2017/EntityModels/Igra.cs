using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore2017.EntityModels
{
    public class Igra
    { 
        [Key]
        public int Id { get; set; }
        public int KorisnikID { get; set; }
        [ForeignKey("KorisnikID")]
        public string Naziv { get; set; }
        public int Iznos { get; set; }
        
    }
}
