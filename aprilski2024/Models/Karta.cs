using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Models
{
    [Table("Karta")]
    public class Karta
    {
        [Key]
        public int ID{get;set;}
        
        
        [Required]
        [Range(0,1000)]
        public int Red{get;set;}
        [Required]
       [Range(1,1000)]
        public int Sediste{get;set;}

        [Required]
        [Range(100,10000)]
        public float Cena{get;set;}

        [Required]
        public bool Kupljena{get;set;}
        public Projekcija PripadaProjekciji{get;set;}
        
    }
}