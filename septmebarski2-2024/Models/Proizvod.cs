using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Proizvod")]
    public class Proizvod
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        [StringLength(100)]
        public string? Naziv{get;set;}
        [Required]
        [Range(0,100)]
        public int Kolicina{get;set;}
        [Required]
        [Range(1,999)]
        public int Cena{get;set;}
        [StringLength(1000)]
        public string? Opis{get;set;}
        [JsonIgnore]

        public List<Korpa>Korpe{get;set;}=new List<Korpa>();
        public Kategorija? Kategorija{get;set;}

    }
}