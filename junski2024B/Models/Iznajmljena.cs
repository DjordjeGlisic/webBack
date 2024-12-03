using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Models{
    [Table("Iznajmljena")]
    public class Iznajmljena
    {
        [Key]
        public int Id{get;set;}
      
        [Required]
        public int Godina{get;set;}
        [Required]
        public int Mesec{get;set;}
        [Required]
        public int Dan{get;set;}
        
        
        public Sala Sala{get;set;}
        public Korisnik Korisnik{get;set;}
        

        

    }
}