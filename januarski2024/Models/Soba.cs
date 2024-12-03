using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Models
    {

    
        [Table("Soba")]
        public class Soba
        {
            [Key]
            public int ID{get;set;}
            [Required]
            [StringLength(25)]
            public string Naziv{get;set;}
            [Required]
            [Range(1,10)]
            public int MaxBrojClanova{get;set;}
            public List<Cet>KorisniciSaNadimkom{get;set;}=new List<Cet>();

        }
    }