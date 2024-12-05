using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace Models
{
    [Table("Tura")]
    public class Tura
    {
        [Key]
        public int Id{get;set;}
        [Required]
        [Range(0,5)]
        public int PresotaloMesta{get;set;}
        [Required]
        [Range(3,100000)]
        public float Cena{get;set;}
        public List<Znamenitost> Znamenitosti{get;set;}=new List<Znamenitost>();   
        public List<Korisnik> Rezervisani{get;set;}=new List<Korisnik>();    
        

    }
    
}