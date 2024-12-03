using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models{
  
    [Table("Auto")]
    public class Auto{
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Naziv{get;set;}
        [Required]
        [StringLength(50)]
        public string Model{get;set;}

        [Required]
        [StringLength(50)]
        public string Boja{get;set;}
        [Range(1000,100000)]
        public float Cena{get;set;}

        [Required]
        [Range(1,1000)]
        public int Kolicina{get;set;}
        
        [StringLength(30)]
        public DateTime? DatumPoslednjeProdaje{get;set;}=null;

        public Brend BrendAuta{get;set;}
        public Prodavnica Prodavnica{get;set;}
        
    }
}