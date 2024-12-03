using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;


namespace Models
{
    
        public enum Boja{
            Crna=1,
            Bela,
            Zelena,
            Plava,
            Crvena,
            Zuta
        }
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
        [StringLength(10)]
        public string Korisncko{get;set;}
        [Required]
        [StringLength(20)]
        public string Email{get;set;}
        [Required]
        public Boja Boja{get;set;}
        [Required]
        [StringLength(30)]
        public string Sifra{get;set;}
        [JsonIgnore]
        public List<Cet>KorisniciSaNadimkom{get;set;}=new List<Cet>();

    }
}