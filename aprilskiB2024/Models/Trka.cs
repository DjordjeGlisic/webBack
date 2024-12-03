using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models{
    [Table("Trka")]
    public class Trka{
        [Key]
        public int Id{get;set;}
       
        [Required]
        [Range(1,1000)]
        public int BrojMaratonca{get;set;}
        [Required]
        public int Pozicija{get;set;}
        [Required]
        public float PredjenoMetra{get;set;}

        [Required]
        [Range(1,100)]
        public int Progres{get;set;}
        [Required]
        [Range(5,45)]
        public float BrzinaTrkaca{get;set;}
        [Required]
      //  [JsonIgnore]
        public Trkac Maratonac{get;set;}
        [Required]
        public Maraton Takmicenje{get;set;}
      
        
        
        
    }
}