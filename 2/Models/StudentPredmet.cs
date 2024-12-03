using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Models
{
    [Table("StudentPredmet")]
    public class StudentPredmet
    {
        [Key]
        public int Id { get; set; }
        [Range(6,10)]
        public int Ocena{get;set;}

        public virtual Student Student{get;set;}
        public virtual Predmet Predmet{get;set;}
        public virtual IspitniRok Rok{get;set;} 
    }

}