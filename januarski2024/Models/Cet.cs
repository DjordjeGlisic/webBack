using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models{
    [Table("Cet")]
    public class Cet
    {
        [Key]
        public int Id{get;set;}

        [Required]
        public string Nadimak{get;set;}
        public Korisnik Korisnik{get;set;}
        public Soba Soba{get;set;}

    }

}