using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Korisnik")]
    public class Korisnik
    {
        [Key]
        public int ID{get;set;}
        [Required]
        [StringLength(50)]
        public string Ime{get;set;}
        [Required]
        [StringLength(50)]
        public string Prezime{get;set;}
        [Required]
         [RegularExpression(@"^\d{13}$", ErrorMessage = "Polje mora imati tačno 13 cifara.")]
         public long Jmbg{get;set;}
         [Required]
         [RegularExpression(@"\d{9}$",ErrorMessage ="Broj vozacke dozvole poseduje tacno 9 cifara")]
         public long BrojVozacke{get;set;}
         [JsonIgnore]
         public List<Automobil> TrenutnoIznajmljeni{get;set;}=new List<Automobil>();
        
    }
}