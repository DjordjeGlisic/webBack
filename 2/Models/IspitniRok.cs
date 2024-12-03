using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace Models
{
    [Table("IspitniRok")]
    public class IspitniRok
    {
        [Key]
        public int Id{get;set;}
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string NazivRoka{get;set;}
        [JsonIgnore]
       public virtual List<StudentPredmet>spojevi{get;set;}=new List<StudentPredmet>();
        
    }

}