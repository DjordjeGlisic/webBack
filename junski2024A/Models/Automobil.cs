using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Automobil")]
    public class Automobil
    {
        [Key]
        public int ID{get;set;}
        [Required]
        [Range(1950,2024)]
        public int Godiste{get;set;}
        [Required]
        [Range(0,500000)]
        public long Kilometraza{get;set;}
        [Required]
         [Range(2,5)]
         public int BrojSedista{get;set;}
         [Required]
        [Range(1,1000)]
         public float CenaPoDanu{get;set;}
         public Model ModelAutomobila{get;set;}
        public Korisnik KorisnikAutomobila{get;set;}
        public int BrojDana{get;set;}


        
    }
}