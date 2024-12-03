using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models{
  
    [Table("Prodavnica")]
    public class Prodavnica{
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Naziv{get;set;}
        [Required]
        [StringLength(50)]
        public string Lokacija{get;set;}

       public List<Auto>Proizvodi{get;set;}
        
    }
}