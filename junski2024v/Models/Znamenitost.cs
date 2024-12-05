using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace Models
{
    [Table("Znamenitost")]
    public class Znamenitost
    {
        [Key]
        public int Id{get;set;}

        [Required]
        [StringLength(30)]
        public string ImeZnamenitosti{get;set;}

        [Range(3,100)]
        public float Cena{get;set;}

        public List<Tura> PripadaTurama{get;set;}=new List<Tura>();        
        
        

    }
    
}