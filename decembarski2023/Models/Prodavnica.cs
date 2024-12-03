using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models{
    [Table("Prodavnica")]
    public class Prodavnica{
        [Key]
        public int Id{get;set;}
        [Required]
        [StringLength(30)]
        public string Naziv{get;set;}
        [Required]
        [StringLength(30)]
        public string Lokacija{get;set;}
        [Required]
        
        public long Telefon{get;set;}

        public List<Proizvod> ListaProizvoda{get;set;}=new List<Proizvod>(); 
        

    }

}