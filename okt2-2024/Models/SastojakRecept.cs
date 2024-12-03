using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models{
    [Table("SastojakRecept")]
    public class SastojakRecept{
        [Key]
        public int Id{get;set;}
        [Required]
        [StringLength(10)]
        public string Kolicina{get;set;}
        public Sastojak Sastojak{get;set;}  
        public Recept Recept{get;set;}
    } 
}