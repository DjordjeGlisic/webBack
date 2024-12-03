using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Models
{
    [Table("Projekcija")]
    public class Projekcija
    {
        [Key]
        public int ID{get;set;}
        [Required]
        [StringLength(50)]
        public string Naziv{get;set;}
        [Required]
        public DateTime Datum{get;set;}

        public Sala Lokacija{get;set;}
        public List<Karta> Karte{get;set;}=new List<Karta>();
        [Required]
        public long Sifra{get;set;}

        
    }
}