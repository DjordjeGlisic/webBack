using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public int ID{ get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Ime{ get; set; }
        [MinLength(3)]
        [MaxLength(20)]
        public string Prezme{ get; set; }
        [Required]
        [Range(10000,20000)]
        public long Indeks{get;set;}
        [JsonIgnore]
        public virtual List<StudentPredmet> StudentPredmet {get;set;}=new List<StudentPredmet>(); 
        
        
    }
}