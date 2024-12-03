using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Kategorija")]
    public class Kategorija
    {
        [Key]
        public int Id { get;set;}
        [StringLength(30)]
        [Required]
        public string? Naziv{get;set;}
        [JsonIgnore]
        public List<Proizvod>Proizvodi{get;set;}=new List<Proizvod>();
    }
}