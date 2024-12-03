using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Models
{
    [Table("Sala")]
    public class Sala
    {
        [Key]
        public int ID{get;set;}
        [Required]
        [Range(1,1000)]
        public int Redovi{get;set;}
         [Required]
        [StringLength(20)]
        public string Naziv{get;set;}
        [Required]
       [Range(1,1000)]
        public int Sedista{get;set;}

        public List<Projekcija> Projekcije{get;set;}=new List<Projekcija>();
   

        
    }
}