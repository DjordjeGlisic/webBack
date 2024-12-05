using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace Models
{
    [Table("Korisnik")]
    public class Korisnik
    {
        [Key]
        public int Id{get;set;}
        [Required]
        [StringLength(20)]
        public string Ime{get;set;}
        [Required]
        [StringLength(20)]
        public string Prezime{get;set;}

        [Required]
        public long BrojLicneKarte{get;set;}
        public List<Tura> Rezervacije{get;set;}=new List<Tura>();   
        

    }
    
}