using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace Models
{
    [Table("Predmet")]
    public class Predmet
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Naziv{get;set;}
        [Required]
        [Range(3,8)]
        public int Espb{get;set;}
        [Required]
        [Range(1,5)]
        public int Godina{get;set;}
        [JsonIgnore]
        public virtual List<StudentPredmet>PredmetStudent{get;set;}=new List<StudentPredmet>(); 


    }
}