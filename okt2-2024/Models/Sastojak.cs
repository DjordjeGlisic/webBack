using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models{
    [Table("Sastojak")]
    public class Sastojak{
        [Key]
        public int Id{get;set;}
        [Required]
        [StringLength(10)]
        public string Boja{get;set;}
         [Required]
        [StringLength(50)]
        public string Naziv{get;set;}
        [JsonIgnore]
        public List<SastojakRecept> pripadaReceptima{get;set;}=new List<SastojakRecept>();
    } 
}