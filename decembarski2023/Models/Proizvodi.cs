using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models{
    public enum Kategorija{
            Knjiga=1,
            Igracka,
            Pribor,
            Ostalo
        }
    [Table("Proizvod")]
    public class Proizvod{
        [Key]
        public int Id{get;set;}
        
        [Required]
        [StringLength(50)]
        public string Naziv{get;set;}
        [Required]
        public Kategorija KategorijaProizvoda{get;set;}
        [Required]
        [Range(1,1000)]
        public float Cena{get;set;}
        [Required]
        [Range(1,100)]
        public int Kolicina{get;set;}
    
        public Prodavnica PripadaProdavnici{get;set;}
        
    }

}