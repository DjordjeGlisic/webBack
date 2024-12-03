using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Model")]
    public class Model
    {
        [Key]
        public int ID{get;set;}
        [Required]
        [StringLength(50)]
        public string NazivModela{get;set;}
        [JsonIgnore]
        public List<Automobil>ListaAutomobila{get;set;}=new List<Automobil>();        
    }
}