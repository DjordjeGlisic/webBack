using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models{
    [Table("Trkac")]
    public class Trkac{
        [Key]
        public int Id{get;set;}
        [Required]
        [StringLength(20)]
        public string Ime{get;set;}
        [Required]
        [StringLength(20)]
        public string Prezime{get;set;}
        [Required]
        public long JMBG{get;set;}

        [Required]
        public int Nagrade{get;set;}

        public List<Trka> Maratoni{get;set;}=new List<Trka>();
        
        
        
    }
}