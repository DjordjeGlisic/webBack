using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Racun")]
    public class Racun
    {
        [Key]
        public int ID{get;set;}
        [JsonIgnore]
        public Stan brojStana{get;set;}
        [Required] 
        [Range(1,12)]
        public int MesecIzdavanja{get;set;}
        [Required]
        public float Struja{get;set;}
        [Required]
        public float Voda{get;set;}
        [Required]
        public float Komunalije{get;set;}
        [Required]
        [StringLength(2)]
        public string Placen{get;set;}
        
    }
}