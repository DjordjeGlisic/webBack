using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Stan")]
    public class Stan
    {
        [Key]
        public int BrojStana{get;set;}
        [Required]
        [StringLength(50)]
        public string ImeVlasnika{get;set;}
        [Required]
        
        [Range(10,100)]
        public float Povrsina{get;set;}
        [Range(1,10)]
        public int BrojClanova{get;set;}
        public List<Racun> Racuni{get;set;}=new List<Racun>();
        
    }

}