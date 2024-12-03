using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models{
    [Table("Recept")]
    public class Recept{
        [Key]
        public int Id{get;set;}
        [Required]
        [StringLength(50)]
        public string Ime{get;set;}
        [Required]
        [StringLength(1000)]
        public string TextRecepta{get;set;}
        [JsonIgnore]
        public List<SastojakRecept> listaSastojka{get;set;}=new List<SastojakRecept>();
    } 
}