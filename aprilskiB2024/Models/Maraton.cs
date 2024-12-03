using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models{
    [Table("Maraton")]
    public class Maraton{
        [Key]
        public int Id{get;set;}
        [Required]
        [StringLength(20)]
        public string Naziv{get;set;}
        [Required]
        [StringLength(20)]
        public string Lokacija{get;set;}
        [Required]
        [Range(1000,10000)]
        public int DuzinaStazeM{get;set;}
          [Required]
        public string VremePocetkaTrke{get;set;}
        [Required]
        public string VremeKrajaTrke{get;set;}
        public List<Trka> Ucensici{get;set;}=new List<Trka>();
        
        
        
    }
}